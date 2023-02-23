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

namespace Manager
{
    public class CPUManager : BattleManager
    {
        // 名前
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI enemyName;

        /// <summary>
        /// 待ちフェイズ
        /// </summary>
        protected override async void WaitingGame()
        {
            playerName.text = PlayerConfig.PlayerName;
            enemyName.text = enemyDeckData._enemyName;
            PhotonNetwork.OfflineMode = true;
            await TimeCounter.Instance.CountDown(3);
            _next.Value = false;
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
            var playerDeck = PlayerConfig.Deck;
            var enemyDeck = enemyDeckData.enemyDeck;
            
            decisionButton.MyInteractable = false;

            await UniTask.Delay(10);
            
            // プレイヤーの初期設定
            Enemy.SetCPU(EnemyType.EasyCPU);
            Player.Initialize(playerDeck);
            Enemy.Initialize(enemyDeck ?? playerDeck);
            Player.ShuffleDeck();
            Enemy.ShuffleDeck();
            Player.AtamakkoData.SetImage(PlayerConfig.Equipmnet.ToArray());

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
            decisionButton.Pushed // 決定ボタンが押されたとき
                .Subscribe(_ =>
                {
                    decisionButton.MyInteractable = false;
                    CardMobile.Value = false;
                    ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
                    _next.Value = true;
                })
                .AddTo(this);
            
            // ドローフェーズへ
            _CurrentState.Value = GameState.Draw;
        }

