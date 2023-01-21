using System.Audio;
using System.Collections.Generic;
using System.Effect;
using System.Linq;
using Atamakko;
using Card;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using TMPro;
using UI;
using UniRx;
using UnityEngine;

namespace Manager
{
    /// <summary>
    /// オンライン戦の戦闘システムを管理するクラス
    /// </summary>
    public class OnlineManager : BattleManager
    {
        // 名前
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI enemyName;
        
        private bool _getData; // データを受け取ったかどうか
        private List<int> _enemyDeck; // 敵のデッキ
        private int _enemyDamage; // 敵のダメージ
        private int _enemyPlace; // 敵の位置

        /// <summary>
        /// 待ちフェイズ
        /// </summary>
        protected override async void WaitingGame()
        {
            PhotonNetwork.OfflineMode = false;
            //自分と相手のプレイヤーネームを表示
            playerName.text = PlayerConfig.PlayerName;
            photonView.RPC(nameof(SetEnemyName), RpcTarget.Others, playerName.text);
            

            await TimeCounter.Instance.CountDown(3);
            // スタートフェイズへ
            _CurrentState.Value = GameState.Init;
        }

        /// <summary>
        /// スタートフェイズ
        /// </summary>
        protected override async void StartGame()
        {
            // カードデータを取得
            await StartCoroutine(CardData.GetData());
            
            // デッキのインスタンス生成
            var playerDeck = PlayerConfig.Deck;
            photonView.RPC(nameof(SendDeck), RpcTarget.Others, playerDeck.ToArray());
            
            // データを受け取るまで待機
            await UniTask.WaitUntil((() => _getData));
            _getData = false;
            await UniTask.Delay(10);

            // プレイヤーの初期設定
            Player.Initialize(playerDeck);
            Enemy.Initialize(_enemyDeck);
            Player.AtamakkoData.SetImage(PlayerConfig.Equipmnet.ToArray());
            photonView.RPC(nameof(SetEnemyEquipment), RpcTarget.Others, PlayerConfig.Equipmnet.ToArray());
            
            decisionButton.MyInteractable = false;
            
            // ゲーム終了の設定
            Player.AtamakkoData.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _CurrentState.Value = GameState.End;
                    AnimationManager.Instance.ResultFadeIn(false);
                })
                .AddTo(this);
            Enemy.AtamakkoData.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _CurrentState.Value = GameState.End;
                    AnimationManager.Instance.ResultFadeIn(true);
                })
                .AddTo(this);
            Option.Instance.Surrender
                .Subscribe(_ =>
                {
                    _CurrentState.Value = GameState.End;
                    AnimationManager.Instance.ResultFadeIn(false);
                })
                .AddTo(this);
            
            // ドローフェイズへ
            _CurrentState.Value = GameState.Draw;
        }

        /// <summary>
        /// 敵の名前を受け取る。
        /// </summary>
        /// <param name="text">名前</param>
        [PunRPC]
        private void SetEnemyName(string text)
        {
            enemyName.text = text;
        }

        /// <summary>
        /// ドローフェーズ
        /// </summary>
        protected override async void DrawFaze()
        {
            if (Player.CheckDeck()) // 自デッキにカードがないなら
            {
                // デッキを補充する
                Player.RefillDeck();
                photonView.RPC(nameof(SendDeck), RpcTarget.Others, Player.GetDeck().ToArray());
                
                // データを受け取るまで待機
                await UniTask.WaitUntil((() => _getData));
                _getData = false;
                await UniTask.Delay(10);
                
                Enemy.SetDeck(_enemyDeck);
            }
            
            if (playerHand.transform.childCount <= 0)   // 手持ちにカードがないなら
            {
                // 自身の手札補充＆スロット生成
                for (int i = 0; i < 6; i++)
                {
                    CreateSlot(Player.DrawCard());
                }
                // エネミーの手札補充
                for (int i = 0; i < 6; i++)
                {
                    Enemy.DrawCard();
                }
            }

            // 選択フェイズへ
            _CurrentState.Value = GameState.Select;
        }

        /// <summary>
        /// 選択フェイズ
        /// </summary>
        protected override async void SelectFaze()
        {
            // デッキ情報を更新
            var playerList = Player.GetDeck();
            var enemyList = Enemy.GetDeck();
            // 補正を初期化する
            Player.ResetCorrection();
            Enemy.ResetCorrection();
            // 山札情報をクリアする
            foreach (Transform childObj in playerContent.transform)
            {
                Destroy(childObj.gameObject);
            }
            foreach (Transform childObj in enemyContent.transform)
            {
                Destroy(childObj.gameObject);
            }
            await UniTask.Delay(10);
            // 山札情報を元に表示する
            foreach (var cardID in playerList)
            {
                var card = Instantiate(cardPrefab, playerContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }
            foreach (var cardID in enemyList)
            {
                var card = Instantiate(cardPrefab, enemyContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }

            CardMobile = true; // カードを移動可能に
            settingPlace.SetActive(true); // セットエフェクトを表示
            
            ultimateButton.MyInteractable = !Player.UsedUltimate; // 必殺技を使っているかどうかを判断
            decisionButton.Pushed // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    decisionButton.MyInteractable = false;
                    CardMobile = false;
                    TimeCounter.Instance.EndTimer(); // タイマーを0にする
                })
                .AddTo(this);

            await TimeCounter.Instance.CountDown(120);    // カウントタイマー起動（120s）
            CardMobile = false;
            settingPlace.SetActive(false);
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF

            // セットしたカードIDを得る
            foreach (var t in battleSlots)
            {
                Player.SetSettingCard(t.MyCardID);
            }

            // セットしたカードIDを送る
            photonView.RPC(nameof(SendSelectFaze), 
                    RpcTarget.Others, 
                    Player.GetSettingCards().ToArray(), 
                    Player.AtamakkoData.UltimateState);
            
            // データを受け取るまで待機
            await UniTask.WaitUntil(() => _getData);
            _getData = false;
            await UniTask.Delay(10);

            // 敵のカードを表示する
            for (int i = 0; i < enemySlots.Length; i++)
            {
                EnemyCard(i, Enemy.GetNowCardID(i));
            }
            
            // 戦闘フェイズへ
            _CurrentState.Value = GameState.Battle;
        }

        /// <summary>
        /// 戦闘フェイズ
        /// </summary>
        protected override async void BattleFaze()
        {
            // 必殺技を選択している
            if (Player.AtamakkoData.UltimateState != UltimateState.Normal)
            {
                AnimationManager.Instance.MyUltimateCutIn(Player.AtamakkoData.UltimateState);
                informText.setText(Player.AtamakkoData.UltimateState);
                switch (Player.AtamakkoData.UltimateState)
                {
                    case UltimateState.Recover:
                        EffectManager.Instance.InstantiateEffect(EffectType.GreenEffect, Player.transform);
                        SeManager.Instance.ShotSe(SeType.ultimateHeal);//必殺技使用時にSEを再生
                        break;
                    case UltimateState.Attack:
                        EffectManager.Instance.InstantiateEffect(EffectType.RedEffect, Player.transform);
                        SeManager.Instance.ShotSe(SeType.ultimateDamageUp);//必殺技使用時にSEを再生
                        break;
                    case UltimateState.Speed:
                        EffectManager.Instance.InstantiateEffect(EffectType.BlueEffect, Player.transform);
                        SeManager.Instance.ShotSe(SeType.ultimateSpeedUp);//必殺技使用時にSEを再生
                        break;
                }
            }
            Player.UseUltimate();
            // 必殺技を選択している
            if (Enemy.AtamakkoData.UltimateState != UltimateState.Normal)
            {
                await AnimationManager.Instance.EnUltimateCutIn();
                switch (Enemy.AtamakkoData.UltimateState)
                {
                    case UltimateState.Recover:
                        EffectManager.Instance.InstantiateEffect(EffectType.GreenEffect, Player.transform);
                        break;
                    case UltimateState.Attack:
                        EffectManager.Instance.InstantiateEffect(EffectType.RedEffect, Enemy.transform);
                        break;
                    case UltimateState.Speed:
                        EffectManager.Instance.InstantiateEffect(EffectType.BlueEffect, Enemy.transform);
                        break;
                }
            }
            Enemy.UseUltimate();

            // スロットごとに戦闘処理を行う
            for (int i = 0; i < battleSlots.Length; i++)
            {
                enemySlots[i].FlipOver();   // 相手カードを見えるように
                
                battleSlots[i].MySelect.Value = true;
                enemySlots[i].MySelect.Value = true;
                
                await Battle(i);
                
                battleSlots[i].MySelect.Value = false;
                enemySlots[i].MySelect.Value = false;

                battleSlots[i].DeleteCard();
                enemySlots[i].DeleteCard();
            }
            
            // 使用済みカードへ
            Player.TrashCard();
            Enemy.TrashCard();
            
            // 必殺技をNormalへ
            Player.AtamakkoData.UltimateState = UltimateState.Normal;
            Enemy.AtamakkoData.UltimateState = UltimateState.Normal;
            
            // ドローフェイズへ
            _CurrentState.Value = GameState.Draw;
        }

        /// <summary>
        /// 各スロットで戦闘を行う
        /// </summary>
        /// <param name="slotNum">スロット番号</param>
        private async UniTask Battle(int slotNum)
        {
            // カード生成
            var myCard = new CardModel(CardData.CardDataArrayList[Player.GetNowCardID(slotNum)]);
            var enemyCard = new CardModel(CardData.CardDataArrayList[Enemy.GetNowCardID(slotNum)]);
            // 先制度取得
            int myInitiative = Player.GetInitiative(myCard.Initiative);
            int enemyInitiative = Enemy.GetInitiative(enemyCard.Initiative);
            await UniTask.Delay(10);

            // 優先度の処理
            if (myInitiative == enemyInitiative && myCard.Kind == enemyCard.Kind)
            {
                if (myCard.Kind == "攻撃")
                {
                    var canAttack = Player.CanAttack(myCard);
                    int playerAttack = 0;
                    foreach (var t in canAttack)
                    {
                        var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                        playerAttack = t;
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * playerAttack);
                        attackArea.AttackPlace = playerAttack;

                        attackArea.Selected
                            .Subscribe(i =>
                            {
                                playerAttack = i;
                                TimeCounter.Instance.EndTimer();
                            })
                            .AddTo(attackArea);
                    }
                    await TimeCounter.Instance.CountDown(30);

                    int myDamage = Player.GetDamage(myCard.Damage);
                    photonView.RPC(nameof(SendAction), RpcTarget.Others, myDamage, playerAttack);

                    await UniTask.WaitUntil(() => _getData);
                    _getData = false;
                    await UniTask.Delay(10);

                    if (Enemy.AtamakkoData.MyPosition == playerAttack)
                    {
                        Enemy.AddDamage(myDamage);
                    }
                    if (myCard.Additional == "〇")
                    {
                        Player.Move(playerAttack);
                    }
                    
                    if (Player.AtamakkoData.MyPosition == _enemyPlace)
                    {
                        Player.AddDamage(_enemyDamage);
                    }
                    if (enemyCard.Additional == "〇")
                    {
                        Enemy.Move(_enemyPlace);
                    }
                }

                if (myCard.Kind == "移動")
                {
                    var canMove = Player.CanMove(myCard);
                    int playerMove = 0;
                    foreach (var t in canMove)
                    {
                        playerMove = t;
                        var moveArea = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[playerMove].transform);
                        moveArea.MovePlace = playerMove;

                        moveArea.Selected
                            .Subscribe(i =>
                            {
                                playerMove = i;
                                TimeCounter.Instance.EndTimer();
                            })
                            .AddTo(moveArea);
                    }
                    await TimeCounter.Instance.CountDown(30);
                    photonView.RPC(nameof(SendAction), RpcTarget.Others, 0, playerMove);

                    await UniTask.WaitUntil(() => _getData);
                    _getData = false;
                    await UniTask.Delay(10);

                    Player.Move(playerMove);
                    if (myCard.Additional == "〇" && Enemy.AtamakkoData.MyPosition == playerMove)
                    {
                        int myDamage = Player.GetDamage(myCard.Damage);
                        Enemy.AddDamage(myDamage);
                    }
                    Enemy.Move(_enemyPlace);
                    if (enemyCard.Additional == "〇" && Player.AtamakkoData.MyPosition == _enemyPlace)
                    {
                        int enemyDamage = Enemy.GetDamage(enemyCard.Damage);
                        Player.AddDamage(enemyDamage);
                    }
                }
            }
            else if (myInitiative > enemyInitiative || (myInitiative == enemyInitiative && myCard.Kind == "攻撃"))
            {
                await PlayerTurn(myCard);
                await EnemyTurn(enemyCard);
            }
            else if (myInitiative < enemyInitiative || (myInitiative == enemyInitiative && myCard.Kind == "移動"))
            {
                await EnemyTurn(enemyCard);
                await PlayerTurn(myCard);
            }
        
            await UniTask.Delay(10);
        }

        /// <summary>
        /// デッキ情報を送る。
        /// </summary>
        /// <param name="deck">デッキ内のカードID</param>
        [PunRPC]
        private void SendDeck(int[] deck)
        {
            var list = deck.ToList();
            _enemyDeck = new List<int>(list);
            _getData = true;
        }

        [PunRPC]
        private void SetEnemyEquipment(int[] equipments)
        {
            Enemy.AtamakkoData.SetImage(equipments);
        }

        /// <summary>
        /// 選択フェイズでの情報を送る。
        /// </summary>
        /// <param name="setting">セットしたカードID</param>
        /// <param name="ultimateState">必殺技</param>
        [PunRPC]
        private void SendSelectFaze(int[] setting, UltimateState ultimateState)
        {
            var list = setting.ToList();
            Enemy.SetSettingCards(list);
            Enemy.AtamakkoData.UltimateState = ultimateState;
            _getData = true;
        }

        /// <summary>
        /// 選択した行動のデータを送る
        /// </summary>
        /// <param name="damage">ダメージ</param>
        /// <param name="place">選択した位置</param>
        [PunRPC]
        private void SendAction(int damage, int place)
        {
            _enemyDamage = damage;
            _enemyPlace = (place + 3) % 6;
            _getData = true;
        }

        /// <summary>
        /// プレイヤーの行動を処理する
        /// </summary>
        /// <param name="card">使用したカード</param>
        private async UniTask PlayerTurn(CardModel card)
        {
            if (card.Kind == "攻撃")
            {
                var canAttack = Player.CanAttack(card);
                int attackPosition = 0;
                foreach (var t in canAttack)
                {
                    var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                    attackPosition = t;
                    attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * attackPosition);
                    attackArea.AttackPlace = attackPosition;

                    attackArea.Selected
                        .Subscribe(i =>
                        {
                            attackPosition = i;
                            TimeCounter.Instance.EndTimer();
                        })
                        .AddTo(attackArea);
                }

                await TimeCounter.Instance.CountDown(30);
                int myDamage = Player.GetDamage(card.Damage);
                photonView.RPC(nameof(SendAction), RpcTarget.Others, myDamage, attackPosition);

                if (Enemy.AtamakkoData.MyPosition == attackPosition)
                {
                    Enemy.AddDamage(myDamage);
                    switch (card.Effect)
                    {
                        case "射撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        case "斬撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        default:
                            break;
                    }
                }
                if (card.Additional == "〇")
                {
                    Player.Move(attackPosition);
                    SeManager.Instance.ShotSe(SeType.MoveCard);
                }
            }

            if (card.Kind == "移動")
            {
                var canMove = Player.CanMove(card);
                int movePosition = 0;
                foreach (var t in canMove)
                {
                    movePosition = t;
                    var moveArea = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[movePosition].transform);
                    moveArea.MovePlace = movePosition;

                    moveArea.Selected
                        .Subscribe(i =>
                        {
                            movePosition = i;
                            TimeCounter.Instance.EndTimer();
                        })
                        .AddTo(moveArea);
                }

                await TimeCounter.Instance.CountDown(30);
                photonView.RPC(nameof(SendAction), RpcTarget.Others, 0, movePosition);

                Player.Move(movePosition);
                SeManager.Instance.ShotSe(SeType.MoveCard);
                if (card.Additional == "〇" && Enemy.AtamakkoData.MyPosition == movePosition)
                {
                    int myDamage = Player.GetDamage(card.Damage);
                    Enemy.AddDamage(myDamage); switch (card.Effect)
                    {
                        case "射撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Player.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        case "斬撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Player.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 敵の行動を処理する。
        /// </summary>
        /// <param name="card">使用したカード</param>
        private async UniTask EnemyTurn(CardModel card)
        {
            await UniTask.WaitUntil(() => _getData);
            _getData = false;
            await UniTask.Delay(10);
            
            if (card.Kind == "攻撃")
            {
                if (Player.AtamakkoData.MyPosition == _enemyPlace)
                {
                    Player.AddDamage(_enemyDamage);
                    switch (card.Effect)
                    {
                        case "射撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Player.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        case "斬撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Player.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        default:
                            break;
                    }
                }
                if (card.Additional == "〇")
                {
                    Enemy.Move(_enemyPlace);
                    SeManager.Instance.ShotSe(SeType.MoveCard);
                }
            }

            if (card.Kind == "移動")
            {
                Enemy.Move(_enemyPlace);
                SeManager.Instance.ShotSe(SeType.MoveCard);
                if (card.Additional == "〇" && Player.AtamakkoData.MyPosition == _enemyPlace)
                {
                    int enemyDamage = Enemy.GetDamage(card.Damage);
                    Player.AddDamage(enemyDamage);
                    switch (card.Effect)
                    {
                        case "射撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        case "斬撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 敵のカードを生成する
        /// </summary>
        /// <param name="sID">スロット番号</param>
        /// <param name="cID">カードID</param>
        private void EnemyCard(int sID, int cID)
        {
            enemySlots[sID].CreateCard(cID);
            enemySlots[sID].FlipOver();
        }

        /// <summary>
        /// カードスロットを生成する。
        /// </summary>
        /// <param name="cData">カードID</param>
        void CreateSlot(int cData)
        {
            var slot = Instantiate(slotPrefab, cardManager);
            slot.CreateCard(cData);
        }

        /// <summary>
        /// 相手が退出したときの処理
        /// </summary>
        /// <param name="otherPlayer"></param>
        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            if (_CurrentState.Value != GameState.End)
            {
                _CurrentState.Value = GameState.End;
                AnimationManager.Instance.ResultFadeIn(true);
            }
        }
    }
}
