using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerwall : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject wall;
    public float triggedheight;
    private Vector3 scaleChange, positionChange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D boxColliderb1 = button1.GetComponent<BoxCollider2D>();
        BoxCollider2D boxColliderb2 = button2.GetComponent<BoxCollider2D>();
        bool isTriggerb1 = boxColliderb1.isTrigger;
        bool isTriggerb2 = boxColliderb2.isTrigger;
        if(isTriggerb1&&isTriggerb2){

            if(wall.transform.localScale.x > triggedheight){
                scaleChange = new Vector3(-wall.transform.localScale.x / 2, 0.0f, 0.0f);
                positionChange = new Vector3(0.0f, -wall.transform.localScale.x / 4, 0.0f);
                wall.transform.localScale += scaleChange;
                wall.transform.localPosition += positionChange;
            }


        }

    }
}
