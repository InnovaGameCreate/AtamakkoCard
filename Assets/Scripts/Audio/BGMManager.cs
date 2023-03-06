using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace System.Audio
{
    public class BGMManager : MonoBehaviour
    {
        public static BGMManager Instance { get; private set; }
        public float CurrentVolume { get; private set; }
        public AudioSource AudioSource { get; private set; }

        [SerializeField] private List<AudioClip> seLists;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                AudioSource = GetComponent<AudioSource>();
                CurrentVolume = AudioSource.volume;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ShotSe(BGMType type)
        {
            AudioClip clip = null;
            clip = seLists.FirstOrDefault(se => se.name.Equals(type.ToString()));

            if (clip != null && AudioSource.clip != clip)
            {
                StopSound();
                AudioSource.clip = clip;
                AudioSource.Play();
            }
        }

        public void StopSound()
        {
            AudioSource.Stop();
        }
        //BGMManager.Instance.ShotSe(BGMType.Battle1);と入力するとBattle1のBGMが流れる­
    }
}