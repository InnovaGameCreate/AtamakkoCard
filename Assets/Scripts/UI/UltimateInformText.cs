using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using Atamakko;
using Photon.Pun;

namespace UI
{
    public class UltimateInformText : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// アルティメットを使用すると自分側に自分の使用したアルティメットをテキストで表示
        /// 相手側にも自分の使用したアルティメットをテキストで表示
        /// </summary>
        [SerializeField] private TextMeshProUGUI _playerInformText;
        [SerializeField] private TextMeshProUGUI _enemyInformText;

        private void Start()
        {
            _enemyInformText.text = null;
            _playerInformText.text = null;
        }

        public async void setText(UltimateState UltimateType, bool isPlayer)
        {
            await Task.Delay(2000);
            string ultimateText = null;//表示用テキストの生成
            switch (UltimateType)//使用するアルティメットに対応するエフェクトを再生する。
            {
                case UltimateState.Recover:
                    ultimateText = "体力回復のアルティメットを使用しました";//体力回復アルティメットの使用エフェクト
                    break;
                case UltimateState.Attack:
                    ultimateText = "攻撃力上昇のアルティメットを使用しました";//攻撃力上昇アルティメットの使用エフェクト
                    break;
                case UltimateState.Speed:
                    ultimateText = "先制度上昇のアルティメットを使用しました";//先制度上昇アルティメットの使用エフェクト
                    break;
                default:
                    Debug.LogError("未割当のアルティメットを表示しようとしています");
                    break;
            }
            if (isPlayer) _playerInformText.text = ultimateText;
            else _enemyInformText.text = ultimateText;
            if (PlayerConfig.IsOnline)
            {
                ultimateText = "相手は" + ultimateText;
                photonView.RPC(nameof(setEnemyInformText), RpcTarget.Others, ultimateText);
            }

            await Task.Delay(3000);
            if (PlayerConfig.IsOnline)
            {
                ultimateText = "相手は" + ultimateText;
                photonView.RPC(nameof(setEnemyInformText), RpcTarget.Others, "");
            }
            if (isPlayer) _playerInformText.text = null;
            else _enemyInformText.text = null;
        }

        [PunRPC]
        private void setEnemyInformText(string ultimateText)
        {
            _enemyInformText.text = ultimateText;
        }
    }
}