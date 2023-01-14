using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assemble;

public class RotateCards : MonoBehaviour
{
    [SerializeField] private equipmentView[] Card;
    [SerializeField] private int CardLength;
    [SerializeField] private float _changeCardTime;
    private int[] CardIds;
    
    void Start()
    {
        CardIds = new int[CardLength];
        for (int i = 0; i < CardLength; i++)
        {
            CardIds[i] = -1;
        }
        StartCoroutine(SetCard());
    }

    IEnumerator SetCard()
    {
        while (true)
        {
            createInts();
            SetCardData(-1, 0);
            if (!duplication()) break;
        }
        while (true)
        {
            for (int i = 0; i < CardLength; i++)
            {

                while (true)
                {
                    CardIds[i] = Random.Range(0, equipmentData.CardDataArrayList.Count);
                    if (!duplication()) break;
                }
                SetCardData(i, CardIds[i]);
                yield return new WaitForSeconds(_changeCardTime);
            }
        }
    }
    private void SetCardData(int CardNum,int CardID)
    {
        if(CardNum == -1)//‘S‘Ì‚És‚¤
        {
            for (int i = 0; i < CardLength; i++)
            {
                
                Card[i].Show(new equipmentModel(equipmentData.CardDataArrayList[CardIds[i]]));
                Card[i].gameObject.transform.parent.transform.SetAsLastSibling();
            }
        }
        else
        {
            Card[CardNum].Show(new equipmentModel(equipmentData.CardDataArrayList[CardID]));
            Card[CardNum].gameObject.transform.parent.transform.SetAsLastSibling();
        }
    }
    private void createInts()
    {
        for (int i = 0; i < CardLength; i++)
        {
            CardIds[i] = Random.Range(0, equipmentData.CardDataArrayList.Count);
        }
    }

    private bool duplication()
    {
        /*
        for (int i = 0; i < CardLength; i++)
        {
            for (int j = 0; i < Card.Length; j++)
            {
                if (CardIds[i] == CardIds[j]) return true;
            }
        }*/
        return false;
    }
}
