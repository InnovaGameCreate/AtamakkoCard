using UnityEngine;
using UnityEngine.SceneManagement;

namespace storyMode
{
    public class InitialStage : MonoBehaviour
    {
        [SerializeField] private GameObject[] Stages;
        ProgressRecorder Recoder;
        void Awake()
        {
            Recoder = GetComponent<ProgressRecorder>();
            InitialActive();
        }

        private void InitialActive()
        {
            var LastProgressed = Recoder.GetPlayerLastProgressed;
            int total = 0;
            for (int i = 0; i < Stages.Length; i++)
            {
                Stages[i].SetActive(false);
            }
            for (int i = 0; i < Stages.Length; i++)
            {
                var ChildCount = Stages[i].transform.childCount;
                if (LastProgressed <= (ChildCount + total) && LastProgressed >= total)
                {
                    Stages[i].SetActive(true);
                    break;
                }
                total += ChildCount;
            }

            //隠しタイルがある特別なステージ用の処理
            if(SceneManager.GetActiveScene().name == "StoryBoard2" && LastProgressed >=13)
            {
                for (int i = 10; i < Stages[0].transform.childCount; i++)
                {
                    Stages[0].transform.GetChild(i).gameObject.SetActive(true);
                }
                for (int i = 10; i < LastProgressed; i++)
                {
                    Stages[0].transform.GetChild(i).gameObject.GetComponent<EventCheck>().Used();
                }
            }
            else if (SceneManager.GetActiveScene().name == "StoryBoardBlue2" && LastProgressed >= 13 && LastProgressed <= 15)
            {
                for (int i = 6; i < Stages[1].transform.childCount; i++)
                {
                    Stages[1].transform.GetChild(i).gameObject.SetActive(true);
                }
                for (int i = 6; i < LastProgressed; i++)
                {
                    Stages[1].transform.GetChild(i).gameObject.GetComponent<EventCheck>().Used();
                }
            }
        }
    }
}