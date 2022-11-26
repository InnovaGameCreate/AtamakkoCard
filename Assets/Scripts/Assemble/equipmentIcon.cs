using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assemble
{
    [CreateAssetMenu(menuName = "Create EquipmentIcon", fileName = "EquipmentIcon")]
    public class equipmentIcon : ScriptableObject
    {
        public List<Sprite> equipmentIconList;
    }
}

