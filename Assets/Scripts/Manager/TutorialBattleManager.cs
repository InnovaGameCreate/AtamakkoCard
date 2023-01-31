using System.Audio;
using System.Effect;
using Arena;
using Atamakko;
using Card;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UI;
using UniRx;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Manager
{
    public class TutorialBattleManager : BattleManager
    {
        // 名前
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI enemyName;
        private int _enemyDamage; // 敵のダメージ
        private int _enemyPlace; // 敵の位置
        
        [SerializeField] private TutorialUI tutorialUI;
        [SerializeField] private GameObject speedupButton;
        private readonly CardSlot[] _myHand = new CardSlot[6];
        private readonly Graphic[] _graphicsSlots = new Graphic[6];
        private readonly Graphic[] _graphicsSettings = new Graphic[3];
        private Graphic _graphicSpecial;
        private Graphic _graphicDecision;
        private Graphic _graphicSpeedup;

        private int _settingTimes;
        private int _actionTimes;

        /// <summary>
        /// 待ちフェイズ
        /// </summary>
        protected override async void WaitingGame()
        {
            playerName.text = PlayerConfig.PlayerName;
            enemyName.text = enemyDeckData._enemyName;
            PhotonNetwork.OfflineMode = true;
            await UniTask.Delay(10);
            _CurrentState.Value = GameState.Init;
        }

        /// <summary>
        /// スタートフェイズ
        /// </summary>
        protected override async void StartGame()
        {
            //Debug.Log("過去のシーン：" + PastSceneManager.Instance.);
            // カードデータを取得
            await StartCoroutine(CardData.GetData());

            // デッキのインスタンス生成
            var playerDeck = new List<int>
            {
                0,
                2,
                5,
                7,
                8,
                1,
                6,
                1,
                0,
                2,
                5,
                6
            };
            var enemyDeck = new List<int>
            {
                9,
                105,
                0,
                65,
                87,
                41,
                0,
                9,
                41,
                66,
                106,
                88
            };

            decisionButton.MyInteractable = false;

            await UniTask.Delay(10);
            
            // プレイヤーの初期設定
            Enemy.SetCPU(EnemyType.Tutorial);
            Player.Initialize(playerDeck);
            Enemy.Initialize(enemyDeck);
            Player.AtamakkoData.SetImage(PlayerConfig.Equipmnet.ToArray());

            // ゲーム終了の設定
            Enemy.AtamakkoData.MyHp
                .Where(hp => hp == 0)
                .Subscribe(_ =>
                {
                    _CurrentState.Value = GameState.End;
                    AnimationManager.Instance.ResultFadeIn(true);
                })
                .AddTo(this);
            
            decisionButton.Pushed // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    decisionButton.MyInteractable = false;
                    CardMobile.Value = false;
                    _next.Value = true;
                })
                .AddTo(this);

            for (var i = 0; i < battleSlots.Length; i++)
            {
                _graphicsSettings[i] = battleSlots[i].GetComponent<Graphic>();
            }
            _graphicSpecial = ultimateButton.gameObject.GetComponent<Graphic>();
            _graphicDecision = decisionButton.gameObject.GetComponent<Graphic>();
            _graphicSpeedup = speedupButton.GetComponent<Graphic>();

            // ドローフェーズへ
            _CurrentState.Value = GameState.Draw;
        }

        /// <summary>
        /// ドローフェーズ
        /// </summary>
        protected override async void DrawFaze()
        {
            await UniTask.Delay(10);

            if (playerHand.transform.childCount <= 0)   // 手持ちにカードがないなら
            {
                // 自身の手札補充＆スロット生成
                for (var i = 0; i < 6; i++)
                {
                    _myHand[i] = Instantiate(slotPrefab, cardManager);
                    _myHand[i].CreateCard(Player.DrawCard());
                }
                _graphicsSlots[0] = _myHand[5].gameObject.GetComponent<Graphic>();
                _graphicsSlots[0].raycastTarget = false;
                _graphicsSlots[1] = _myHand[2].gameObject.GetComponent<Graphic>();
                _graphicsSlots[1].raycastTarget = false;
                _graphicsSlots[2] = _myHand[1].gameObject.GetComponent<Graphic>();
                _graphicsSlots[2].raycastTarget = false;
                _graphicsSlots[3] = _myHand[0].gameObject.GetComponent<Graphic>();
                _graphicsSlots[3].raycastTarget = false;
                _graphicsSlots[4] = _myHand[3].gameObject.GetComponent<Graphic>();
                _graphicsSlots[4].raycastTarget = false;
                _graphicsSlots[5] = _myHand[4].gameObject.GetComponent<Graphic>();
                _graphicsSlots[5].raycastTarget = false;
                
                // エネミーの手札補充
                for (var i = 0; i < 6; i++)
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
            Player.ResetCorrection();
            Enemy.ResetCorrection();
            foreach (Transform childObj in playerContent.transform)
            {
                Destroy(childObj.gameObject);
            }
            foreach (Transform childObj in enemyContent.transform)
            {
                Destroy(childObj.gameObject);
            }
            await UniTask.Delay(10);
            foreach (var cardID in playerList)
            {
                var card = Instantiate(cardPrefab, playerContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }
            Enemy.DebugDeck();
            foreach (var cardID in enemyList)
            {
                var card = Instantiate(cardPrefab, enemyContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }

            CardMobile.Value = true;
            settingPlace.SetActive(true);
            
            ultimateButton.MyInteractable = !Player.UsedUltimate; // 必殺技を使っているかどうかを判断

            foreach (var battleSlot in battleSlots)
            {
                tutorialUI.SelectCard(_settingTimes);
                _graphicsSlots[_settingTimes].raycastTarget = true;
                _graphicsSettings[_settingTimes % 3].raycastTarget = true;
                var cardMovement = battleSlot.GetComponent<CardMovement>();
                await cardMovement.CheckCardID.ToUniTask(true);
                tutorialUI.HideUI();
                _graphicsSlots[_settingTimes].raycastTarget = false;
                _graphicsSettings[_settingTimes % 3].raycastTarget = false;
                _settingTimes++;
            }

            if (_settingTimes >= 5)
            {
                tutorialUI.EmphasisUltimate();
                _graphicSpecial.raycastTarget = true;
                await ultimateButton.Pushed.ToUniTask(true);
                tutorialUI.HideUI();
                _graphicSpecial.raycastTarget = false;
                await UniTask.Delay(10);
                tutorialUI.UltimateSelect();
                _graphicSpeedup.raycastTarget = true;
                await UniTask.WaitUntil(() => Player.AtamakkoData.UltimateState == UltimateState.Speed);
                tutorialUI.HideUI();
                _graphicSpeedup.raycastTarget = false;
            }

            tutorialUI.EmphasisDecision();
            _graphicDecision.raycastTarget = true;
            await Next.Where(b => b).ToUniTask(true);
            tutorialUI.HideUI();
            _graphicDecision.raycastTarget = false;
            _next.Value = false;
            CardMobile.Value = false;
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
            settingPlace.SetActive(false);
            
            // 敵の行動情報を受け取る
            Enemy.CardSelect(); // CPUがカードを選択する
            Enemy.UltimateSelect();

            // 敵のカードを表示する
            for (var i = 0; i < battleSlots.Length; i++)
            {
                Player.SetSettingCard(battleSlots[i].MyCardID);
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
                        EffectManager.Instance.InstantiateEffect(EffectType.GreenEffect, Enemy.transform);
                        break;
                    case UltimateState.Attack:
                        EffectManager.Instance.InstantiateEffect(EffectType.RedEffect, Enemy.transform);
                        break;
                    case UltimateState.Speed:
                        EffectManager.Instance.InstantiateEffect(EffectType.BlueEffect, Enemy.transform);
                        break;
                }
            }

            for (var i = 0; i < battleSlots.Length; i++)
            {
                enemySlots[i].FlipOver();   // 相手カードを見えるように
                
                battleSlots[i].MySelect.Value = true;
                enemySlots[i].MySelect.Value = true;
                
                await Battle(i);
                
                battleSlots[i].MySelect.Value = false;
                enemySlots[i].MySelect.Value = false;

                battleSlots[i].DeleteCard();
                enemySlots[i].DeleteCard();
                await UniTask.Delay(500);
            }
            
            // 使用済みカードへ
            Player.TrashCard();
            Enemy.TrashCard();
            
            // 必殺技をNormalへ
            Player.AtamakkoData.UltimateState = UltimateState.Normal;
            Enemy.AtamakkoData.UltimateState = UltimateState.Normal;
            
            // ドローフェイズへ
            if (_CurrentState.Value == GameState.End) return;
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
            Debug.Log("敵のカード："+ enemyCard.ID);
            await UniTask.Delay(10);

            int playerPlace, enemyPlace;
            switch (_actionTimes)
            {
                case 0:
                case 1:
                case 2:
                case 5:
                    playerPlace = await SelectPlayer(myCard);
                    ApplyPlayer(myCard, playerPlace);
                    enemyPlace = await SelectEnemy(enemyCard);
                    ApplyEnemy(enemyCard, enemyPlace);
                    break;
                case 3:
                    enemyPlace = await SelectEnemy(enemyCard);
                    ApplyEnemy(enemyCard, enemyPlace);
                    playerPlace = await SelectPlayer(myCard);
                    ApplyPlayer(myCard, playerPlace);
                    break;
                case 4:
                    playerPlace = await SelectPlayer(myCard);
                    enemyPlace = await SelectEnemy(enemyCard);
                    ApplyPlayer(myCard, playerPlace);
                    ApplyEnemy(enemyCard, enemyPlace);
                    break;
            }

            await UniTask.Delay(10);

            await tutorialUI.ActionDirection(_actionTimes);
            tutorialUI.HideUI();
            _actionTimes++;
        }

        private async UniTask<int> SelectPlayer(CardModel card)
        {
            var position = 0;
            switch (card.Kind)
            {
                case "攻撃":
                    var canAttack = Player.CanAttack(card);
                    foreach (var t in canAttack)
                    {
                        var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                        position = t;
                        attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * position);
                        attackArea.AttackPlace = position;
                        switch (_actionTimes)
                        {
                            case 1:
                                if (t != 3) attackArea.gameObject.transform.GetChild(0).gameObject.GetComponent<Graphic>().raycastTarget = false;
                                break;
                            case 4:
                            case 5:
                                if (t != 4) attackArea.gameObject.GetComponent<Graphic>().raycastTarget = false;
                                break;
                        }

                        attackArea.Selected
                            .Subscribe(i =>
                            {
                                position = i;
                                _next.Value = true;
                            })
                            .AddTo(attackArea);
                    }
                    break;
                case "移動":
                    var canMove = Player.CanMove(card);
                    foreach (var t in canMove)
                    {
                        position = t;
                        var moveArea = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[position].transform);
                        moveArea.MovePlace = position;
                        switch (_actionTimes)
                        {
                            case 0:
                            case 3:
                                if (t != 4) moveArea.gameObject.GetComponent<Graphic>().raycastTarget = false;
                                break;
                            case 2:
                                if (t != 3) moveArea.gameObject.GetComponent<Graphic>().raycastTarget = false;
                                break;
                        }

                        moveArea.Selected
                            .Subscribe(i =>
                            {
                                position = i;
                                _next.Value = true;
                            })
                            .AddTo(moveArea);
                    }
                    break;
            }
            tutorialUI.SelectAction(_actionTimes);
            await Next.Where(b => b).ToUniTask(true);
            _next.Value = false;
            tutorialUI.HideUI();
            return position;
        }

        private async UniTask<int> SelectEnemy(CardModel card)
        {
            var position = card.Kind switch
            {
                "攻撃" => Enemy.AttackSelect(Player.AtamakkoData.MyPosition, card),
                "移動" => Enemy.MoveSelect(Player.AtamakkoData.MyPosition, card),
                _ => 0
            };
            await UniTask.Delay(10);
            return position;
        }

        private void ApplyPlayer(CardModel card, int position)
        {
            switch (card.Kind)
            {
                case "攻撃":
                    var myDamage = Player.GetDamage(card.Damage);
                    if (Enemy.AtamakkoData.MyPosition != position) return;
                    Enemy.AddDamage(myDamage);
                    switch (card.Effect)
                    {
                        case "射撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            Debug.Log("射撃エフェクトを再生:" + Player.gameObject.name);
                            break;
                        case "斬撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            Debug.Log("斬撃エフェクトを再生:" + Player.gameObject.name);
                            break;
                    }
                    break;
                case "移動":
                    Player.Move(position);
                    SeManager.Instance.ShotSe(SeType.MoveCard);
                    break;
            }
        }

        private void ApplyEnemy(CardModel card, int position)
        {
            switch (card.Kind)
            {
                case "攻撃":
                    var enemyDamage = Enemy.GetDamage(card.Damage);
                    if (Player.AtamakkoData.MyPosition != position) return;
                    Player.AddDamage(enemyDamage);
                    switch (card.Effect)
                    {
                        case "射撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            Debug.Log("射撃エフェクトを再生:" + Enemy.gameObject.name);
                            break;
                        case "斬撃":
                            EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Enemy.transform);
                            SeManager.Instance.ShotSe(SeType.AttackCard);
                            Debug.Log("斬撃エフェクトを再生:" + Enemy.gameObject.name);
                            break;
                    }
                    break;
                case "移動":
                    Enemy.Move(position);
                    SeManager.Instance.ShotSe(SeType.MoveCard);
                    break;
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
    }
}
