using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float maxVolume = 1.0f;
        private Slider _slider;
        private float _nowVolume;
        void Start()
        {
            _slider = GetComponent<Slider>();
            OnValueChanged();
        }

        public void OnValueChanged()
        {
            _nowVolume = _slider.value * maxVolume;
            audioSource.volume = _nowVolume;
        }
    }
}
