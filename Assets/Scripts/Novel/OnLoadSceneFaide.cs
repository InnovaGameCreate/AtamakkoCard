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
    private void OnEnable()
    {
        if (FadeImage != null)FadeImage.DOFade(1f, FadeTime).SetLoops(2, LoopType.Yoyo);

        if (FadeTMP != null) FadeTMP.DOFade(1f, FadeTime).SetLoops(2, LoopType.Yoyo);
    }
}
