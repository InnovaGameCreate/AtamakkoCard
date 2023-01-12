using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ranker;
    [SerializeField] private GameObject Player;
    [SerializeField]private int PlayerArenaRank;
    void Start()
    {
        //PlayerArenaRank = PlayerConfig.ArenaRank;
        RankerPosition();
    }
    private void RankerPosition()
    {
        var _allRankerNum = ranker.Length;
        Debug.Log("_allRankerNum" + _allRankerNum);
        var allRanker = new GameObject[_allRankerNum + 1];
        int Index = 0;
        for (int i = 0; i < _allRankerNum; i++)
        {
            Debug.Log("�A���[�i����" + Index+"�F"+ ranker[i].name);
            allRanker[Index] = ranker[i];
            Index++;
            if (PlayerArenaRank == Index + 1)
            {
                Debug.Log("�A���[�i����" + Index + "�F" + Player.name);
                allRanker[Index] = Player;
                Index++;
            }
        }
        for (int i = 0; i < _allRankerNum; i++)
        {
            //Debug.Log("���בւ�" + i);
            allRanker[i].transform.SetSiblingIndex(i);
        }
    }
}
