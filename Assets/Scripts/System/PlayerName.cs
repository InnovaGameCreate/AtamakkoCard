using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField name;
    void Start()
    {
        if(PlayerConfig.IsTutorial != 0)
        {
            //gameObject.SetActive(false);
        }
    }

    public void OnClick()//clickされたら名前を登録してウィンドウを閉じる。
    {
        PlayerConfig.PlayerName = name.text;
        PlayerConfig.SetData();
        gameObject.SetActive(false);
    }
}
