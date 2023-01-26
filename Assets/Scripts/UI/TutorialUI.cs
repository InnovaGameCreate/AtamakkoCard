using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private GameObject shadow;
        [SerializeField] private ClickEvent cancel;

        [SerializeField] private GameObject[] cardEmphasises;
        [SerializeField] private GameObject[] slotEmphasises;
        [SerializeField] private GameObject actionEmphasis1;
        [SerializeField] private GameObject actionEmphasis2;
        [SerializeField] private GameObject ultimateEmphasis;
        [SerializeField] private GameObject speedupEmphasis;
        [SerializeField] private GameObject decisionEmphasis;
        [SerializeField] private GameObject[] settingExplanations;
        [SerializeField] private GameObject[] actionExplanations;
        [SerializeField] private GameObject specialExplanation;
        [SerializeField] private GameObject speedupExplanation;
        [SerializeField] private GameObject decisionExplanation;

        [SerializeField] private GameObject emphasis;
        [SerializeField] private GameObject explanation;

        public void SelectCard(int times)
        {
            shadow.SetActive(true);
            cardEmphasises[times].SetActive(true);
            settingExplanations[times].SetActive(true);
            slotEmphasises[times % 3].SetActive(true);
        }

        public void SelectAction(int times)
        {
            shadow.SetActive(true);
            if (times is 1 or 2) actionEmphasis1.SetActive(true);
            else actionEmphasis2.SetActive(true);
        }

        public async UniTask ActionDirection(int times)
        {
            cancel.gameObject.SetActive(true);
            actionExplanations[times].SetActive(true);

            await cancel.Clicked.ToUniTask(true);
        }

        public void EmphasisUltimate()
        {
            shadow.SetActive(true);
            ultimateEmphasis.SetActive(true);
            specialExplanation.SetActive(true);
        }

        public void UltimateSelect()
        {
            shadow.SetActive(true);
            speedupEmphasis.SetActive(true);
            speedupExplanation.SetActive(true);
        }

        public void EmphasisDecision()
        {
            shadow.SetActive(true);
            decisionEmphasis.SetActive(true);
            decisionExplanation.SetActive(true);
        }

        public void HideUI()
        {
            shadow.SetActive(false);
            cancel.gameObject.SetActive(false);
            foreach (Transform childClear in emphasis.transform)
            {
                childClear.gameObject.SetActive(false);
            }
            foreach (Transform childDirection in explanation.transform)
            {
                childDirection.gameObject.SetActive(false);
            }
        }
    }
}
