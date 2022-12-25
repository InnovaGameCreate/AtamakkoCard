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

        public void InstantiateEffect(EffectType effectType, Transform instantiatePosition)//外部からのアクセス場所
        {
            var tempObject = effectLists.FirstOrDefault(effect => effect.name.Equals(effectType.ToString()));

            if (tempObject != null)
            {
                var Object = Instantiate(tempObject, instantiatePosition.position, Quaternion.identity);
                Debug.Log(instantiatePosition);
                Destroy(Object, 3f);//エフェクトを1秒後の消えるようにする
            }
        }
        //EffectManager.Instance.InstantiateEffect(EffectType.magicAttackEffect, position);と入力すると音が出る­
    }
}