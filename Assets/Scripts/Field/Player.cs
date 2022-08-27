using System;
using Card;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Field
{
    public class Player : MonoBehaviour
    {
        private int _position;

        public int MyPosition
        {
            get => _position;
            set => _position = value;
        }

        [SerializeField] private GameObject[] sSlot;
        [SerializeField] private PButton pButton;

        private readonly Subject<bool> _moved = new Subject<bool>();
        public IObservable<bool> Moved => _moved;

        public async UniTask CanMove(int cardID)
        {
            var card = new CardModel(CardData.CardDataArrayList[cardID]);
            for(int i = 0; i < card.Move.Length; i++)
            {
                if (card.Move[i] == "〇")
                {
                    var toPosition = (i + _position) % 6;
                    var toPlayer = Instantiate(pButton, transform.position, Quaternion.identity,
                        sSlot[toPosition].transform);
                    toPlayer.MyPlace = toPosition;
                    toPlayer.player = this;
                }
            }
            if (card.Move[0] == "〇" || card.Move[1] == "〇" || card.Move[2] == "〇" || card.Move[3] == "〇" || card.Move[4] == "〇" || card.Move[5] == "〇")
            {
                await UniTask.WaitUntilValueChanged(this, player => player._position);
            }
            Move(_position);
        }

        private void Move(int slotNum)
        {
            gameObject.transform.SetParent(sSlot[slotNum].transform);
            _position = slotNum;
            Debug.Log("Moved" + slotNum);
            _moved.OnNext(true);
        }
    }
}
