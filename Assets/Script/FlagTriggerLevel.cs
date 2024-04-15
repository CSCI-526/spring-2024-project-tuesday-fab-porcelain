using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FlagTriggerLevel : MonoBehaviour
{

    public GameObject flag; // 在Inspector中设置这个引用

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的是不是旗帜
        if (other.gameObject == flag)
        {
            Debug.Log("Ball is close to the Flag!");
            // 这里可以添加你想要的逻辑
            Time.timeScale = 1;
            SceneManager.LoadScene("levelSelection");
        }
    }
}
