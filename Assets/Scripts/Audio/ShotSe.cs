using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Audio;

/// <summary>
/// 特定のSEの音を鳴らす
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
