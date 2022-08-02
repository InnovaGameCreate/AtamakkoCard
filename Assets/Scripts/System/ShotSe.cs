using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Audio;

/// <summary>
/// “Á’è‚ÌSE‚Ì‰¹‚ð–Â‚ç‚·
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
