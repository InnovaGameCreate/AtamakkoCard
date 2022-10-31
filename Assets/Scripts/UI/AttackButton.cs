using System;
using Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AttackButton : MonoBehaviour
    {
        [NonSerialized] public int AttackPlace = 0;
        private Button _myButton;

        [NonSerialized] public PlayerAttack PlayerAttack;
        
        void Start()
        {
            _myButton = transform.GetChild(0).gameObject.GetComponent<Button>();

            _myButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    PlayerAttack.AttackPlace = AttackPlace;
                    Debug.Log(AttackPlace);
                    PlayerAttack.ASelected.OnNext(true);
                })
                .AddTo(this);

            PlayerAttack.AttackSelected
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }
    }
}
