using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class trggerpausemenu : MonoBehaviour
{
    public Button continueButton;
    public Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        Button btn1 = continueButton.GetComponent<Button>();
        Button btn2 = menuButton.GetComponent<Button>();
        btn1.onClick.AddListener(OncontinueButton);
        btn2.onClick.AddListener(OnmenuButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OncontinueButton()
    {
     
        SceneManager.UnloadSceneAsync("Pause");
        // Debug.Log("Continue");

    }
    public void OnmenuButton()
    {
    
        SceneManager.LoadScene("levelSelection");


    }
}
