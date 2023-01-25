using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ranker;
    [SerializeField] private GameObject Player;
    [SerializeField] ArenaText Texts;
    private int PlayerArenaRank;
    void Start()
    {
        PlayerArenaRank = PlayerConfig.ArenaRank;
        RankerPosition();
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < (PlayerArenaRank - 2); i++)
        {
            ranker[i].GetComponent<Button>().interactable = false;
        }
    }
    private void RankerPosition()
    {
        var _allRankerNum = ranker.Length;
        Debug.Log("_allRankerNum" + _allRankerNum);
        var allRanker = new GameObject[_allRankerNum + 1];
        int Index = 0;
        for (int i = 0; i < _allRankerNum; i++)
        {
            Debug.Log("アリーナ順位" + Index+"："+ ranker[i].name);
            allRanker[Index] = ranker[i];
            Index++;
            if (PlayerArenaRank == Index + 1)
            {
                Debug.Log("アリーナ順位" + Index + "：" + Player.name);
                allRanker[Index] = Player;
                Index++;
            }
        }
        for (int i = 0; i < _allRankerNum; i++)
        {
            //Debug.Log("並べ替え" + i);
            allRanker[i].transform.SetSiblingIndex(i);
        }
    }

    public void InitData(int num)
    {
        Texts.SetText(num);
    }
}
