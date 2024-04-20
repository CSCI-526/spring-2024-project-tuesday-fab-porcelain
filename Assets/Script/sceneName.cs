using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneName : MonoBehaviour
{
    private static string playingsceneN;
    // Start is called before the first frame update

    private static List<string> levelpassList = new List<string>();
    public void setName(string name){

        playingsceneN=name;
        Debug.Log("setname:"+playingsceneN);

    }
    public string getName(){

        Debug.Log("getname:"+playingsceneN);
        return playingsceneN;
        

    }

    public bool levelnpassq(string leveln){
        foreach (string level in levelpassList)
        {
            if (level == leveln)
            {
                return true;
            }
        }
        return false;
    }
    
    public void passlevel(string leveln){
        levelpassList.Add(leveln);
    }
}
