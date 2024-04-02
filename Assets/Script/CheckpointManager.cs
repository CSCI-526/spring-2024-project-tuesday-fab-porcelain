using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Dictionary<int, float> checkpointTimes = new Dictionary<int, float>();
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Data/CheckpointTimes.csv");
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "FromCheckpoint,ToCheckpoint,Time\n");
        }
    }

    public void CheckpointTriggered(int checkpointNumber)
    {
        float currentTime = Time.time;

        if (!checkpointTimes.ContainsKey(checkpointNumber))
        {
            // 当前检查点是首次触发，记录时间
            checkpointTimes[checkpointNumber] = currentTime;
        }
        else
        {
            // 更新检查点时间
            checkpointTimes[checkpointNumber] = currentTime;
        }

        // 查找并记录前一个检查点到当前检查点的时间
        int previousCheckpointNumber = checkpointNumber - 1; // 假设检查点编号是连续的
        if (checkpointTimes.ContainsKey(previousCheckpointNumber))
        {
            float elapsedTime = currentTime - checkpointTimes[previousCheckpointNumber];
            Debug.Log($"Time between Checkpoint {previousCheckpointNumber} and Checkpoint {checkpointNumber}: {elapsedTime} seconds");
            File.AppendAllText(filePath, $"{previousCheckpointNumber},{checkpointNumber},{elapsedTime}\n");
        }
    }
}
