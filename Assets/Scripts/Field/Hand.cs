using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private int grabbingCardID;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Input.mousePosition;
    }

    public int GetGrabbingCardID()
    {
        int oldCardID = grabbingCardID;
        grabbingCardID = -1;
        return oldCardID;
    }

    public void SetGrabbingCardID(int cardID)
    {
        grabbingCardID = cardID;
    }

    public bool IsHavaintCardID()
    {
        return grabbingCardID != -1;
    }
}
