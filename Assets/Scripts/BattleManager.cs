using System;
using System.Collections;
using System.Collections.Generic;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using UniRx;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum State
    {
        Init,
        Draw,
        Select,
        Battle
    }
    public ReactiveProperty<State> gameState = new ReactiveProperty<State>(State.Init);

    [SerializeField] private CardSlot slotPrefab;
    [SerializeField] private Transform cardManager;
    [SerializeField] private DecisionButton _decisionButton;
    private Deck _deck1;
    private List<int> _cardList;

    public static BattleManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameState
            .Where(x => x == State.Init)
            .Subscribe(_ => StartGame())
            .AddTo(this);

        gameState
            .Where(x => x == State.Draw)
            .Subscribe(_ => DrawFaze())
            .AddTo(this);

        gameState
            .Where(x => x == State.Select)
            .Subscribe(_ => SelectFaze())
            .AddTo(this);

        gameState
            .Where(x => x == State.Battle)
            .Subscribe(_ => BattleFaze())
            .AddTo(this);
    }

    private async UniTask StartGame()
    {
        await StartCoroutine(CardData.GetData());
        _deck1 = Resources.Load<Deck>("Deck1");

        _cardList = new List<int>(_deck1.cardIDList);
        _cardList = ShuffleDeck(_cardList);

        gameState.Value = State.Draw;
    }

    private void DrawFaze()
    {
        if (_cardList.Count <= 0)
        {
            _cardList = new List<int>(_deck1.cardIDList);;
            _cardList = ShuffleDeck(_cardList);
        }
        
        DrawCard();

        gameState.Value = State.Select;
    }

    private void SelectFaze()
    {
        Debug.Log("SelectFaze");
        _decisionButton.Decision
            .Subscribe(_ =>
            {
                _decisionButton.MyInteractable = false;
        
                gameState.Value = State.Battle;
            })
            .AddTo(this);
    }
    
    private void BattleFaze()
    {
        Debug.Log("BattleFaze");
    }

    private List<int> ShuffleDeck(List<int> idList)
    {
        System.Random random = new System.Random((int) DateTime.Now.Ticks);
        for (int i = 0; i < idList.Count; i++)
        {
            int index = i + (int) (random.NextDouble() * (idList.Count - i));
            (idList[index], idList[i]) = (idList[i], idList[index]);
        }
        
        return idList;
    }

    void DrawCard()
    {
        for (int i = 0; i < 6; i++)
        {
            CreateSlot(_cardList[0]);
            _cardList.Remove(_cardList[0]);
        }
    }
    
    void CreateSlot(int cData)
    {
        var slot = Instantiate(slotPrefab, cardManager);
        slot.CreateCard(cData);
    }
}
