using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UniRx;

namespace Assemble
{
    public class ChangeContetntPage : MonoBehaviour
    {
        [SerializeField] private ButtonController NextPage;
        [SerializeField] private ButtonController LastPage;
        [SerializeField] private RectTransform Transform;
        private int MaxPageNum = 5;
        private int PageNum;
        void Start()
        {
            NextPage.Pushed.Subscribe(_ =>//NextPageÇÃbuttonÇ™âüÇ≥ÇÍÇΩèÍçá
            {
                if (PageNum < MaxPageNum)
                {
                    Debug.Log("NextPage");
                    NextPageMove();
                    PageNum++;
                }
            }).AddTo(this);
            LastPage.Pushed.Subscribe(_ =>//NextPageÇÃbuttonÇ™âüÇ≥ÇÍÇΩèÍçá
            {
                if (PageNum > 0)
                {
                    Debug.Log("LastPage");
                    LastPageMove();
                    PageNum--;
                }
            }).AddTo(this);

        }

        private void NextPageMove()
        {
            Transform.position -= new Vector3(1100, 0, 0);
        }
        private void LastPageMove()
        {
            Transform.position += new Vector3(1100, 0, 0);
        }

    }
}