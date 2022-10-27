using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        private const string SHEET_ID = "10ffaXstejMSgOXv8xDcLqIRntThBsr5R76YawNDt4uM";
        private const string SHEET_NAME = "シート1";

        [SerializeField] private CardController cardPrefab;
        [SerializeField] private Transform cardManager;
    
        private void Awake()
        {
            StartCoroutine(CreateDeck(SHEET_NAME));
        }

        IEnumerator CreateDeck(string _SHEET_NAME)
        {
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/"+SHEET_ID+"/gviz/tq?tqx=out:csv&sheet="+_SHEET_NAME);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                List<string[]> cardDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
                Deck deck1 = Resources.Load<Deck>("Deck1");

                foreach (var cardData in deck1.cardIDList)
                {
                    var card = Instantiate(cardPrefab, cardManager);
                    card.Init(cardDataArrayList[cardData]);
                }
            }
        }

        List<string[]> ConvertToArrayListFrom(string text)
        {
            List<string[]> cardDataStringsList = new List<string[]>();
            StringReader reader = new StringReader(text);
            reader.ReadLine();
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] elements = line.Split(',');
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i] == "\"\"")
                    {
                        continue;
                    }

                    elements[i] = elements[i].TrimStart('"').TrimEnd('"');
                }
                cardDataStringsList.Add(elements);
            }

            return cardDataStringsList;
        }
    }
}
