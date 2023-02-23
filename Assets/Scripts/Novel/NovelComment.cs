using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using TMPro;
using System.Audio;
using Cysharp.Threading.Tasks;

namespace storyMode
{
    public class NovelComment : MonoBehaviour
    {
        const string SHEET_ID = "1OBn1iwK1yuuwgfM-vpQWOdmHe6XepyghLTHrdLZ4Mto";

        private int TextNum = 0; 
        [SerializeField] private string SHEET_NAME;
        List<string[]> characterDataArrayList;
        private float textSpeed = 0.05f;//文字送り速度
        public int currentChapter;
        [SerializeField] private Image StillImage;
        [SerializeField] private Image BackGroundImage;
        StoryBoardEvent eventSystem;

        //テキスト
        [SerializeField] private TextMeshProUGUI CommentText;
        [SerializeField] private TextMeshProUGUI CharacterName;
        private bool isWriting = false;                                 //書いているかどうか
        private bool autoWriting = false;
        private bool skipWriting = false;

        public bool onAnimation = false;

        //エクセル内data
        private string capter;
        private string Type;
        private string lastCapter = null;
        private string characterName;                                   //話者の名前
        private string comment;                                         //書かれる内容
        private string diff;                                            //表情差分
        private bool readEnd = false;                                   //読み込み完了しているか
        


        [SerializeField] private Sprite[] Still;        //スチル画像
        [SerializeField] private Sprite[] BackImage;

        [System.Obsolete]
        private void OnEnable()
        {
            autoWriting = false;
            skipWriting = false;
            textSpeed = 0.05f;
            loadData();
        }

        [System.Obsolete]
        private void Start()
        {
            eventSystem = FindObjectOfType<StoryBoardEvent>().GetComponent<StoryBoardEvent>();
        }

        [System.Obsolete]
        private async void loadData()
        {
            await Method(SHEET_NAME);
            StartCoroutine(next());
        }

        public void nextText()
        {
            if (isWriting) return;
            lastCapter = capter;                                        //前回のchapter数を記録
            convertData(characterDataArrayList[TextNum]);               //各データをスプレットシートから読み取る

            if (lastCapter != capter && lastCapter != null && lastCapter == currentChapter.ToString())//会話の終了
            {
                endTalk();
                return;
            }
            else if (capter != currentChapter.ToString())//目標のchapterではなかった場合
            {
                if (characterDataArrayList.Count - 1 > TextNum) TextNum++;
                else TextNum = 0;
                nextText();
                return;
            }

            StartCoroutine(textDisplay());                              //テキストの反映
            CharacterName.text = characterName;                                  //名前

            if (characterDataArrayList.Count - 1 > TextNum) TextNum++;    //読み込んだdataの行数以上には行かない
        }

        private void endTalk()
        {
            //Debug.Log("chapterが代わりました。lastCpaterは" + lastCapter + "：capterは" + capter);
            eventSystem.endEvent(int.Parse(capter));
            PlayerConfig.lastChapter = int.Parse(capter);
            transform.parent.gameObject.SetActive(false);
        }

        [System.Obsolete]
        IEnumerator Method(string _SHEET_NAME)
        {
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + _SHEET_NAME);
            yield return request.SendWebRequest();

            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                characterDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
            }
            readEnd = true;
        }

        public void convertData(string[] _dataList)
        {
            capter = _dataList[0];
            Type = _dataList[1];
            characterName = _dataList[2];
            diff = _dataList[3];
            comment = _dataList[4];
        }
        List<string[]> ConvertToArrayListFrom(string _text)
        {
            List<string[]> characterDataArrayList = new List<string[]>();
            StringReader reader = new StringReader(_text);
            reader.ReadLine();  // 1行目はラベルなので外す
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();        // 一行ずつ読み込み
                string[] elements = line.Split(',');    // 行のセルは,で区切られる
                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i] == "\"\"" || string.IsNullOrEmpty(elements[i]))
                    {
                        continue;                       // 空白は除去
                    }
                    elements[i] = elements[i].TrimStart('"').TrimEnd('"');
                }
                characterDataArrayList.Add(elements);
            }
            return characterDataArrayList;
        }

        IEnumerator textDisplay()//テキストを一文字ずつ表示するする
        {
            switch (Type)
            {
                case "テキスト":
                    isWriting = true;
                    deleteText();
                    for (int i = 0; i <= comment.Length; i++)
                    {
                        CommentText.text = comment.Substring(0, i);
                        yield return new WaitForSeconds(textSpeed);
                    }
                    isWriting = false;

                    if (autoWriting)                         //自動送り機能がオンの時に文章が終わると次の文章を送るようにする
                    {
                        yield return new WaitForSeconds(0.5f);
                        nextText();
                    }
                    if (skipWriting)                         //スキップ機能がオンの時に文章が終わると次の文章を送るようにする
                    {
                        yield return new WaitForSeconds(0.1f);
                        nextText();
                    }
                    break;
                case "スチル":
                    showSteel(int.Parse(comment));
                    yield return new WaitForSeconds(0.1f);
                    yield return new WaitUntil(() => Input.anyKey);
                    StillImage.sprite = Still[0];
                    nextText();
                    break;
                case "背景":
                    ChangeBackGroundImage(int.Parse(comment));
                    yield return new WaitForSeconds(0.4f);
                    nextText();
                    break;
                case "SE":
                    SeManager.Instance.ShotSe((SeType)System.Enum.Parse(typeof(SeType), comment, true));
                    yield return new WaitForSeconds(0.3f);
                    nextText();
                    break;
                default:
                    Debug.LogError("スプレットシートのデータの記入ミスがあります");
                    break;
            }
        }

        public void showSteel(int num)
        {
            StillImage.sprite = Still[num];
        }
        public void ChangeBackGroundImage(int num)
        {
            BackGroundImage.sprite = BackImage[num];
        }

        IEnumerator next()
        {
            if (PlayerConfig.afterBattle)
            {
                transform.parent.gameObject.SetActive(false);
                Debug.Log("戦闘画面から戻りました");
                PlayerConfig.afterBattle = false;
            }
            else
            {
                while (true)
                {
                    yield return new WaitUntil(() => readEnd && Input.GetKey(KeyCode.Mouse0));
                    if (!onAnimation) nextText();
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        public void Auto()
        {
            autoWriting ^= true;
            if (autoWriting)
            {
                skipWriting = false;
                textSpeed = 0.05f;
                if (!isWriting) nextText();
            }
        }

        public void Skip()
        {
            skipWriting ^= true;
            if (skipWriting)
            {
                autoWriting = false;
                textSpeed = 0.01f;
                if (!isWriting) nextText();
            }
            else textSpeed = 0.05f;
        }

        public void deleteText()//テキスト内を消す
        {
            CommentText.text = "";
        }
    }

}