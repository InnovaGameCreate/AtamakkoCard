using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private TextMeshProUGUI TitleText;
    [SerializeField]
    private TextMeshProUGUI SummaryText;
    public int StoryNum;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnMouseEnter");
        switch (StoryNum)
        {
            case 1:
                TitleText.text = "�n�܂�̕���";
                SummaryText.text = "�@�S�Ă̎n�܂�B�m��Ȃ����A�̒��Ŗڂ��o�܂��B\n�@�Ȃ�����ȏꏊ�ɂ��������v���o�����ɂ��A�����v���o�����Ƃ��ł��Ȃ��B";
                break;
            default:

                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TitleText.text = "";
        SummaryText.text = "";
        Debug.Log("OnMouseExit");
    }
}