        /// <summary>
        /// ドローフェーズ
        /// </summary>
        protected override async void DrawFaze()
        {
            if (Player.CheckDeck() && Player.CheckHand()) // 自デッキにカードがないなら
            {
                Player.RefillDeck(); // デッキを補充する
            }
            if (Enemy.CheckDeck() && Enemy.CheckHand()) // 敵デッキにカードがないなら
            {
                Enemy.RefillDeck(); // デッキを補充する
            }

            await UniTask.Delay(10);
            
            Player.DebugHand();
            //Enemy.DebugHand();

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
            if (_CurrentState.Value == GameState.End) return;
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
            //Enemy.DebugHand();
            //Enemy.DebugDeck();
            foreach (var cardID in enemyList)
            {
                var card = Instantiate(cardPrefab, enemyContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }

            CardMobile.Value = true;
            settingPlace.SetActive(true);
            
            ultimateButton.MyInteractable = !Player.UsedUltimate; // 必殺技を使っているかどうかを判断

            await Next.Where(b => b).ToUniTask(true);
            _next.Value = false;
            CardMobile.Value = false;
            ultimateButton.MyInteractable = false;  // 必殺技ボタンのOFF
            settingPlace.SetActive(false);
            
            // 敵の行動情報を受け取る
            Enemy.CardSelect(); // CPUがカードを選択する
            Enemy.UltimateSelect();

            // 敵のカードを表示する
            for (int i = 0; i < battleSlots.Length; i++)
            {
                Player.SetSettingCard(battleSlots[i].MyCardID);
                EnemyCard(i, Enemy.GetNowCardID(i));
            }
            
            // 戦闘フェイズへ
            if (_CurrentState.Value == GameState.End) return;
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
                informText.setText(Player.AtamakkoData.UltimateState, true);
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
                //await AnimationManager.Instance.EnUltimateCutIn();
                AnimationManager.Instance.MyUltimateCutIn(Enemy.AtamakkoData.UltimateState);
                informText.setText(Enemy.AtamakkoData.UltimateState, false);
                switch (Enemy.AtamakkoData.UltimateState)
                {
                    case UltimateState.Recover:
                        EffectManager.Instance.InstantiateEffect(EffectType.GreenEffect, Enemy.transform);
                        SeManager.Instance.ShotSe(SeType.ultimateHeal);//必殺技使用時にSEを再生
                        break;
                    case UltimateState.Attack:
                        EffectManager.Instance.InstantiateEffect(EffectType.RedEffect, Enemy.transform);
                        SeManager.Instance.ShotSe(SeType.ultimateDamageUp);//必殺技使用時にSEを再生
                        break;
                    case UltimateState.Speed:
                        EffectManager.Instance.InstantiateEffect(EffectType.BlueEffect, Enemy.transform);
                        SeManager.Instance.ShotSe(SeType.ultimateSpeedUp);//必殺技使用時にSEを再生
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
                Player.RemoveHand(battleSlots[i].MyCardID);

                battleSlots[i].DeleteCard();
                enemySlots[i].DeleteCard();
                if (_CurrentState.Value == GameState.End) return;
                await UniTask.Delay(800);
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
            var myInitiative = Player.GetInitiative(myCard.Initiative);
            var enemyInitiative = Enemy.GetInitiative(enemyCard.Initiative);

            int playerPosition, enemyPosition;
            await UniTask.Delay(10);

            // 優先度の処理
            if (myInitiative == enemyInitiative && myCard.Kind == enemyCard.Kind)
            {
                Debug.Log("同時処理");
                playerPosition = await SelectAction(myCard, true);
                enemyPosition = await SelectAction(enemyCard, false);
                ApplyAction(myCard, playerPosition, true);
                await UniTask.Delay(800);
                ApplyAction(enemyCard, enemyPosition, false);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                
                playerPosition = await AdditionalEffect(myCard, playerPosition, true);
                enemyPosition = await AdditionalEffect(enemyCard, enemyPosition, false);
                ApplyAdditional(myCard, playerPosition, true);
                await UniTask.Delay(800);
                ApplyAdditional(enemyCard, enemyPosition, false);
                await UniTask.Delay(800);
            }
            else if (myInitiative > enemyInitiative || (myInitiative == enemyInitiative && myCard.Kind == "攻撃"))
            {
                Debug.Log("プレイヤー先攻");
                playerPosition = await SelectAction(myCard, true);
                ApplyAction(myCard, playerPosition, true);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                playerPosition = await AdditionalEffect(myCard, playerPosition, true);
                ApplyAdditional(myCard, playerPosition, true);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                
                enemyPosition = await SelectAction(enemyCard, false);
                ApplyAction(enemyCard, enemyPosition, false);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                enemyPosition = await AdditionalEffect(enemyCard, enemyPosition, false);
                ApplyAdditional(enemyCard, enemyPosition, false);
                await UniTask.Delay(800);
            }
            else if (myInitiative < enemyInitiative || (myInitiative == enemyInitiative && myCard.Kind == "移動"))
            {
                Debug.Log("プレイヤー後攻");
                enemyPosition = await SelectAction(enemyCard, false);
                ApplyAction(enemyCard, enemyPosition, false);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                enemyPosition = await AdditionalEffect(enemyCard, enemyPosition, false);
                ApplyAdditional(enemyCard, enemyPosition, false);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                
                playerPosition = await SelectAction(myCard, true);
                ApplyAction(myCard, playerPosition, true);
                await UniTask.Delay(800);
                if (_CurrentState.Value == GameState.End) return;
                playerPosition = await AdditionalEffect(myCard, playerPosition, true);
                ApplyAdditional(myCard, playerPosition, true);
                await UniTask.Delay(800);
            }

        
            await UniTask.Delay(10);
        }

        /// <summary>
        /// 選択したカードから行動を決定する。
        /// </summary>
        /// <param name="card">選択したカード</param>
        /// <param name="isPlayer">プレイヤーかどうか</param>
        /// <returns>選択したポジション</returns>
        private async UniTask<int> SelectAction(CardModel card, bool isPlayer)
        {
            var position = 0;
            if (isPlayer)
            {
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
                await Next.Where(b => b).ToUniTask(true);
                _next.Value = false;
            }
            else
            {
                position = card.Kind switch
                {
                    "攻撃" => Enemy.AttackSelect(Player.AtamakkoData.MyPosition, card),
                    "移動" => Enemy.MoveSelect(Player.AtamakkoData.MyPosition, card),
                    _ => 0
                };
                await UniTask.Delay(10);
            }
            return position;
        }

        /// <summary>
        /// 選択したカードの追加効果を処理する。
        /// </summary>
        /// <param name="card">選択したカード</param>
        /// <param name="position">自身のポジション</param>
        /// <param name="isPlayer">プレイヤーかどうか</param>
        /// <returns>選択したポジション</returns>
        private async UniTask<int> AdditionalEffect(CardModel card, int position, bool isPlayer)
        {
            if (card.Additional == "×") return position;
            if (isPlayer)
            {
                switch (card.Additional)
                {
                    case "追行動":
                        break;
                    case "再行動":
                        switch (card.Kind)
                        {
                            case "攻撃":
                                var canMove = Player.CanMove(card);
                                foreach (var t in canMove)
                                {
                                    position = t;
                                    var moveArea = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[position].transform);
                                    moveArea.MovePlace = position;

                                    moveArea.Selected
                                        .Subscribe(i =>
                                        {
                                            position = i;
                                            _next.Value = true;
                                        })
                                        .AddTo(moveArea);
                                }
                                break;
                            case "移動":
                                var canAttack = Player.CanAttack(card);
                                foreach (var t in canAttack)
                                {
                                    var attackArea = Instantiate(attackButton, stage.transform.position, Quaternion.identity, stage.transform);
                                    position = t;
                                    attackArea.transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * position);
                                    attackArea.AttackPlace = position;

                                    attackArea.Selected
                                        .Subscribe(i =>
                                        {
                                            position = i;
                                            _next.Value = true;
                                        })
                                        .AddTo(attackArea);
                                }
                                break;
                        }
                        await Next.Where(b => b).ToUniTask(true);
                        _next.Value = false;
                        break;
                }
            }
            else
            {
                switch (card.Additional)
                {
                    case "追行動":
                        break;
                    case "再行動":
                        position = card.Kind switch
                        {
                            "攻撃" => Enemy.MoveSelect(Player.AtamakkoData.MyPosition, card),
                            "移動" => Enemy.AttackSelect(Player.AtamakkoData.MyPosition, card),
                            _ => 0
                        };
                        break;
                }
            }
            await UniTask.Delay(10);
            return position;
        }
        
