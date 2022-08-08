using UnityEngine;

namespace Assemble
{
    [CreateAssetMenu(fileName = "Items", menuName = "Items/Armoritem")]
    public class Item : ScriptableObject
    {
        [SerializeField]
        private string itemName;
        [SerializeField]
        private Sprite itemImage;
        [SerializeField]
        private int CardNum1;
        [SerializeField]
        private int CardNum2;
        public enum slotType
        {
            upper,
            mid,
            lower,
            accessory
        }
        [SerializeField]
        public slotType PartsType;


        public string MyItemName { get => itemName; }
        public Sprite MyItemImage { get => itemImage; }
        public slotType MyPartsType { get => PartsType; }
        public int MyCardNum1 { get => CardNum1; set => CardNum1 = value; }
        public int MyCardNum2 { get => CardNum2; set => CardNum2 = value; }
    }
}