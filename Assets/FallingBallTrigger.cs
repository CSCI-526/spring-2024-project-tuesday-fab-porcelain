using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBallTrigger : MonoBehaviour
{
    // 设置引用脚本
    public CreatFallingBalls creatFallingBallsScript; 

    // 触发器事件开始时调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发事件的对象是否为玩家
        if (other.CompareTag("Combination"))
        {
            creatFallingBallsScript.StopFlling();
        }
    }
}
