using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class OnLoadSceneFade : MonoBehaviour
{
    [SerializeField] Image FadeImage;
    [SerializeField] TextMeshProUGUI FadeTMP;
    [SerializeField] float FadeTime;
    [SerializeField] private bool BlackStart = false;
    public static bool BackBattle = false;
    private void OnEnable()
    {
        if (check())
        {
            if (BlackStart)
            {
                FadeImage.color = new Color(0f, 0f, 0f, 1f);
                if (FadeImage != null) FadeImage.DOFade(0f, FadeTime).SetLoops(1, LoopType.Yoyo);
                if (FadeTMP != null) FadeTMP.DOFade(1f, FadeTime / 2).SetLoops(2, LoopType.Yoyo);
            }
            else
            {
                if (FadeImage != null) FadeImage.DOFade(1f, FadeTime).SetLoops(2, LoopType.Yoyo);
                if (FadeTMP != null) FadeTMP.DOFade(1f, FadeTime).SetLoops(2, LoopType.Yoyo);
            }
        }
    }

    private bool check()
    {
        if (!BackBattle || BlackStart)
        {
            BackBattle = false;
            return true;
        }
        else
        {
            BackBattle = false;
            return false;
        }
    }
}
