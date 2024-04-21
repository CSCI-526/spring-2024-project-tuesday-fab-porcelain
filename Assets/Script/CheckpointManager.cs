using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System;

public class CheckpointManager : MonoBehaviour
{
    private Dictionary<int, float> checkpointTimes = new Dictionary<int, float>();
    private string firebaseUrl = "https://csci526-19391-default-rtdb.firebaseio.com/Checkpoints.json"; // 更换为你的Firebase数据库URL

    public void CheckpointTriggered(int checkpointNumber)
    {
        float currentTime = Time.time;
        checkpointTimes[checkpointNumber] = currentTime;  // 更新或设置检查点时间

        int previousCheckpointNumber = checkpointNumber - 1;
        if (checkpointTimes.ContainsKey(previousCheckpointNumber))
        {
            float elapsedTime = currentTime - checkpointTimes[previousCheckpointNumber];
            Debug.Log($"Time between Checkpoint {previousCheckpointNumber} and Checkpoint {checkpointNumber}: {elapsedTime} seconds");
            StartCoroutine(UploadCheckpointData(previousCheckpointNumber, checkpointNumber, elapsedTime));
        }
    }

    IEnumerator UploadCheckpointData(int startCheckpoint, int endCheckpoint, float timeDifference)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string jsonData = $"{{\"FromCheckpoint\":{startCheckpoint}, \"ToCheckpoint\":{endCheckpoint}, \"Time\":{timeDifference}, \"Timestamp\":\"{timestamp}\"}}";
        UnityWebRequest www = UnityWebRequest.Put(firebaseUrl, jsonData);
        www.method = UnityWebRequest.kHttpVerbPOST;
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error uploading: {www.error}");
        }
        else
        {
            Debug.Log("Upload successful");
        }
    }
    //public static CheckpointManager Instance;

    //private Dictionary<int, float> checkpointTimes = new Dictionary<int, float>();
    //private string firebaseUrl = "https://csci526-19391-default-rtdb.firebaseio.com/Checkpoints.json";

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject); // 确保对象在加载新场景时不被销毁
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject); // 如果已有实例存在且不是当前实例，则销毁当前gameObject
    //    }
    //}

    //public void CheckpointTriggered(int checkpointNumber)
    //{
    //    if (!checkpointTimes.ContainsKey(checkpointNumber))
    //    {
    //        checkpointTimes[checkpointNumber] = Time.time;
    //        Debug.Log("Checkpoint " + checkpointNumber + " triggered at " + Time.time);
    //    }

    //    // Optionally, check for sequence and upload the time difference
    //    // Example: If checkpoints 1 and 2 are triggered, calculate and upload
    //    if (checkpointNumber > 1 && checkpointTimes.ContainsKey(checkpointNumber - 1))
    //    {
    //        float timeDifference = checkpointTimes[checkpointNumber] - checkpointTimes[checkpointNumber - 1];
    //        UploadCheckpointData(checkpointNumber - 1, checkpointNumber, timeDifference);
    //    }
    //}

    //private void UploadCheckpointData(int startCheckpoint, int endCheckpoint, float timeDifference)
    //{
    //    StartCoroutine(UploadTimeToFirebase(startCheckpoint, endCheckpoint, timeDifference));
    //}

    //IEnumerator UploadTimeToFirebase(int startCheckpoint, int endCheckpoint, float timeDifference)
    //{
    //    string dataJson = $"{{\"FromCheckpoint\":{startCheckpoint}, \"ToCheckpoint\":{endCheckpoint}, \"Time\":{timeDifference.ToString("F2")}}}";
    //    using (UnityWebRequest www = UnityWebRequest.Put(firebaseUrl, dataJson))
    //    {
    //        www.SetRequestHeader("Content-Type", "application/json");
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.LogError(www.error);
    //        }
    //        else
    //        {
    //            Debug.Log("Time data uploaded successfully: " + dataJson);
    //        }
    //    }
    //    //using (UnityWebRequest www = UnityWebRequest.Put(firebaseUrl + $"/{startCheckpoint}_to_{endCheckpoint}.json", dataJson))
    //}
}
