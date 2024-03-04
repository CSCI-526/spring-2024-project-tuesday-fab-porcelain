using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject helpPanel;

    void Start()
    {
        helpPanel.SetActive(false); // 在游戏开始时设置为不可见
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("levelSelection");
    }

    // 显示操作指导窗口
    public void ShowHelpPanel()
    {
        helpPanel.SetActive(true);
    }

    // 隐藏操作指导窗口
    public void HideHelpPanel()
    {
        helpPanel.SetActive(false);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene("Menu");
    }


}
