using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showtuimg : MonoBehaviour
{
    private RectTransform imageRectTransform; // Image对象的RectTransform组件
    public float loc1xmin;
    public float loc1xmax;
    public float loc1ymin;
    public float loc1ymax;

    // public float showlocx;
    // public float showlocy;

    public GameObject playerA;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        // 获取Image对象的
        img = GetComponent<Image>();
        imageRectTransform = GetComponent<RectTransform>();
        // 隐藏图像
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 获取 PlayerA 的位置
        Vector3 playerAPosition = playerA.transform.position;
        if (playerAPosition.x >= loc1xmin && playerAPosition.x <= loc1xmax && 
            playerAPosition.y >= loc1ymin && playerAPosition.y <= loc1ymax){
            

        //    // 计算新的 anchoredPosition
        //     Vector2 newPosition = new Vector2(showlocx, showlocy);

        //     // 设置 Image 的位置
        //     imageRectTransform.anchoredPosition = newPosition;

            // 显示图像
            img.enabled = true;
   
        }
        else{
             img.enabled = false;
             

        }
    }
}
