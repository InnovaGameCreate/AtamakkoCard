using UnityEngine;

namespace Assemble
{
    public class equipmentController : MonoBehaviour
    {
        public equipmentView view;
        public equipmentModel Model;

        private void Awake()
        {
            view = GetComponent<equipmentView>();
        }

        public void Init(string[] datalist)
        {
            Model = new equipmentModel(datalist);
            view.Show(Model);
        }
    }
}