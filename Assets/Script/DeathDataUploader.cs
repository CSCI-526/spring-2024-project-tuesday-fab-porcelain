using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

public class DeathDataUploader : MonoBehaviour
{
    //private int lastCheckpointNumber = -1;
    private int deathCount = 0;
    private string firebaseUrl = "https://csci526-19391-default-rtdb.firebaseio.com/Death.json"; 

    public void RecordDeath(int lastCheckpointNumber)
    {
       
        deathCount++;

       
        string jsonData = $"{{\"LastCheckpoint\":{lastCheckpointNumber}, \"DeathCount\":{deathCount}}}";

        
        StartCoroutine(UploadDeathData(jsonData));
    }

    IEnumerator UploadDeathData(string jsonData)
    {
        UnityWebRequest www = UnityWebRequest.Put(firebaseUrl, jsonData);
        www.method = UnityWebRequest.kHttpVerbPOST;
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error uploading death data: {www.error}");
        }
        else
        {
            Debug.Log("Death data upload successful");
        }
    }
}
