using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void OnButton1()
    {
        SceneManager.LoadScene("Level0");
    }

    public void OnButton2()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnButton3()
    {
        SceneManager.LoadScene("Level2");
    }
    public void OnButton4()
    {
        SceneManager.LoadScene("Level3");
    }
}
