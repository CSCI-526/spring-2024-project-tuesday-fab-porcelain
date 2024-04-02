using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Headup : MonoBehaviour
{
    private Combination con;
    public string text="Material:STICK";
    public TextMeshProUGUI headsupText;
    // Start is called before the first frame update
    void Start()
    {
        con = GetComponent<Combination>();
        //   // 获取 headsupText 对应的 RectTransform 组件
        // RectTransform rectTransform = headsupText.rectTransform;

        // // 获取屏幕的宽度和高度
        // float screenWidth = Screen.width;
        // float screenHeight = Screen.height;

        // // 设置 headsupText 的位置为屏幕右上角
        // rectTransform.anchoredPosition = new Vector2(screenWidth - rectTransform.rect.width / 2, screenHeight - rectTransform.rect.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        int value = con.connectorIndex%3;
        if(value==0){

            headsupText.text = "Material:STICK";
        }
        else if(value==1){
            headsupText.text = "Material:ROPE";
        }
        else{
            headsupText.text = "Material:SWING";
        }
    }

}
