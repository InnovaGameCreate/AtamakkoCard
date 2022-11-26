using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Assemble
{
    public class equipmentData : MonoBehaviour
    {
        private const string SHEET_ID = "1f9xmA85qHljNzkYmeSnCOzjkew2X2Zm62UhThCvnssk";
        private const string SHEET_NAME = "�V�[�g1";

        public static List<string[]> CardDataArrayList;

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
            List<string[]> equipmentDataStringsList = new List<string[]>();
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
                    equipmentDataStringsList.Add(elements);
                }
            }

            return equipmentDataStringsList;
        }
    }
}