using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class GameDataCollector : MonoBehaviour
{
    public static GameDataCollector Instance;
    private string firebaseUrl = "https://csci526-19391-default-rtdb.firebaseio.com/levelCompletionTimes.json";

    public class LevelCompletionData
    {
        public float CompletionTime;
        public string Timestamp;

        public LevelCompletionData(float completionTime, string timestamp)
        {
            CompletionTime = completionTime;
            Timestamp = timestamp;
        }
    }


    private Dictionary<string, List<LevelCompletionData>> levelCompletionTimes = new Dictionary<string, List<LevelCompletionData>>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RecordLevelCompletionTime(string levelName, float completionTime)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        if (!levelCompletionTimes.ContainsKey(levelName))
        {
            levelCompletionTimes[levelName] = new List<LevelCompletionData>();
        }
        levelCompletionTimes[levelName].Add(new LevelCompletionData(completionTime, timestamp));

        StartCoroutine(PostFirebaseData(levelName, completionTime, timestamp));
    }


    public void RecordMaterialUsage(string levelName, int stickCount, int ropeCount)
    {
        //string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string json = $"{{\"LevelName\":\"{levelName}\", \"StickCount\":{stickCount}, \"RopeCount\":{ropeCount}}}";
        StartCoroutine(PostFirebaseData2("MaterialUsage", json));
    }

    IEnumerator PostFirebaseData(string levelName, float completionTime, string timestamp)
    {
        string json = $"{{\"LevelName\":\"{levelName}\", \"CompletionTime\":{completionTime}, \"Timestamp\":\"{timestamp}\"}}";
        using (UnityWebRequest request = UnityWebRequest.Put(firebaseUrl, json))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Data uploaded successfully!");
            }
        }
    }

    IEnumerator PostFirebaseData2(string dataType, string jsonData)
    {
        string firebaseUrl2 = "https://csci526-19391-default-rtdb.firebaseio.com/" + dataType + ".json"; 

        using (UnityWebRequest request = UnityWebRequest.Put(firebaseUrl2, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error uploading data: {request.error}");
            }
            else
            {
                Debug.Log($"Data uploaded successfully for {dataType}");
            }
        }
    }

}
