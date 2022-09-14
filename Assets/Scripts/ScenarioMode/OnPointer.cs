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
                TitleText.text = "始まりの物語";
                SummaryText.text = "　全ての始まり。知らない洞窟の中で目を覚ます。\n　なぜこんな場所にいたかを思い出そうにも、何も思い出すことができない。";
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
