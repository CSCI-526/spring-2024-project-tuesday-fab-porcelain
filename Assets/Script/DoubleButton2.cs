using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton2 : MonoBehaviour
{
    public int buttonId;
    private DoubleButtonTrigger trickController;

    private void Start()
    {
        trickController = GetComponentInParent<DoubleButtonTrigger>();
        Debug.Log(trickController);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UpdateButtonState(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UpdateButtonState(false);
    }

    private void UpdateButtonState(bool state)
    {
        if (buttonId == 3)
        {
            Debug.Log("in 3");
            trickController.isFirstButtonPressed = state;
        }
        else if (buttonId == 4)
        {
            Debug.Log("in 4");
            
            trickController.isSecondButtonPressed = state;
        }
        Debug.Log(trickController);
        trickController.CheckButtons(); // ¸üÐÂ°´Å¥×´Ì¬
    }
}
