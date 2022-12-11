using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Assemble
{
    public class slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
    {
        private Item item;
        [SerializeField]
        protected Image itemImage;

        private GameObject draggingObj;
        [SerializeField]
        private GameObject itemImageObj;
        [SerializeField]
        private TextMeshProUGUI textMesh;
        private Transform canvasTransform;
        public Item MyItem { get => item; protected set => item = value; }

        private hand Hand;
        protected virtual void Start()
        {
            canvasTransform = FindObjectOfType<Canvas>().transform;
            Hand = FindObjectOfType<hand>();

            if (MyItem == null) itemImage.color = new Color(0, 0, 0, 0);


        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (MyItem == null) return;
            draggingObj = Instantiate(itemImageObj, canvasTransform);
            draggingObj.transform.SetAsLastSibling();
            itemImage.color = Color.gray;
            Hand.SetGtabbingItem(MyItem);

        }
        public void OnDrag(PointerEventData eventData)
        {
            if (MyItem == null) return;
            draggingObj.transform.position = eventData.position + new Vector2(20, 20);
        }

        public virtual void setItem(Item item)
        {
            MyItem = item;

            if (item != null)
            {
                itemImage.color = new Color(1, 1, 1, 1);
                itemImage.sprite = item.MyItemImage;
                textMesh.text = MyItem.MyItemName;
            }
            else
            {
                itemImage.color = new Color(0, 0, 0, 0);
            }
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (!Hand.IsHavingItem()) return;
            Item gotItem = Hand.GetGrabbingItem();
            Hand.SetGtabbingItem(MyItem);
            setItem(gotItem);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            Destroy(draggingObj);
            Item gotItem = Hand.GetGrabbingItem();
            setItem(gotItem);
        }

    }
}