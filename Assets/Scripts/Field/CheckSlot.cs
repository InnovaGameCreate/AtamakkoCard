using System.Collections.Generic;
using Card;
using UI;
using UniRx;
using UnityEngine;

namespace Field
{
    public class CheckSlot : MonoBehaviour
    {
        [SerializeField] private List<CardSlot> displaySlots = new List<CardSlot>();
        
        [SerializeField] private DecisionButton dButton;
        
        void Start()
        {
            foreach (var cardSlot in displaySlots)
            {
                var cardMovement = cardSlot.gameObject.GetComponent<CardMovement>();
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
