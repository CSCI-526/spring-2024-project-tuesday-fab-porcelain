using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneName : MonoBehaviour
{
    private static string playingsceneN;
    // Start is called before the first frame update
    public void setName(string name){

        playingsceneN=name;
        Debug.Log("setname:"+playingsceneN);

    }
    public string getName(){

        Debug.Log("getname:"+playingsceneN);
        return playingsceneN;
        

    }
}
