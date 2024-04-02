using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameDataCollector : MonoBehaviour
{
    public static GameDataCollector Instance;

    // 关卡完成时间
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
    }



    public void ExportDataToCSV(string levelName, string filePath)
    {
        StringBuilder csvContent = new StringBuilder();

        // 如果文件不存在，添加CSV头部
        if (!File.Exists(filePath))
        {
            // 仅在文件首次创建时添加表头
            csvContent.AppendLine("LevelName,CompletionTime");
            //csvContent.AppendLine("LevelName,Material,AverageUsageTime");
        }


        //// 导出关卡完成时间
        //foreach (var item in levelCompletionTimes)
        //{
        //    // 对于每个关卡，导出所有尝试的完成时间
        //    foreach (var time in item.Value)
        //    {
        //        csvContent.AppendLine($"{item.Key},{time}");
        //    }
        //}

        //// 导出指定关卡的完成时间
        //var times = levelCompletionTimes[levelName];
        //foreach (var time in times)
        //{
        //    csvContent.AppendLine($"{levelName},{time}");
        //}

        // 检查是否有该关卡的完成时间记录
        if (levelCompletionTimes.ContainsKey(levelName))
        {
            // 只获取最后一次完成时间
            float lastCompletionTime = levelCompletionTimes[levelName].Last();
            csvContent.AppendLine($"{levelName},{lastCompletionTime}");
        }

        File.AppendAllText(filePath, csvContent.ToString());

    }
}
