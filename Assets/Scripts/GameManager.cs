using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Card;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardController cardPrefab;
    [SerializeField] private Transform cardManager;
    private Deck _deck1;
    private List<int> _cardList;

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        _deck1 = Resources.Load<Deck>("Deck1");

        _cardList = ShuffleDeck(_deck1.cardIDList);
        
        DrawCard();
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
            CreateCard(_cardList[0]);
            _cardList.Remove(_cardList[0]);
        }
    }
    
    void CreateCard(int cData)
    {
        var card = Instantiate(cardPrefab, cardManager);
        card.Init(CardData.CardDataArrayList[cData]);
    }
}
