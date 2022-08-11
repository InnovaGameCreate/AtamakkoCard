using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using TMPro;

public class NovelComment : MonoBehaviour
{
    const string SHEET_ID = "1OBn1iwK1yuuwgfM-vpQWOdmHe6XepyghLTHrdLZ4Mto";
    const string SHEET_NAME = "�e�X�g�V�[�g";

    private int TextNum = 0;
    [SerializeField]
    private string seatName;
    List<string[]> characterDataArrayList;
    private float textSpeed = 0.05f;//�������葬�x
    public int currentChapter;

    //�e�L�X�g
    [SerializeField]
    private TextMeshProUGUI CommentText;
    [SerializeField]
    private TextMeshProUGUI CharacterName;
    private bool isWriting = false;                                 //�����Ă��邩�ǂ���
    private bool autoWriting = false;
    private bool skipWriting = false;

    //�G�N�Z����data
    private string capter;
    private string Type;
    private string lastCapter = null;
    private string characterName;                                            //�b�҂̖��O
    private string comment;                                         //���������e
    private string diff;                                            //�\���
    private bool readEnd = false;                                   //�ǂݍ��݊������Ă��邩


    //�����o���n
    [SerializeField]
    private Sprite[] Steel;

    //�X�`���摜

    [System.Obsolete]
    private void Awake()
    {
        StartCoroutine(Method(SHEET_NAME));
    }
    private void OnEnable()
    {
        autoWriting = false;
        skipWriting = false;
    }
    private void Start()
    {
        StartCoroutine(startText());
    }

    public void nextText()
    {
        if (isWriting) return;
        lastCapter = capter;                                        //�O���chapter�����L�^
        convertData(characterDataArrayList[TextNum]);               //�e�f�[�^���X�v���b�g�V�[�g����ǂݎ��

        if (lastCapter != capter && lastCapter != null && lastCapter == currentChapter.ToString())//��b�̏I��
        {
            endTalk();
            return;
        }
        else if (capter != currentChapter.ToString())//�ڕW��chapter�ł͂Ȃ������ꍇ
        {
            nextText();
        }

        StartCoroutine(textDisplay());                              //�e�L�X�g�̔��f
        CharacterName.text = characterName;                                  //���O

        /*
        switch (Speechbubble)
        {
            case "Default":
                SpeechbubbleImage.sprite = image[0];
                break;
            case "Think":
                SpeechbubbleImage.sprite = image[1];
                break;
            case "Uni":
                SpeechbubbleImage.sprite = image[2];
                break;
            default:
                break;
        }
        */


        if (characterDataArrayList.Count - 1 > TextNum) TextNum++;    //�ǂݍ���data�̍s���ȏ�ɂ͍s���Ȃ�
    }

    public void startTalk()
    {
        nextText();
    }

    private void endTalk()
    {
        Debug.Log("chapter������܂����BlastCpater��" + lastCapter + "�Fcapter��" + capter);
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
        reader.ReadLine();  // 1�s�ڂ̓��x���Ȃ̂ŊO��
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();        // ��s���ǂݍ���
            string[] elements = line.Split(',');    // �s�̃Z����,�ŋ�؂���
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;                       // �󔒂͏���
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            characterDataArrayList.Add(elements);
        }
        return characterDataArrayList;
    }

    IEnumerator textDisplay()//�e�L�X�g���ꕶ�����\�����邷��
    {
        if(Type == "�e�L�X�g")
        {

            isWriting = true;
            CommentText.text = "";
            //SeManager.Instance.ShotSe(SeType.talk);
            for (int i = 0; i <= comment.Length; i++)
            {
                CommentText.text = comment.Substring(0, i);
                yield return new WaitForSeconds(textSpeed);
            }
            //SeManager.Instance.stopSe();
            isWriting = false;

            if(autoWriting)                         //��������@�\���I���̎��ɕ��͂��I���Ǝ��̕��͂𑗂�悤�ɂ���
            {
                yield return new WaitForSeconds(0.5f);
                nextText();
            }
            if (skipWriting)                         //�X�L�b�v�@�\���I���̎��ɕ��͂��I���Ǝ��̕��͂𑗂�悤�ɂ���
            {
                yield return new WaitForSeconds(0.1f);
                nextText();
            }
        }
        else if(Type == "�X�`��")
        {
            showSteel();
            yield return new WaitUntil(() => Input.anyKey);
        }
    }

    IEnumerator startText()
    {
        yield return new WaitUntil(() => readEnd);
        yield return new WaitForSeconds(0.5f);
        startTalk();
    }

    public void showSteel()
    {

    }

    public void Auto()
    {
        if (autoWriting == false)
        {
            skipWriting = false;
            autoWriting = true;
            textSpeed = 0.05f;
            if (isWriting == false)
            {
                nextText();
            }
        }
        else
        {
            autoWriting = false;
        }
    }

    public void Skip()
    {
        if (skipWriting == false)
        {
            skipWriting = true;
            autoWriting = false;
            textSpeed = 0.01f;
            if (isWriting == false)
            {
                nextText();
            }
        }
        else
        {
            skipWriting = false;
            textSpeed = 0.05f;
        }
    }
}

