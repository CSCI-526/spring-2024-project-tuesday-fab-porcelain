using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchLava : MonoBehaviour
{
    public DeathDataUploader deathDataUploader; 

    private int lastCheckpointNumber = -1; 

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Combination"))
        {
            Debug.Log("Game Over");
            Debug.Log("Last Checkpoint Number: " + lastCheckpointNumber);
            deathDataUploader.RecordDeath(lastCheckpointNumber);
            ShowGameOver(); 
            Time.timeScale = 0; 
        }
    }

    void ShowGameOver()
    {
        SceneManager.LoadScene("GameOverPage");
    }

    
    public void UpdateLastCheckpointNumber(int checkpointNumber)
    {
        lastCheckpointNumber = checkpointNumber;
    }
}


















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;


//public class TouchLava : MonoBehaviour
//{


//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        if (other.gameObject.CompareTag("Combination"))
//        {
//            Debug.Log("Game Over");

//            ShowGameOver(); // 调用显示游戏结束页面的方法
//            Time.timeScale = 0; // 停止游戏
//        }
//    }

//    void ShowGameOver()
//    {
//        SceneManager.LoadScene("GameOverPage");
//    }

//}
