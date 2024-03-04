using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallDestory : MonoBehaviour
{
    // 引用GameOver页面的Panel
    public GameObject gameOverPage; // 在Inspector中设置这个引用

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
            ShowGameOver(); // 调用显示游戏结束页面的方法
            Time.timeScale = 0; // 停止游戏
        }

        //if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Combination"))
        //{
        //    // 当小球碰到地面或Combination时，销毁小球并显示游戏结束页面
        //    Debug.Log("Game Over");
        //    ShowGameOver(); // 调用显示游戏结束页面的方法
        //    Time.timeScale = 0; // 停止游戏
        //    //Destroy(gameObject); // 注意：销毁对象应在最后执行
        //}

    }

    void ShowGameOver()
    {
        //Debug.Log("Trying to show game over page."); // 调试信息
        //if (gameOverPage != null)
        //{
        //    gameOverPage.SetActive(true);
        //    Time.timeScale = 0;
        //    Debug.Log("Set page active");
        //}
        //else
        //{
        //    Debug.Log("GameOverPage is not set."); // 如果没有设置GameOverPage，输出调试信息
        //}
        SceneManager.LoadScene("GameOverPage");
    }


    //返回主菜单
    public void RestartGame() { }
}

