using UnityEngine;
using UnityEngine.SceneManagement;

public class PastSceneManager : MonoBehaviour
{
    public  static PastSceneManager Instance { get; private set; }
    private string CurrentScene;
    private static string PastScene;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PastScene = SceneManager.GetActiveScene().name;
        Debug.Log("PastScene��" + PastScene);
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        CurrentScene = SceneManager.GetActiveScene().name;
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log(nextScene.name);
        CurrentScene = nextScene.name;
        Debug.Log("PastScene��" + PastScene);
        if (CurrentScene != PastScene)
        {
            Debug.Log("PastScene��" + PastScene + "����" + CurrentScene + "�ɍX�V����܂���");
            PastScene = CurrentScene;
        }
    }
    public void BackScene()
    {
        SceneManager.LoadScene(PastScene);
    }
}
