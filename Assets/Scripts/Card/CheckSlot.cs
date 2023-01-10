using System.Collections.Generic;
using UI;
using UniRx;
using UnityEngine;

namespace Card
{
    /// <summary>
    /// カードがセットされているかどうかを判定するクラス
    /// </summary>
    public class CheckSlot : MonoBehaviour
    {
        [SerializeField] private List<CardSlot> displaySlots = new List<CardSlot>(); // セットスロット
        [SerializeField] private ButtonController dButton; // 決定ボタン
        
        void Start()
        {
            foreach (var cardSlot in displaySlots)
            {
                var cardMovement = cardSlot.gameObject.GetComponent<CardMovement>();
                // カードをセットしたときに全てのスロットにカードが入っているかどうかを判定する
                cardMovement.CheckCardID
                    .Subscribe(_ =>
                    {
                        if (displaySlots[0].MyCardID < 0 || displaySlots[1].MyCardID < 0 ||
                            displaySlots[2].MyCardID < 0)
                        {
                            dButton.MyInteractable = false;
                        }
                        else
                        {
                            dButton.MyInteractable = true;
                        }
                    })
                    .AddTo(this);
            }
        }
    }
}
