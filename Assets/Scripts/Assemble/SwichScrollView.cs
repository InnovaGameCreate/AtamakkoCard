using UnityEngine;

namespace Assemble
{
    public class SwichScrollView : MonoBehaviour
    {
        /// <summary>
        /// �w�肵���X�N���[���r���[�ȊO���\���ɂ���
        /// </summary>
        [SerializeField]
        private GameObject[] ScrollView;
        void Start()
        {
            foreach (var item in ScrollView)
            {
                item.SetActive(false);
            }
            ScrollView[0].SetActive(true);
        }


        public void SwichView(int i)
        {

            foreach (var item in ScrollView)
            {
                item.SetActive(false);
            }
            ScrollView[i].SetActive(true);
        }
    }
}