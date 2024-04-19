using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class changebuttonColor : MonoBehaviour
{
    public Button button0;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public sceneName sN;
    // Start is called before the first frame update
    void Start()
    {
        if(sN.levelnpassq("Level0")){
            changec(button0);
        }
        if(sN.levelnpassq("Level1")){
            changec(button1);
        }
        if(sN.levelnpassq("Level2")){
            changec(button2);
        }
        if(sN.levelnpassq("Level3")){
            changec(button3);
        }
        if(sN.levelnpassq("Level4")){
            changec(button4);
        }
        if(sN.levelnpassq("Level5")){
            changec(button5);
        }
    }

    void changec(Button button){
         // 获取按钮上的 Image 组件
        Image buttonImage = button.GetComponent<Image>();

        // 设置按钮的颜色
        buttonImage.color = Color.yellow; 
    }
}
