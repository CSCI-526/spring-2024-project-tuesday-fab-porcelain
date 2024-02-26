using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public int buttonId; // Assign unique ID to each button in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Trick6.Instance.ButtonPressed(buttonId);

    }
}