        /// <summary>
        /// 選択したアクションを適用する。
        /// </summary>
        /// <param name="card">選択したカード</param>
        /// <param name="position">自身のポジション</param>
        /// <param name="isPlayer">プレイヤーかどうか</param>
        private void ApplyAction(CardModel card, int position, bool isPlayer)
        {
            switch (card.Kind)
            {
                case "攻撃":
                    AttackAction(card, position, isPlayer);
                    break;
                case "移動":
                    if (isPlayer) Player.Move(position);
                    else Enemy.Move(position);
                    break;
            }
        }

        /// <summary>
        /// 追加効果を適用する。
        /// </summary>
        /// <param name="card">選択したカード</param>
        /// <param name="position">自身のポジション</param>
        /// <param name="isPlayer">プレイヤーかどうか</param>
        private void ApplyAdditional(CardModel card, int position, bool isPlayer)
        {
            if (card.Additional == "×") return;
            switch (card.Kind)
            {
                case "攻撃":
                    if (isPlayer) Player.Move(position);
                    else Enemy.Move(position);
                    break;
                case "移動":
                    AttackAction(card, position, isPlayer);
                    break;
            }
        }

        /// <summary>
        /// 攻撃アクションを行う。
        /// </summary>
        /// <param name="card">選択したカード</param>
        /// <param name="position">自身のポジション</param>
        /// <param name="isPlayer">プレイヤーかどうか</param>
        private void AttackAction(CardModel card, int position, bool isPlayer)
        {
            int damage;
            if (isPlayer)
            {
                damage = Player.GetDamage(card.Damage);
                if (Enemy.AtamakkoData.MyPosition == position)
                {
                    Enemy.AddDamage(damage);
                }
            }
            else
            {
                damage = Enemy.GetDamage(card.Damage);
                if (Player.AtamakkoData.MyPosition == position)
                {
                    Player.AddDamage(damage);
                }
            }
            
            switch (card.Effect)
            {
                case "射撃":
                    EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, sSlot[position].transform);
                    SeManager.Instance.ShotSe(SeType.AttackCard);
                    //Debug.Log("射撃エフェクトを再生:" + Enemy.gameObject.name);
                    break;
                case "斬撃":
                    EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, sSlot[position].transform);
                    SeManager.Instance.ShotSe(SeType.AttackCard);
                    //Debug.Log("斬撃エフェクトを再生:" + Enemy.gameObject.name);
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

        /// <summary>
        /// カードスロットを生成する。
        /// </summary>
        /// <param name="cData">カードID</param>
        void CreateSlot(int cData)
        {
            var slot = Instantiate(slotPrefab, cardManager);
            slot.CreateCard(cData);
        }
    }
}
