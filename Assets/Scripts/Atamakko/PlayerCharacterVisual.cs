using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assemble;

public class PlayerCharacterVisual : MonoBehaviour
{
    [SerializeField]
    private Image top, mid, under, accessory1, accessory2, accessory3;
    void Start()
    {
        setImage();
    }

    public void setImage()
    {
        equipmentIcon cardIcon = Resources.Load<equipmentIcon>("EquipmentIcon");
        top.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[0]];
        mid.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[1]];
        under.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[2]];
        accessory1.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[3]];
        accessory2.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[4]];
        accessory3.sprite = cardIcon.equipmentIconList[PlayerConfig.Equipmnet[5]];
    }
}
