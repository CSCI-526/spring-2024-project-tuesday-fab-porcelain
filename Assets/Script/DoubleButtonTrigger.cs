using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleButtonTrigger : MonoBehaviour
{
    public GameObject square1;
    public GameObject square2;
    private bool platformsActivated = false;
    public bool isFirstButtonPressed { get; set; }
    public bool isSecondButtonPressed { get; set; }
    private float timeSinceFirstPressed = 0f;
    public float timeLimit = 1f;

    private void Start()
    {
        // ¿ªÊ¼Ê±Æ½Ì¨²»¿É¼ûÇÒÎ´±»¼¤»î
        square1.SetActive(false);
        square2.SetActive(false);
    }

    private void Update()
    {
        if (isFirstButtonPressed && isSecondButtonPressed)
        {
            timeSinceFirstPressed += Time.deltaTime;

            if (timeSinceFirstPressed <= timeLimit && !platformsActivated)
            {
                ActivatePlatforms();
            }
            else if (timeSinceFirstPressed > timeLimit)
            {
                ResetButtons();
            }
        }
    }

    private void ActivatePlatforms()
    {
        square1.SetActive(true);
        square2.SetActive(true);
        platformsActivated = true;
    }

    public void CheckButtons()
    {
        // ÔÚ°´Å¥±»°´ÏÂ»òÊÍ·ÅÊ±¸üÐÂ°´Å¥×´Ì¬
        // ×¢Òâ£ºÔÚÕâ¸öÊ¾ÀýÖÐ£¬ÎÒÃÇ¼ÙÉè°´Å¥Í¨¹ý OnTriggerEnter2D ºÍ OnTriggerExit2D À´¸üÐÂ×´Ì¬
        // Èç¹ûÄãµÄÂß¼­²»ÊÇ»ùÓÚÅö×²Æ÷µÄ£¬ÄãÐèÒªÏàÓ¦ÐÞ¸Ä´Ë´¦µÄÂß¼­

        // Èç¹ûÁ½¸ö°´Å¥¶¼±»°´ÏÂ£¬²Å»á¸üÐÂ°´Å¥×´Ì¬
        // Debug.Log(isFirstButtonPressed);
        if (isFirstButtonPressed && isSecondButtonPressed)
        {
            isFirstButtonPressed = GameObject.Find("Button1").GetComponent<DoubleButton2>().buttonId == 3;
            isSecondButtonPressed = GameObject.Find("Button2").GetComponent<DoubleButton2>().buttonId == 4;
        }
    }

    private void ResetButtons()
    {
        isFirstButtonPressed = false;
        isSecondButtonPressed = false;
        timeSinceFirstPressed = 0f;
    }
}
