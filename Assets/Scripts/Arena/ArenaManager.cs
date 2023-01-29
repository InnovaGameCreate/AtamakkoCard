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
        Debug.Log("解放されるランクは" + (PlayerArenaRank - 2));
        for (int i = 0; i < (PlayerArenaRank - 2); i++)
        {
            ranker[i].GetComponent<Button>().interactable = false;
        }
    }
    private void RankerPosition()
    {
        int playerRankData = PlayerArenaRank - 2;
        for (int i = 0; i < 20; i++)
        {
            ranker[i].transform.SetAsLastSibling();
            if (i == playerRankData)//現在の位置がプレイヤーのランクと同じかどうか
            {
                Debug.Log("プレイヤーのランクは" + (playerRankData) +"現在の位置は"+i);
                Player.transform.SetAsLastSibling();
            }
        }
    }

    public void InitData(int num)
    {
        Texts.SetText(num);
    }
}
