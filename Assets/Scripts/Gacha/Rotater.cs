using UnityEngine;
using DG.Tweening;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float RotateTime = 10f;
    void Start()
    {
        transform.DORotate(new Vector3(0, -360f, 0), RotateTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
}
