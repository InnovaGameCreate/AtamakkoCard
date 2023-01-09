using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Arena
{
    public class enemyDeckData : MonoBehaviour
    {

        public static List<int> enemyDeck = new List<int>();//敵のデッキのデータ
        public static string _enemyName;
        public static int _enemyID;

        private const string SHEET_ID = "1laAr4f3RgQRTD1ySSnLHooyNnRByMtzh-oqSSYV9LtM";
        private const string SHEET_NAME = "シート1";

        public static List<string[]> CardDataArrayList;
        public static void setDeckData(int enemyID)
        {
            var Model = new enmyDeckModel(CardDataArrayList[enemyID]);
            _enemyName = Model.characterName;
            _enemyID = Model.ID;
            enemyDeck.Clear();
            for (int i = 0; i < 12; i++)
            {
                enemyDeck.Add(Model.card[i]);
            }
        }


        public static IEnumerator GetData()
        {
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + SHEET_NAME);
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
                    }
                    cardDataStringsList.Add(elements);
                }
            }

            return cardDataStringsList;
        }
    }
}