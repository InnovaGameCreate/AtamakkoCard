using UnityEngine;
using TMPro;

namespace system
{
    public class PlayerName : MonoBehaviour
    {
        [SerializeField]
        private new TMP_InputField name;

        [SerializeField] private TMP_Text namePlate;
        [SerializeField] private TMP_Text nameProfile;
        void Start()
        {
            if (PlayerConfig.PlayerName != "Player")
            {
                gameObject.SetActive(false);
            }
        }

        public void OnClick()//click���ꂽ�疼�O��o�^���ăE�B���h�E�����B
        {
            PlayerConfig.PlayerName = name.text;
            PlayerConfig.SetData();
            namePlate.text = name.text;
            nameProfile.text = name.text;
            gameObject.SetActive(false);
        }
    }
}