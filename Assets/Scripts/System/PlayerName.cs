using UnityEngine;
using TMPro;

namespace system
{
    public class PlayerName : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField name;
        void Start()
        {
            if (PlayerConfig.IsTutorial != 0)
            {
                //gameObject.SetActive(false);
            }
        }

        public void OnClick()//click���ꂽ�疼�O��o�^���ăE�B���h�E�����B
        {
            PlayerConfig.PlayerName = name.text;
            PlayerConfig.SetData();
            gameObject.SetActive(false);
        }
    }
}