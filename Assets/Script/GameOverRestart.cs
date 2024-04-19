using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverRestart : MonoBehaviour
{
    public sceneName sN;

    public void OnSelectLevelButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("levelSelection");
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        string sceneN=sN.getName();
        Debug.Log("continue scene:"+sceneN);

        // 重置关卡时间再重新加载关卡 wy add
        GameManager.Instance.ResetLevelStartTime();

        SceneManager.LoadScene(sceneN);
    }
    public void OnNextLevelButton()
    {
        Time.timeScale = 1;
        string sceneN=sN.getName();
        string levelNumberString = sceneN.Substring(5); // 提取数字部分，即 "3"
        int levelNumber = int.Parse(levelNumberString); // 将字符串转换为整数
        Debug.Log("curlevel:"+levelNumber);
        if (levelNumber<5){

            levelNumber++; // 关卡数加一
            string nextScene = "Level" + levelNumber.ToString(); 
            Debug.Log("nextlevelscene:"+nextScene);

            // 重置关卡时间再重新加载关卡 wy add
            GameManager.Instance.ResetLevelStartTime();
            sN.setName(nextScene);
            sN.passlevel(sceneN);
            SceneManager.LoadScene(nextScene);
        }
        else{
            // 通关所有关卡
            SceneManager.LoadScene("levelSelection");
        }
     }
    public void OnSelectLevelButton_pass()
    {
        Time.timeScale = 1;
        string sceneN=sN.getName();
        sN.passlevel(sceneN);
        SceneManager.LoadScene("levelSelection");
    }
}
