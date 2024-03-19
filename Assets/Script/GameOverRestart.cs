using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverRestart : MonoBehaviour
{
    public void OnRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
