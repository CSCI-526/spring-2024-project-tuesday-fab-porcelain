using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
