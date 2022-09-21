using System;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using Photon.Pun;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviourPunCallbacks, IMobile
    {
        private AtamakkoStatus _atamakkoStatus;
        [NonSerialized] public int Position;
        
        [SerializeField] private GameObject[] sSlot;
        [SerializeField] private MoveButton moveButton;
        [SerializeField] private Enemy enemy;

        private bool _moved;

        public readonly Subject<bool> MSelected = new Subject<bool>();
        public IObservable<bool> MoveSelected => MSelected;

        private void Start()
        {
            _atamakkoStatus = gameObject.GetComponent<AtamakkoStatus>();
        }

        public async UniTask CanMove(CardModel card, int initiative)
        {
            Position = _atamakkoStatus.MyPosition;
            
            if (card.Kind == "移動" && card.Initiative == initiative)
            {
                for(int i = 0; i < card.Move.Length; i++)
                {
                    if (card.Move[i] == "〇")
                    {
                        var toPosition = (i + Position) % 6;
                        var toPlayer = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[toPosition].transform);
                        toPlayer.MyPlace = toPosition;
                        toPlayer.playerMove = this;
                    }
                }
                await MSelected.ToUniTask(true);
                _moved = true;
            }
        }

        public void MovePart()
        {
            if (!_moved)
            {
                return;
            }
            Move(Position);
            photonView.RPC(nameof(EnemyMove), RpcTarget.Others, Position);
            _moved = false;
        }

        public void Move(int slotNum)
        {
            gameObject.transform.SetParent(sSlot[slotNum].transform);
            Position = slotNum;
            _atamakkoStatus.MyPosition = Position;
        }

        [PunRPC]
        public void EnemyMove(int position)
        {
            enemy.Move(position);
        }
    }
}
