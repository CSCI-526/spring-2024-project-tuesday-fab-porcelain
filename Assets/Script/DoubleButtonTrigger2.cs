using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButtonTrigger2 : MonoBehaviour
{
    public GameObject wall;
    private Vector3 scaleChange, positionChange;
    private bool platformsActivated = true;
    public bool isFirstButtonPressed { get; set; }
    public bool isSecondButtonPressed { get; set; }
    private float timeSinceFirstPressed = 0f;
    public float timeLimit = 1f;

    private void Update()
    {
        if (isFirstButtonPressed && isSecondButtonPressed)
        {
            timeSinceFirstPressed += Time.deltaTime;

            if (timeSinceFirstPressed <= timeLimit && platformsActivated)
            {
                ShortenPlatforms();
            }
            else if (timeSinceFirstPressed > timeLimit)
            {
                ResetButtons();
            }
        }
    }

    private void ShortenPlatforms()
    {
        scaleChange = new Vector3(0.0f, -wall.transform.localScale.y / 2, 0.0f);
        positionChange = new Vector3(0.0f, -wall.transform.localScale.y / 4, 0.0f);
        wall.transform.localScale += scaleChange;
        wall.transform.localPosition += positionChange;
        platformsActivated = false;
    }

    public void CheckButtons()
    {
        // ÔÚ°´Å¥±»°´ÏÂ»òÊÍ·ÅÊ±¸üÐÂ°´Å¥×´Ì¬
        // ×¢Òâ£ºÔÚÕâ¸öÊ¾ÀýÖÐ£¬ÎÒÃÇ¼ÙÉè°´Å¥Í¨¹ý OnTriggerEnter2D ºÍ OnTriggerExit2D À´¸üÐÂ×´Ì¬
        // Èç¹ûÄãµÄÂß¼­²»ÊÇ»ùÓÚÅö×²Æ÷µÄ£¬ÄãÐèÒªÏàÓ¦ÐÞ¸Ä´Ë´¦µÄÂß¼­

        // Èç¹ûÁ½¸ö°´Å¥¶¼±»°´ÏÂ£¬²Å»á¸üÐÂ°´Å¥×´Ì¬
        if (isFirstButtonPressed && isSecondButtonPressed)
        {
            isFirstButtonPressed = GameObject.Find("Button1").GetComponent<DoubleButton>().buttonId == 3;
            isSecondButtonPressed = GameObject.Find("Button2").GetComponent<DoubleButton>().buttonId == 4;
        }
    }

    private void ResetButtons()
    {
        isFirstButtonPressed = false;
        isSecondButtonPressed = false;
        timeSinceFirstPressed = 0f;
    }
}
