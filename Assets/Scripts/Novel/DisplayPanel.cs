using UnityEngine;

namespace storyMode
{
    public class DisplayPanel : MonoBehaviour
    {
        /// <summary>
        /// ��\���̃I�u�W�F�N�g�����̏����ŕ\������
        /// </summary>
        [SerializeField] private GameObject TargetObject;

        public void OnClick(bool _bool)
        {
            TargetObject.SetActive(_bool);
        }
    }
}