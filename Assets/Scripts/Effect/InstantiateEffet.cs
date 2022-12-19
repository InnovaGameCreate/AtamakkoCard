using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Effect
{
    public class InstantiateEffet : MonoBehaviour
    {
        [SerializeField]
        private Transform _transform;
        [SerializeField]
        private EffectType type;


        public void Onclick()
        {
            EffectManager.Instance.InstantiateEffect(type, _transform);
        }
    }
}