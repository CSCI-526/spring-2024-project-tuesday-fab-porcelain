using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestory : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // 当小球碰到地面时销毁
            Destroy(gameObject); 
        }
        else if (other.gameObject.CompareTag("Combination"))
        {
            // 游戏结束逻辑
            Debug.Log("Game Over");
            //Debug.Log("Game Over by " + other.gameObject.name); 
            Time.timeScale = 0; // 停止游戏
        }
    }

    //显示游戏结束的画面
    void ShowGameOver() { }

    //返回主菜单
    public void RestartGame() { }
}

