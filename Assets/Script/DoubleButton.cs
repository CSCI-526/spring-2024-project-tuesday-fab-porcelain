using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButton : MonoBehaviour
{
    public int buttonId;
    private DoubleButtonTrigger2 trickController;

    private void Start()
    {
        trickController = GetComponentInParent<DoubleButtonTrigger2>();
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
            trickController.isFirstButtonPressed = state;
        }
        else if (buttonId == 4)
        {
            trickController.isSecondButtonPressed = state;
        }
        trickController.CheckButtons(); // ¸üÐÂ°´Å¥×´Ì¬
    }
}
