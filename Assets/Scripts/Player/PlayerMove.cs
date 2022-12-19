using System;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using Photon.Pun;
using UI;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviourPunCallbacks, IMobile
    {
        private AtamakkoData _atamakkoData;
        [NonSerialized] public int Position;
        
        [SerializeField] private GameObject[] sSlot;
        [SerializeField] private MoveButton moveButton;
        [SerializeField] private Enemy enemy;

        private bool _moved;

        public readonly Subject<bool> MSelected = new Subject<bool>();
        public IObservable<bool> MoveSelected => MSelected;

        private void Start()
        {
            _atamakkoData = gameObject.GetComponent<AtamakkoData>();
        }

        public async UniTask CanMove(CardModel card, int initiative)
        {
            Position = _atamakkoData.MyPosition;
            for(int i = 0; i < card.Move.Length; i++)
            {
                if (card.Move[i] == "〇")
                {
                    var toPosition = (i + Position) % 6;
                    var toPlayer = Instantiate(moveButton, transform.position, Quaternion.identity, sSlot[toPosition].transform);
                    //toPlayer.MyPlace = toPosition;
                    //toPlayer.playerMove = this;
                }
            }
            await MSelected.ToUniTask(true);
            _moved = true;
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
            _atamakkoData.MyPosition = Position;
        }

        [PunRPC]
        public void EnemyMove(int position)
        {
            enemy.Move(position);
        }
    }
}
