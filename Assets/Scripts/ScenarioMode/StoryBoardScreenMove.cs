using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardScreenMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float RotateSpeed = 2;
    [SerializeField]
    private float zoomSpeed = 0.03f;
    private int rotation = 0;
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.position += new Vector3(0, moveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position += new Vector3(0, -moveSpeed, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.position += new Vector3(-moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.position += new Vector3(moveSpeed, 0, 0);
        }


        if (Input.GetKey(KeyCode.E))
        {
            rotation--;
            this.gameObject.transform.rotation = Quaternion.Euler(60, 0, RotateSpeed * rotation);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotation++;
            this.gameObject.transform.rotation = Quaternion.Euler(60, 0, RotateSpeed * rotation);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            this.gameObject.transform.localScale += new Vector3(zoomSpeed, zoomSpeed, zoomSpeed);
        }
        if (Input.GetKey(KeyCode.X))
        {
            if(this.gameObject.transform.localScale.x > 0)
            {
                this.gameObject.transform.localScale -= new Vector3(zoomSpeed, zoomSpeed, zoomSpeed);
            }
        }
    }
}
