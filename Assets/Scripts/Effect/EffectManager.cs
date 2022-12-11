﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace System.Effect
{
    public class EffectManager : MonoBehaviour
    {

        public static EffectManager Instance { get; private set; }
        public AudioSource AudioSource { get; private set; }

        [SerializeField] private List<GameObject> effectLists;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InstatiateEffect(EffectType effectType, Transform InstantiatePosition)//外部からのアクセス場所
        {
            GameObject tempObject = null;
            tempObject = effectLists.FirstOrDefault(effect => effect.name.Equals(effectType.ToString()));

            if (tempObject != null)
            {
                var Object = Instantiate(tempObject, InstantiatePosition.position,Quaternion.identity);
                Destroy(Object, 1f);//エフェクトを1秒後の消えるようにする
            }
        }
        //EffectManager.Instance.InstatiateEffect(EffectType.magicAttackEffet);と入力すると音が出る­
    }
}