using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trick1 : MonoBehaviour
{
    public GameObject wall;

    private Vector3 scaleChange, positionChange;

    // Objects helping to make the button turn into red when pressed, turn back into green when released
    // public GameObject myButton;
    // public Color originalColor = Color.green;
    // public Color touchColor = Color.red;

    // void Start()
    // {
    //     myButton.GetComponent<SpriteRenderer>().color = originalColor;
    // }

    public int triggedheight = 4;
    void OnTriggerEnter2D(Collider2D other)
    {
        // get the initial color of the button when it was not pressed yet
        // originalColor = myButton.GetComponent<SpriteRenderer>().color;
        // myButton.GetComponent<SpriteRenderer>().color = touchColor;

        if (wall.transform.localScale.x > triggedheight)
        {
            scaleChange = new Vector3(-wall.transform.localScale.x / 2, 0.0f, 0.0f);
            positionChange = new Vector3(0.0f, -wall.transform.localScale.x / 4, 0.0f);
            wall.transform.localScale += scaleChange;
            wall.transform.localPosition += positionChange;
        }

    }


    // Change the color back to the intial color
    // void OnTriggerExit2D()
    // {
    //     myButton.GetComponent<SpriteRenderer>().color = originalColor;
    // }
}
