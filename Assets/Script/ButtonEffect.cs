using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    // Objects helping to make the button turn into red when pressed, turn back into green when released
    public GameObject myButton;
    public Color originalColor = Color.red;
    public Color touchColor = Color.green;

    void Start()
    {
        myButton.GetComponent<SpriteRenderer>().color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        myButton.GetComponent<SpriteRenderer>().color = touchColor;

    }


    // Change the color back to the intial color
    void OnTriggerExit2D()
    {
        myButton.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
