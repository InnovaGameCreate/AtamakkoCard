using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPanel : MonoBehaviour
{

    [SerializeField]
    private GameObject TargetObject;

    public void OnClick(bool _bool)
    {
        TargetObject.SetActive(_bool);
    }
}
