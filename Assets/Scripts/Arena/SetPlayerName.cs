using UnityEngine;
using TMPro;

namespace system.Arena
{
    public class SetPlayerName : MonoBehaviour
    {
        /// <summary>
        /// �A���[�i�̎����̗��Ɏ����̖��O��\��
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI Playername;
        void Start()
        {
            Playername.text = PlayerConfig.PlayerName;
        }
    }
}
