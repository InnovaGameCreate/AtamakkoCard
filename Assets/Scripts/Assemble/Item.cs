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
    }
}