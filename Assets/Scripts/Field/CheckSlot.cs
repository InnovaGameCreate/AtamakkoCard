using System;
using System.Collections.Generic;
using Card;
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
            // 本当はUniRxで書きたかった
        }

        private void Update()
        {
            if (displaySlots[0].MyCardID < 0 || displaySlots[1].MyCardID < 0 || displaySlots[2].MyCardID < 0)
            {
                dButton.MyInteractable = false;
            }
            else
            {
                dButton.MyInteractable = true;
            }
        }
    }
}
