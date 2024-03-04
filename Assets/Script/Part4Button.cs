using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Part4Button : MonoBehaviour
//{
//    public int buttonId;
//    private Part4Trick buttonController;

//    private void Start()
//    {
//        buttonController = GetComponentInParent<Part4Trick>();
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        buttonController.CheckButtons();
//        if (buttonId == 3)
//        {
//            buttonController.isFirstButtonPressed = true;
//        }
//        else if (buttonId == 4)
//        {
//            buttonController.isSecondButtonPressed = true;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (buttonId == 3)
//        {
//            buttonController.isFirstButtonPressed = false;
//        }
//        else if (buttonId == 4)
//        {
//            buttonController.isSecondButtonPressed = false;
//        }
//        buttonController.CheckButtons();
//    }
//}





public class Part4Button : MonoBehaviour
{
    public int buttonId;
    private Part4Trick trickController;

    private void Start()
    {
        trickController = GetComponentInParent<Part4Trick>();
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