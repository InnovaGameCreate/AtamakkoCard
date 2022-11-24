using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageSelect : MonoBehaviour
{

    /// <summary>
    /// シナリオで最後のマスに行った後次の複数ステージへ進むかどうか
    /// </summary>

    [SerializeField]
    private SceneObject[] nextScenes;

    public void goNestStage(int i)
    {

        SceneManager.LoadScene(nextScenes[i]);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
