using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GameDataCollector : MonoBehaviour
{
    public static GameDataCollector Instance;
    private string firebaseUrl = "https://csci526proj-default-rtdb.firebaseio.com/levelCompletionTimes.json";

    private Dictionary<string, List<float>> levelCompletionTimes = new Dictionary<string, List<float>>();

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
        if (!levelCompletionTimes.ContainsKey(levelName))
        {
            levelCompletionTimes[levelName] = new List<float>();
        }
        levelCompletionTimes[levelName].Add(completionTime);
        StartCoroutine(PostFirebaseData(levelName, completionTime));
    }

    IEnumerator PostFirebaseData(string levelName, float completionTime)
    {
        string json = $"{{\"LevelName\":\"{levelName}\", \"CompletionTime\":{completionTime}}}";
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
}
