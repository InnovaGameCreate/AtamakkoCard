using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardPlayerMove : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPosition;
    private GameObject GameManager;
    StoryBoardEvent Event;
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        Event = GameManager.GetComponent<StoryBoardEvent>();
        transform.position = PlayerPosition.transform.position;//プレイヤーの初期値
    }

    public void PlayerMove(int tileNum)
    {
        var TileName = "tile (" + tileNum + ")" ;//タイルの名前を検索
        var TileObject = GameObject.Find(TileName);//タイルのオブジェクトを取得
        if(TileObject == null)
        {
            Debug.LogError("指定したタイルが存在しません");
            return;
        }
        var distance = Vector3.Distance(gameObject.transform.position, TileObject.transform.position);
        if(distance < 180)
        {
            this.transform.position = TileObject.transform.position;

            if (TileObject.GetComponent<EventCheck>().yet()) Event.Event(tileNum);//tileのイベントを行う
            TileObject.GetComponent<EventCheck>().EventUsed();//タイルのイベント処理後のアクションを行う
        }
    }
}
