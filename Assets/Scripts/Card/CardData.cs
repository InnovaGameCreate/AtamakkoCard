using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Card
{
    public class CardData : MonoBehaviour
    {
        private const string SHEET_ID = "10ffaXstejMSgOXv8xDcLqIRntThBsr5R76YawNDt4uM";
        private const string SHEET_NAME = "シート1";
    
        public static List<string[]> CardDataArrayList;

        private void Awake()
        {
            StartCoroutine(GetData(SHEET_NAME));
        }

        IEnumerator GetData(string _SHEET_NAME)
        {
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/"+SHEET_ID+"/gviz/tq?tqx=out:csv&sheet="+_SHEET_NAME);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                CardDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
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
