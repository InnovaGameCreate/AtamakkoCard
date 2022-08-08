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
        transform.position = PlayerPosition.transform.position;//�v���C���[�̏����l
    }

    public void PlayerMove(int tileNum)
    {
        var TileName = "tile (" + tileNum + ")" ;//�^�C���̖��O������
        var TileObject = GameObject.Find(TileName);//�^�C���̃I�u�W�F�N�g���擾
        if(TileObject == null)
        {
            Debug.LogError("�w�肵���^�C�������݂��܂���");
            return;
        }
        var distance = Vector3.Distance(gameObject.transform.position, TileObject.transform.position);
        if(distance < 180)
        {
            this.transform.position = TileObject.transform.position;

            if (TileObject.GetComponent<EventCheck>().yet()) Event.Event(tileNum);//tile�̃C�x���g���s��
            TileObject.GetComponent<EventCheck>().EventUsed();//�^�C���̃C�x���g������̃A�N�V�������s��
        }
    }
}
