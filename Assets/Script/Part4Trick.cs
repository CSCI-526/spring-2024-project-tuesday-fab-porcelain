using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Part4Trick : MonoBehaviour
//{
//    public GameObject square1;
//    public GameObject square2;
//    private bool platformsActivated = false;
//    public bool isFirstButtonPressed { get; set; }
//    public bool isSecondButtonPressed { get; set; }
//    private float timeSinceFirstPressed = 0f;
//    public float timeLimit = 1f;

//    private void Start()
//    {
//        // 开始时平台不可见且未被激活
//        square1.SetActive(false);
//        square2.SetActive(false);
//    }

//    private void Update()
//    {
//        if (isFirstButtonPressed || isSecondButtonPressed)
//        {
//            timeSinceFirstPressed += Time.deltaTime;

//            if (isFirstButtonPressed && isSecondButtonPressed && timeSinceFirstPressed <= timeLimit && !platformsActivated)
//            {
//                ActivatePlatforms();
//            }
//            else if (timeSinceFirstPressed > timeLimit)
//            {
//                ResetButtons();
//            }
//        }
//    }

//    private void ActivatePlatforms()
//    {
//        square1.SetActive(true);
//        square2.SetActive(true);
//        platformsActivated = true;
//    }

//    public void CheckButtons()
//    {
//        isFirstButtonPressed = GameObject.Find("Button1").GetComponent<Part4Button>().buttonId == 3;
//        isSecondButtonPressed = GameObject.Find("Button2").GetComponent<Part4Button>().buttonId == 4;
//    }

//    private void ResetButtons()
//    {
//        isFirstButtonPressed = false;
//        isSecondButtonPressed = false;
//        timeSinceFirstPressed = 0f;
//    }
//}





public class Part4Trick : MonoBehaviour
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
        // 开始时平台不可见且未被激活
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
        // 在按钮被按下或释放时更新按钮状态
        // 注意：在这个示例中，我们假设按钮通过 OnTriggerEnter2D 和 OnTriggerExit2D 来更新状态
        // 如果你的逻辑不是基于碰撞器的，你需要相应修改此处的逻辑

        // 如果两个按钮都被按下，才会更新按钮状态
        if (isFirstButtonPressed && isSecondButtonPressed)
        {
            isFirstButtonPressed = GameObject.Find("Button1").GetComponent<Part4Button>().buttonId == 3;
            isSecondButtonPressed = GameObject.Find("Button2").GetComponent<Part4Button>().buttonId == 4;
        }
    }

    private void ResetButtons()
    {
        isFirstButtonPressed = false;
        isSecondButtonPressed = false;
        timeSinceFirstPressed = 0f;
    }
}







