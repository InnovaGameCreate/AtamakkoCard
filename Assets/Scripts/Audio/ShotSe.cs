using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Audio;

/// <summary>
/// �����SE�̉���炷
/// </summary>
public class ShotSe : MonoBehaviour
{
    [SerializeField]
    SeType se;
    public void ShotSeSound()
    {
        SeManager.Instance.ShotSe(se);
    }
}
