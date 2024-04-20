using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick1 : MonoBehaviour
{
    public GameObject wall;
    private Vector3 scaleChange, positionChange;
    public int triggedheight=4;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(wall.transform.localScale.x > triggedheight){
            scaleChange = new Vector3(-wall.transform.localScale.x / 2, 0.0f, 0.0f);
            positionChange = new Vector3(0.0f, -wall.transform.localScale.x / 4, 0.0f);
            wall.transform.localScale += scaleChange;
            wall.transform.localPosition += positionChange;
        }
    }
}
