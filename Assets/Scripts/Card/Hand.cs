using UnityEngine;

namespace Field
{
    public class Hand : MonoBehaviour
    {
        private int _grabbingCardID;

        // Update is called once per frame
        void Update()
        {
            if (Camera.main != null)
            {
                var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = 0f;
                transform.position = targetPos;
            }
        }

        public int GetGrabbingCardID()
        {
            int oldCardID = _grabbingCardID;
            _grabbingCardID = -1;
            return oldCardID;
        }

        public void SetGrabbingCardID(int cardID)
        {
            _grabbingCardID = cardID;
        }

        public bool IsHavaintCardID()
        {
            return _grabbingCardID != -1;
        }
    }
}
