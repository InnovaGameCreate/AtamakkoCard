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
        Debug.Log("PastSceneが" + PastScene);
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        CurrentScene = SceneManager.GetActiveScene().name;
    }
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log(nextScene.name);
        CurrentScene = nextScene.name;
        Debug.Log("PastSceneが" + PastScene);
        if (CurrentScene != PastScene)
        {
            Debug.Log("PastSceneが" + PastScene + "から" + CurrentScene + "に更新されました");
            PastScene = CurrentScene;
            switch (PastScene)
            {
                case "StoryBoard1":
                case "StoryBoard2":
                case "StoryBoardSelect":
                case "StoryBoardBlue1":
                case "StoryBoardBlue2":
                case "StoryBoardBlue3":
                case "StoryBoardEnd1":
                case "StoryBoardEnd2":
                case "StoryBoardEnd3":
                case "StoryBoardRed1":
                case "StoryBoardRed2":
                case "StoryBoardRed3":
                case "StoryBoardWhite1":
                case "StoryBoardWhite2":
                case "StoryBoardWhite3":
                    PlayerConfig.LastPlayStory = PastScene;
                    break;
                default:
                    break;
            }
        }
    }
    public void BackScene()
    {
        SceneManager.LoadScene(PastScene);
    }
}
