using System;
using Card;
using Cysharp.Threading.Tasks;
using Field;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        private AtamakkoStatus _atamakkoStatus;
        [NonSerialized] public int Position;
        
        [SerializeField] private GameObject[] sSlot;
        [SerializeField] private MoveButton moveButton;

        public readonly Subject<bool> MSelected = new Subject<bool>();
        public IObservable<bool> MoveSelected => MSelected;

        private void Start()
        {
            _atamakkoStatus = gameObject.GetComponent<AtamakkoStatus>();
        }

        public async UniTask CanMove(CardModel card)
        {
            Position = _atamakkoStatus.MyPosition;
            
            if (card.Kind == "移動")
            {
                for(int i = 0; i < card.Move.Length; i++)
                {
                    if (card.Move[i] == "〇")
                    {
                        var toPosition = (i + Position) % 6;
                        var toPlayer = Instantiate(moveButton, transform.position, Quaternion.identity,
                            sSlot[toPosition].transform);
                        toPlayer.MyPlace = toPosition;
                        toPlayer.playerMove = this;
                    }
                }
                
                await MSelected.ToUniTask(true);
            }
            Move(Position);
        }

        private void Move(int slotNum)
        {
            gameObject.transform.SetParent(sSlot[slotNum].transform);
            Position = slotNum;
            _atamakkoStatus.MyPosition = Position;
            Debug.Log("Moved " + slotNum);
        }
    }
}
