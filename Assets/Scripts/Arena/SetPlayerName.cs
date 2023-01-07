using UnityEngine;
using TMPro;

namespace system.Arena
{
    public class SetPlayerName : MonoBehaviour
    {
        /// <summary>
        /// アリーナの自分の欄に自分の名前を表示
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI Playername;
        void Start()
        {
            Playername.text = PlayerConfig.PlayerName;
        }
    }
}
