using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public string curLevel = "Level1";

    public void OnButton1()
    {
        curLevel = "Level1";
        SceneManager.LoadScene("Level1");
    }

    public void OnButton2()
    {
        curLevel = "Level2";
        SceneManager.LoadScene("Level2");
    }

    public void OnButton3()
    {
        curLevel = "Level3";
        SceneManager.LoadScene("Level3");
    }
    public void OnButton4()
    {
        curLevel = "Level4";
        SceneManager.LoadScene("Level4");
    }
    public void OnButton5()
    {
        curLevel = "Level5";
        SceneManager.LoadScene("Level5");
    }
    public void OnButton6()
    {
        curLevel = "Level6";
        SceneManager.LoadScene("Level6");
    }
}
