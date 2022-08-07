using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMarkerMove : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveY(-40, 1f).SetLoops(-1).SetEase(Ease.InOutSine).SetRelative();
        transform.DOMoveZ(20, 1f).SetLoops(-1).SetEase(Ease.InOutSine).SetRelative();
    }
}
