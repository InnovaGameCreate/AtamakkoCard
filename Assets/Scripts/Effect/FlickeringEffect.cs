using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlickeringEffect : MonoBehaviour
{
    [SerializeField] private Image _targetImage;
    [SerializeField] private float EffectTime;
    [SerializeField] private bool _loop;
    void Start()
    {
        int i = 1;
        if (_loop) i = -1;
        _targetImage.DOFade(0, EffectTime)
            .SetLoops(i,LoopType.Yoyo);

    }

}
