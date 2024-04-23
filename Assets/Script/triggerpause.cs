using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class triggerpause : MonoBehaviour
{
    private int trigger=1;
    public Button pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = pauseButton.GetComponent<Button>();
		btn.onClick.AddListener(OnPauseButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // public void OnPauseButton()
    // {
    //     if (trigger==1){
    //         Time.timeScale = 0;
    //         buttonText.text = "CONTINUE";
    //         SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    //         Debug.Log("Pause");
    //     }
    //     else{
    //         Time.timeScale = 1;
    //         buttonText.text = "PAUSE";
    //         Debug.Log("Continue");
    //     }
    //     trigger=1-trigger;
    // }
    public void OnPauseButton()
    {

        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        Debug.Log("Pause");

    }
  
}
