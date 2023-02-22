using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class InstantTMP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;


    public async void Init(string text,float FadeInTime,float FadeOutTime)
    {
        Text.text = text;
        Text.DOColor(new Color(0.6f, 0.6f, 0.6f, 1), FadeInTime);
        Text.DOColor(new Color(0.6f, 0.6f, 0.6f, 0), FadeOutTime).SetDelay(FadeInTime);
        float DelayTime = 1000 * (FadeInTime + FadeOutTime);
        await UniTask.Delay((int)DelayTime);
        Destroy(gameObject);
    }
}
