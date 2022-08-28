using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class OnLoadSceneFaide : MonoBehaviour
{
    [SerializeField]
    Image FadeImage;
    [SerializeField]
    TextMeshProUGUI FadeTMP;
    [SerializeField]
    float FadeTime;
    [SerializeField]
    private bool BlackStart = false;
    private void OnEnable()
    {
        if(BlackStart)
        {
            FadeImage.color = new Color(0f, 0f, 0f, 1f);
            if (FadeImage != null) FadeImage.DOFade(0f, FadeTime).SetLoops(1, LoopType.Yoyo);

            if (FadeTMP != null) FadeTMP.DOFade(1f, FadeTime/2).SetLoops(2, LoopType.Yoyo);
        }
        else
        {
            if (FadeImage != null) FadeImage.DOFade(1f, FadeTime).SetLoops(2, LoopType.Yoyo);

            if (FadeTMP != null) FadeTMP.DOFade(1f, FadeTime).SetLoops(2, LoopType.Yoyo);
        }
    }
}
