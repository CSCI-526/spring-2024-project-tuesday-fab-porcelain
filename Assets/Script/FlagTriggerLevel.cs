using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FlagTriggerLevel : MonoBehaviour
{

    public GameObject flag; // 在Inspector中设置这个引用

    private bool isLevelCompleted = false; //wy add

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == flag && !isLevelCompleted)
        {
            isLevelCompleted = true; // 确保只触发一次
            float completionTime = Time.time - GameManager.Instance.LevelStartTime;
            string levelName = SceneManager.GetActiveScene().name;
            GameDataCollector.Instance.RecordLevelCompletionTime(levelName, completionTime);
            // 获取材质使用次数
            int stickCount = Combination.stickUsageCount;
            int ropeCount = Combination.ropeUsageCount;
            //int springCount = Combination.springUsageCount;
            GameDataCollector.Instance.RecordMaterialUsage(levelName, stickCount, ropeCount);

            Time.timeScale = 1;
            SceneManager.LoadScene("LevelPassed");
        }
        else
        {
            Debug.Log("Trigger entered by non-flag object or level already completed");
        }
    }
}
