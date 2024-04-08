using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverRestart : MonoBehaviour
{
    public GameObject levelSelector;

    private void Start()
    {
        // levelAgent = GetComponentInParent<LevelSelection>();
        Debug.Log(levelSelector.GetComponent<LevelSelection>().curLevel);
    }

    public void OnSelectLevelButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("levelSelection");
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelSelector.GetComponent<LevelSelection>().curLevel);
    }
}
