using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TouchLava : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Combination"))
        {
            Debug.Log("Game Over");
            ShowGameOver(); // 调用显示游戏结束页面的方法
            Time.timeScale = 0; // 停止游戏
        }
    }

    void ShowGameOver()
    {
        SceneManager.LoadScene("GameOverPage");
    }

}
