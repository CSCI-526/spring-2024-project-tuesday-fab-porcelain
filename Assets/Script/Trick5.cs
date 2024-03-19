using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trick5 : MonoBehaviour
{
    public static Trick5 Instance; // Singleton instance

    public GameObject gate;
    private bool isMoving = false; // Flag to control gate movement

    private bool isFirstButtonPressed = false;
    private bool isSecondButtonPressed = false;
    private float timeSinceFirstPressed = 0f;

    private float speed = 2.0f; // Set speed here for continuous use
    public float timeLimit = 1f; // Time window for both buttons to be pressed
    private float addedHeight = 10.0f; // Target height to move the gate

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isFirstButtonPressed || isSecondButtonPressed)
        {
            timeSinceFirstPressed += Time.deltaTime;

            if (isFirstButtonPressed && isSecondButtonPressed && timeSinceFirstPressed <= timeLimit && !isMoving)
            {
                isMoving = true; // Start moving the gate
                ResetButtons();
            }
            else if (timeSinceFirstPressed > timeLimit)
            {
                ResetButtons();
            }
        }

        // Continue moving the gate if the flag is true
        if (isMoving)
        {
            Vector3 targetPosition = new Vector3(gate.transform.position.x, gate.transform.position.y + addedHeight, gate.transform.position.z);
            gate.transform.position = Vector3.MoveTowards(gate.transform.position, targetPosition, speed * Time.deltaTime);

            // Optionally, stop moving if the gate reaches the target position
            if (gate.transform.position == targetPosition)
            {
                isMoving = false; // Stop moving the gate
            }
        }
    }

    public void ButtonPressed(int buttonId)
    {
        if (buttonId == 1)
        {
            isFirstButtonPressed = true;
        }
        else if (buttonId == 2)
        {
            isSecondButtonPressed = true;
        }

        if (timeSinceFirstPressed == 0)
        {
            timeSinceFirstPressed = Time.deltaTime;
        }
    }

    private void TriggerAction()
    {
        Debug.Log("Both buttons pressed within time limit!");
        // Add your action here
        float speed = 2.0f;
        float addedHeight = 10.0f;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + addedHeight, transform.position.z), speed * Time.deltaTime);
        gate.transform.position = newPosition;
    }


    private void ResetButtons()
    {
        isFirstButtonPressed = false;
        isSecondButtonPressed = false;
        timeSinceFirstPressed = 0f;

    }
}
