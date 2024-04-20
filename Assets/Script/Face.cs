using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public Transform follow;
    public KeyCode left;
    public KeyCode right;

    public GameObject leftFace;
    public GameObject rightFace;

    public Transform redColor;
    private void LateUpdate()
    {
        transform.position = follow.position;


        if (Input.GetKeyDown(left))
        {
            leftFace.SetActive(true);
            rightFace.SetActive(false);
        }
        if(Input.GetKeyDown (right))
        {
            leftFace.SetActive(false);
            rightFace.SetActive(true);
        }


    }

    public void SetFaceRed(float value)
    {
        redColor.localPosition =new Vector3(0,-0.5f +value/2,0);
        redColor.localScale = new Vector3(1, value, 1);
    }
}
