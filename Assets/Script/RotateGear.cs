using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGear : MonoBehaviour
{
    public int tempoIntervalo;
    int multiplier = 1; //Will be -1 to lower, 1 to raisen and 0 to stand still
    float rotationSpeed = 0f; //The speed we rotate by, set in Start()
 
    void Start()
    {
        StartCoroutine(SetMultiplier()); //I took the liberty to make a better function name
        rotationSpeed = -120 / 5; //90 degrees per 5 seconds
    }
 
    private void Update()
    {
        if(multiplier==0)
        {
            return;
        }
        //This can be somewhat inaccurate so we will also set the accurate rotation when we have reached the final position
        transform.Rotate(0, 0, multiplier * rotationSpeed * Time.deltaTime); // multiplier defines direction,
                                                                             // Time.deltaTime is the time between each frame
    }
 
    IEnumerator SetMultiplier()
    {
        while (true) //depending on starting position you might need to edit the order of these
        {
            multiplier = 1; //sets the bridge up
            yield return new WaitForSeconds(tempoIntervalo); //waiting for it to rotate down
            multiplier = 0; //let it stand still
            yield return new WaitForSeconds(tempoIntervalo); //waiting

        }
    }
}
