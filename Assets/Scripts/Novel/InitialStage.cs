using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            int total = 1;
            for (int i = 0; i < Stages.Length; i++)
            {
                Stages[i].SetActive(false);
            }
            for (int i = 0; i < Stages.Length; i++)
            {
                var ChildCount = Stages[i].transform.childCount - 1;
                if (LastProgressed <= (ChildCount + total -1) && LastProgressed >= total)
                {
                    Stages[i].SetActive(true);
                    Debug.Log("ChildCount + total:" + (ChildCount + total - 1) + ":total:" + total);
                }
                total += ChildCount;
            }

            if(SceneManager.GetActiveScene().name == "StoryBoard2" && LastProgressed >=13)
            {
                for (int i = 0; i < Stages[0].transform.childCount; i++)
                {
                    Stages[0].transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}