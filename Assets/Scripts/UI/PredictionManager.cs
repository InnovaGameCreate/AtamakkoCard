using Atamakko;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PredictionManager : MonoBehaviour
    {
        [SerializeField] private GameObject areaUI;
        [SerializeField] private PlayerCore player;
        private readonly GameObject[] _areaUIs = new GameObject[6];
        private readonly Image[] _areaImages = new Image[6];
        
        void Start()
        {
            for (int i = 0; i < 6; i++)
            {
                var transform1 = transform;
                _areaUIs[i] = Instantiate(areaUI, transform1.position , Quaternion.identity, transform1);
                _areaUIs[i].transform.rotation = Quaternion.Euler(0f, 0f, 180 + -60 * i);
                _areaUIs[i].SetActive(false);
                _areaImages[i] = _areaUIs[i].GetComponentInChildren<Image>();
            }
        }

        public void Show(int place, bool isAttack)
        {
            int i = (place + player.AtamakkoData.MyPosition) % 6;
            _areaUIs[i].SetActive(true);
            _areaImages[i].color = isAttack ? new Color(1f, 0f, 0f, 0.5f) : new Color(0f, 1f, 0f, 0.5f);
        }

        public void Hide()
        {
            foreach (var area in _areaUIs)
            {
                area.SetActive(false);
            }
        }
    }
}
