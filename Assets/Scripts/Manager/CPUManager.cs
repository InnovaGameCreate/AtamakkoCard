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
            Player.Initialize(playerDeck);
            Enemy.Initialize(enemyDeck ?? playerDeck);
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
            
            // ドローフェーズへ
            _CurrentState.Value = GameState.Draw;
        }

        /// <summary>
        /// ドローフェーズ
        /// </summary>
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
            //Enemy.DebugHand();
            Enemy.DebugDeck();
            foreach (var cardID in enemyList)
            {
                var card = Instantiate(cardPrefab, enemyContent.transform);
                card.GetComponent<CardController>().Init(cardID);
            }

            CardMobile = true;
            settingPlace.SetActive(true);
            
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
            await UniTask.Delay(10);

            // 優先度の処理
            if (myInitiative == enemyInitiative && myCard.Kind == enemyCard.Kind)
            {
                switch (myCard.Kind)
                {
                    case "攻撃":
                    {
                        var canAttack = Player.CanAttack(myCard);
                        var playerAttack = 0;
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
                    
                        var myDamage = Player.GetDamage(myCard.Damage);
                        var enemyDamage = Enemy.GetDamage(enemyCard.Damage);
                        var enemyAttack = Enemy.AttackSelect(Player.AtamakkoData.MyPosition, enemyCard);

                        if (Enemy.AtamakkoData.MyPosition == playerAttack)
                        {
                            Enemy.AddDamage(myDamage);
                            switch (myCard.Effect)
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
                                default:
                                    break;
                            }
                        }
                        switch (myCard.Additional)
                        {
                            case "〇":
                                Player.Move(playerAttack);
                                SeManager.Instance.ShotSe(SeType.MoveCard);
                                break;
                            case "◎":
                                var canMove = Player.CanMove(myCard);
                                var playerMove = 0;
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
                                Player.Move(playerMove);
                                SeManager.Instance.ShotSe(SeType.MoveCard);
                                break;
                        }

                        if (Player.AtamakkoData.MyPosition == enemyAttack)
                        {
                            Player.AddDamage(enemyDamage);
                            switch (enemyCard.Effect)
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
                                default:
                                    break;
                            }
                        }
                        switch (enemyCard.Additional)
                        {
                            case "〇":
                                Enemy.Move(enemyAttack);
                                SeManager.Instance.ShotSe(SeType.MoveCard);
                                break;
                            case "◎":
                                var enemyMove = Enemy.MoveSelect(Player.AtamakkoData.MyPosition, enemyCard);
                                Enemy.Move(enemyMove);
                                SeManager.Instance.ShotSe(SeType.MoveCard);
                                break;
                        }
                        break;
                    }
                    case "移動":
                    {
                        var canMove = Player.CanMove(myCard);
                        var playerMove = 0;
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
                        var enemyMove = Enemy.MoveSelect(Player.AtamakkoData.MyPosition, enemyCard);
                    
                        Player.Move(playerMove);
                        SeManager.Instance.ShotSe(SeType.MoveCard);
                        
                        int myDamage;
                        switch (myCard.Additional)
                        {
                            case "〇" when Enemy.AtamakkoData.MyPosition == playerMove:
                            {
                                myDamage = Player.GetDamage(myCard.Damage);
                                Enemy.AddDamage(myDamage);
                                switch (myCard.Effect)
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
                                    default:
                                        break;
                                }
                                break;
                            }
                            case "◎":
                                var canAttack = Player.CanAttack(myCard);
                                var playerAttack = 0;
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
                                myDamage = Player.GetDamage(myCard.Damage);

                                if (Enemy.AtamakkoData.MyPosition == playerAttack)
                                {
                                    Enemy.AddDamage(myDamage);
                                    switch (myCard.Effect)
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
                                        default:
                                            break;
                                    }
                                }
                                break;
                        }
                    
                        Enemy.Move(enemyMove);
                        SeManager.Instance.ShotSe(SeType.MoveCard);
                        
                        int enemyDamage;
                        switch (enemyCard.Additional)
                        {
                            case "〇" when Player.AtamakkoData.MyPosition == enemyMove:
                            {
                                enemyDamage = Enemy.GetDamage(enemyCard.Damage);
                                Player.AddDamage(enemyDamage);
                                switch (enemyCard.Effect)
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
                                    default:
                                        break;
                                }
                                break;
                            }
                            case "◎":
                                enemyDamage = Enemy.GetDamage(enemyCard.Damage);
                                var enemyAttack = Enemy.AttackSelect(Player.AtamakkoData.MyPosition, enemyCard);
                                if (Player.AtamakkoData.MyPosition == enemyAttack)
                                {
                                    Player.AddDamage(enemyDamage);
                                    switch (enemyCard.Effect)
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
                                        default:
                                            break;
                                    }
                                }
                                break;
                        }

                        break;
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
        /// プレイヤーの行動を処理する
        /// </summary>
        /// <param name="card">使用したカード</param>
        private async UniTask PlayerTurn(CardModel card)
        {
            switch (card.Kind)
            {
                case "攻撃":
                {
                    var canAttack = Player.CanAttack(card);
                    var attackPosition = 0;
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
                    var myDamage = Player.GetDamage(card.Damage);
                    if (Enemy.AtamakkoData.MyPosition == attackPosition)
                    {
                        Enemy.AddDamage(myDamage);
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
                            default:
                                break;
                        }
                    }
                    switch (card.Additional)
                    {
                        case "〇":
                            Player.Move(attackPosition);
                            SeManager.Instance.ShotSe(SeType.MoveCard);
                            break;
                        case "◎":
                            var canMove = Player.CanMove(card);
                            var movePosition = 0;
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
                            SeManager.Instance.ShotSe(SeType.MoveCard);
                            break;
                    }
                    break;

                }
                case "移動":
                {
                    var canMove = Player.CanMove(card);
                    var movePosition = 0;
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
                    SeManager.Instance.ShotSe(SeType.MoveCard);
                    
                    int myDamage;
                    switch (card.Additional)
                    {
                        case "〇" when Enemy.AtamakkoData.MyPosition == movePosition:
                        {
                            myDamage = Player.GetDamage(card.Damage);
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
                            break;
                        }
                        case "◎":
                            var canAttack = Player.CanAttack(card);
                            var attackPosition = 0;
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
                            myDamage = Player.GetDamage(card.Damage);
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
                            break;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 敵の行動を処理する。
        /// </summary>
        /// <param name="card">使用したカード</param>
        private async UniTask EnemyTurn(CardModel card)
        {
            switch (card.Kind)
            {
                case "攻撃":
                {
                    var enemyDamage = Enemy.GetDamage(card.Damage);
                    var attackPosition = Enemy.AttackSelect(Player.AtamakkoData.MyPosition, card);
                    await UniTask.Delay(10);
                    if (Player.AtamakkoData.MyPosition == attackPosition)
                    {
                        Player.AddDamage(enemyDamage);
                        switch (card.Effect)
                        {
                            case "射撃":
                                EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Player.transform);
                                SeManager.Instance.ShotSe(SeType.AttackCard);
                                Debug.Log("射撃エフェクトを再生:" + Enemy.gameObject.name);
                                break;
                            case "斬撃":
                                EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Player.transform);
                                SeManager.Instance.ShotSe(SeType.AttackCard);
                                Debug.Log("斬撃エフェクトを再生:" + Enemy.gameObject.name);
                                break;
                            default:
                                break;
                        }
                    }
                    switch (card.Additional)
                    {
                        case "〇":
                            Enemy.Move(attackPosition);
                            SeManager.Instance.ShotSe(SeType.MoveCard);
                            break;
                        case "◎":
                            var movePosition = Enemy.MoveSelect(Player.AtamakkoData.MyPosition, card);
                            await UniTask.Delay(10);
                            Enemy.Move(movePosition);
                            SeManager.Instance.ShotSe(SeType.MoveCard);
                            break;
                    }
                    break;
                }
                case "移動":
                {
                    var movePosition = Enemy.MoveSelect(Player.AtamakkoData.MyPosition, card);
                    await UniTask.Delay(10);
                    Enemy.Move(movePosition);
                    SeManager.Instance.ShotSe(SeType.MoveCard);
                    
                    int enemyDamage;
                    switch (card.Additional)
                    {
                        case "〇" when Player.AtamakkoData.MyPosition == movePosition:
                        {
                            enemyDamage = Enemy.GetDamage(card.Damage);
                            Player.AddDamage(enemyDamage);
                            switch (card.Effect)
                            {
                                case "射撃":
                                    EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Player.transform);
                                    SeManager.Instance.ShotSe(SeType.AttackCard);
                                    Debug.Log("射撃エフェクトを再生:" + Enemy.gameObject.name);
                                    break;
                                case "斬撃":
                                    EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Player.transform);
                                    SeManager.Instance.ShotSe(SeType.AttackCard);
                                    Debug.Log("斬撃エフェクトを再生:" + Enemy.gameObject.name);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                        case "◎":
                            enemyDamage = Enemy.GetDamage(card.Damage);
                            var attackPosition = Enemy.AttackSelect(Player.AtamakkoData.MyPosition, card);
                            await UniTask.Delay(10);
                            if (Player.AtamakkoData.MyPosition == attackPosition)
                            {
                                Player.AddDamage(enemyDamage);
                                switch (card.Effect)
                                {
                                    case "射撃":
                                        EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffet, Player.transform);
                                        SeManager.Instance.ShotSe(SeType.AttackCard);
                                        Debug.Log("射撃エフェクトを再生:" + Enemy.gameObject.name);
                                        break;
                                    case "斬撃":
                                        EffectManager.Instance.InstantiateEffect(EffectType.slashAttackEffet, Player.transform);
                                        SeManager.Instance.ShotSe(SeType.AttackCard);
                                        Debug.Log("斬撃エフェクトを再生:" + Enemy.gameObject.name);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                    }
                    break;
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
    }
}
