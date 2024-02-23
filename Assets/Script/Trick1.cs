using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick1 : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject child = other.GetComponent<Collider2D>().gameObject;
        Debug.Log("in ");

        if(child.name == "Button"){
            Debug.Log("hit botton");
        }
    }
}
