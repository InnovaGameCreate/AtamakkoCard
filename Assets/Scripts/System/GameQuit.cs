using UnityEngine;

namespace System
{
    public class GameQuit : MonoBehaviour
    {
        //�Q�[���I��
        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
