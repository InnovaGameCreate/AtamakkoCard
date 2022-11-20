using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MouseOverEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Vector3 baseScale;
    [SerializeField]
    private float time;
    [SerializeField]
    private float scale;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(baseScale * scale, time);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(baseScale, time);
    }

    void Start()
    {
        baseScale = transform.localScale;
    }

    
}
