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
        Debug.Log("PastScene‚ª" + PastScene);
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        CurrentScene = SceneManager.GetActiveScene().name;
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log(nextScene.name);
        CurrentScene = nextScene.name;
        Debug.Log("PastScene‚ª" + PastScene);
        if (CurrentScene != PastScene)
        {
            Debug.Log("PastScene‚ª" + PastScene + "‚©‚ç" + CurrentScene + "‚ÉXV‚³‚ê‚Ü‚µ‚½");
            PastScene = CurrentScene;
        }
    }
    public void BackScene()
    {
        SceneManager.LoadScene(PastScene);
    }
}
