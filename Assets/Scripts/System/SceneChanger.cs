using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private SceneObject nextScene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
