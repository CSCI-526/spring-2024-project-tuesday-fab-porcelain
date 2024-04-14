using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public sceneName sN;
    public void OnButton0()
    {
       sN.setName("Level0");
       SceneManager.LoadScene("Level0");

    }
    public void OnButton1()
    {
        sN.setName("Level1");
        SceneManager.LoadScene("Level1");

       
    }

    public void OnButton2()
    {
        sN.setName("Level2");
        SceneManager.LoadScene("Level2");
        
    }

    public void OnButton3()
    {
       sN.setName("Level3");
       SceneManager.LoadScene("Level3");
       
    }
    public void OnButton4()
    {
       sN.setName("Level4");
       SceneManager.LoadScene("Level4");
       
    }
    public void OnButton5()
    {
       sN.setName("Level5");
       SceneManager.LoadScene("Level5");
       
    }
    
}
