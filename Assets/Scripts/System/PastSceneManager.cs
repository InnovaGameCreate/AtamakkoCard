using UnityEngine;
using UnityEngine.SceneManagement;

public class PastSceneManager : MonoBehaviour
{
    public static PastSceneManager Instance { get; private set; }
    public string CurrentScene;
    private static string PastScene = "Title";
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
        //PastScene = SceneManager.GetActiveScene().name;
        Debug.Log("PastSceneÇ™" + PastScene);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        CurrentScene = SceneManager.GetActiveScene().name;
    }
    void OnSceneUnloaded(Scene scene)
    {
        CurrentScene = scene.name;
        Debug.Log("PastSceneÇ™" + PastScene);
        if (CurrentScene != PastScene)
        {
            Debug.Log("PastSceneÇ™" + PastScene + "Ç©ÇÁ" + CurrentScene + "Ç…çXêVÇ≥ÇÍÇ‹ÇµÇΩ");
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

    public string getPastScene()
    {
        return PastScene;
    }
}
