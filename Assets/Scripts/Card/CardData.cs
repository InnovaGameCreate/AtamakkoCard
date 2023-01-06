using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Card
{
    public class CardData : MonoBehaviour
    {
        private const string SheetID = "10ffaXstejMSgOXv8xDcLqIRntThBsr5R76YawNDt4uM";
        private const string SheetName = "シート1";
    
        public static List<string[]> CardDataArrayList;

        public static IEnumerator GetData()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/"+SheetID+"/gviz/tq?tqx=out:csv&sheet="+SheetName);
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

        static List<string[]> ConvertToArrayListFrom(string text)
        {
            List<string[]> cardDataStringsList = new List<string[]>();
            StringReader reader = new StringReader(text);
            reader.ReadLine();
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    string[] elements = line.Split(',');
                    for (int i = 0; i < elements.Length; i++)
                    {
                        if (elements[i] == "\"\"")
                        {
                            continue;
                        }

                        elements[i] = elements[i].TrimStart('"').TrimEnd('"');
                        //Debug.Log(elements[0]);
                    }
                    cardDataStringsList.Add(elements);
                }
            }

            return cardDataStringsList;
        }
    }
}
