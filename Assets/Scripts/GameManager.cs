using System;
using System.Collections;
using System.Collections.Generic;
using Card;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CardSlot slotPrefab;
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

        _cardList = new List<int>(_deck1.cardIDList);
        _cardList = ShuffleDeck(_cardList);
        
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
