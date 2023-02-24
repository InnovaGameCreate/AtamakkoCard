using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace storyMode
{
    public class HideTile : MonoBehaviour
    {
        /// <summary>
        /// ��\���̃I�u�W�F�N�g�����̏����ŕ\������
        /// </summary>
        [SerializeField] private GameObject[] hideTile;

        public void OnClick(bool _bool)
        {
            var PlayerObject = GameObject.FindGameObjectWithTag("Player");
            var distance = Vector3.Distance(gameObject.transform.position, PlayerObject.transform.position);
            if (distance < 180)
            {
                foreach (var item in hideTile)
                {
                    item.SetActive(_bool);
                }
            }
        }
    }
}