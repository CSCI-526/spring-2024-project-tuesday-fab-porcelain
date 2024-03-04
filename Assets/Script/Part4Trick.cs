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
//        // ��ʼʱƽ̨���ɼ���δ������
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
        // ��ʼʱƽ̨���ɼ���δ������
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
        // �ڰ�ť�����»��ͷ�ʱ���°�ť״̬
        // ע�⣺�����ʾ���У����Ǽ��谴ťͨ�� OnTriggerEnter2D �� OnTriggerExit2D ������״̬
        // �������߼����ǻ�����ײ���ģ�����Ҫ��Ӧ�޸Ĵ˴����߼�

        // ���������ť�������£��Ż���°�ť״̬
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







