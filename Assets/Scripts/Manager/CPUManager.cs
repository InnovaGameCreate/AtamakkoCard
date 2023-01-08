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

namespace Manager
{
    public class CPUManager : BattleManager
    {
        /*
         * ゲームをスタートする前に行う関数
         */
        protected override async void WaitingGame()
        {
            PhotonNetwork.OfflineMode = true;
            await TimeCounter.Instance.CountDown(3);
            _CurrentState.Value = GameState.Init;
        }

        /*
         * ゲーム開始時に行う関数
         */
        protected override async void StartGame()
        {
            // カードデータを取得
            await StartCoroutine(CardData.GetData());
            
            // デッキのインスタンス生成
            var playerDeck = PlayerConfig.Deck;
            var enemyDeck = enemyDeckData.enemyDeck;

            await UniTask.Delay(10);
            
            // プレイヤーの初期設定
            Player.Initialize(playerDeck);
            Enemy.Initialize(enemyDeck);
            
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
            
            // ドローフェーズへ
            _CurrentState.Value = GameState.Draw;
        }

        /*
         * ドローフェーズ関数
         */
        protected override async void DrawFaze()
        {
            if (Player.CheckDeck()) // 自デッキにカードがないなら
            {
                Player.RefillDeck(); // デッキを補充する
            }
            if (Enemy.CheckDeck()) // 敵デッキにカードがないなら
            {
                Enemy.RefillDeck(); // デッキを補充する
            }

            await UniTask.Delay(10);

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

            _CurrentState.Value = GameState.Select;
        }

        /*
         * セレクトフェーズ関数
         */
        protected override async void SelectFaze()
        {
            // デッキ情報を更新
            var playerList = Player.GetDeck();
            var enemyList = Enemy.GetDeck();
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
            foreach (var cardID in enemyList)
            {
                var card = Instantiate(cardPrefab, enemyContent.transform);
                Debug.Log(cardID);
                card.GetComponent<CardController>().Init(cardID);
            }

            CardMobile = true;
            
            ultimateButton.MyInteractable = !Player.UsedUltimate; // 必殺技を使っているかどうかを判断
            decisionButton.Decision // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    CardMobile = false;
                    TimeCounter.Instance.EndTimer(); // タイマーを0にする
                })
                .AddTo(this);

            await TimeCounter.Instance.CountDown(120);    // カウントタイマー起動（120s）
            CardMobile = false;
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
            if (Player.AtamakkoData.UltimateState != UltimateState.Normal) // 必殺技を指定していたら
            {
                Player.UsedUltimate = true;   // 必殺技を使用済みに
            }
            
            // 敵の行動情報を受け取る
            Enemy.CardSelect(); // CPUがカードを選択する
            Enemy.UltimateSelect();

            for (int i = 0; i < battleSlots.Length; i++)
            {
                Player.SetSettingCard(i, battleSlots[i].MyCardID);
                EnemyCard(i, Enemy.GetNowCardID(i));
            }
            
            _CurrentState.Value = GameState.Battle;
        }

        /*
         * CPUがカード選択するフェーズ
        
        private void ReceiveEnemyCard()
        {
            _cpuSettingCard = _cpu.SelectCard(_cpuHandCard);
            _enemyData.UltimateState = _cpu.SelectUltimate(_enemyData);
        }
         */

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
            
            Player.TrashCard();
            Enemy.TrashCard();
            
            Player.AtamakkoData.UltimateState = UltimateState.Normal;
            Enemy.AtamakkoData.UltimateState = UltimateState.Normal;
            
            _CurrentState.Value = GameState.Draw;
        }

        private async UniTask Battle(int slotNum)
        {
            var myCard = new CardModel(CardData.CardDataArrayList[Player.GetNowCardID(slotNum)]);
            var enemyCard = new CardModel(CardData.CardDataArrayList[Enemy.GetNowCardID(slotNum)]);
            int myInitiative = Player.GetInitiative(myCard.Initiative);
            int enemyInitiative = Enemy.GetInitiative(enemyCard.Initiative);
            await UniTask.Delay(10);
            Debug.Log("自分のポジション：" + Player.AtamakkoData.MyPosition);
            Debug.Log("相手のポジション：" + Enemy.AtamakkoData.MyPosition);

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
                    int enemyDamage = Enemy.GetDamage(enemyCard.Damage);
                    int enemyAttack = Enemy.AttackSelect(Player.AtamakkoData.MyPosition, enemyCard);

                    if (Enemy.AtamakkoData.MyPosition == playerAttack)
                    {
                        Enemy.AddDamage(myDamage);
                    }
                    if (Player.AtamakkoData.MyPosition == enemyAttack)
                    {
                        Player.AddDamage(enemyDamage);
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
                    int enemyMove = Enemy.MoveSelect(Player.AtamakkoData.MyPosition, enemyCard);
                    
                    Player.Move(playerMove);
                    Enemy.Move(enemyMove);
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
                if (Enemy.AtamakkoData.MyPosition == attackPosition)
                {
                    Enemy.AddDamage(myDamage);
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
                Player.Move(movePosition);
            }
        }

        private async UniTask EnemyTurn(CardModel card)
        {
            if (card.Kind == "攻撃")
            {
                int enemyDamage = Enemy.GetDamage(card.Damage);
                int attackPosition = Enemy.AttackSelect(Player.AtamakkoData.MyPosition, card);
                await UniTask.Delay(10);
                if (Player.AtamakkoData.MyPosition == attackPosition)
                {
                    Player.AddDamage(enemyDamage);
                }
            }

            if (card.Kind == "移動")
            {
                int movePosition = Enemy.MoveSelect(Player.AtamakkoData.MyPosition, card);
                await UniTask.Delay(10);
                Enemy.Move(movePosition);
            }
        }

        private void EnemyCard(int sID, int cID)
        {
            enemySlots[sID].CreateCard(cID);
            enemySlots[sID].FlipOver();
        }

        void CreateSlot(int cData)
        {
            var slot = Instantiate(slotPrefab, cardManager);
            slot.CreateCard(cData);
        }
    }
}
