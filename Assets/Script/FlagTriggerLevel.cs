using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; // 确保引入了System.IO命名空间

public class FlagTriggerLevel : MonoBehaviour
{
    public GameObject flag; // 在Inspector中设置这个引用

    private bool isLevelCompleted = false; //wy add

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isLevelCompleted && other.gameObject == flag)  // wy add
        {
            isLevelCompleted = true; // wy add
            // 计算完成时间
            float completionTime = Time.time - GameManager.Instance.LevelStartTime;
            // 获取当前关卡名称
            string levelName = SceneManager.GetActiveScene().name;

            // 调用GameDataCollector来记录关卡名称和完成时间
            GameDataCollector.Instance.RecordLevelCompletionTime(levelName, completionTime);

            // 导出数据到CSV文件
            ExportGameData();

            // 重置时间缩放并加载关卡选择界面
            Time.timeScale = 1;
            SceneManager.LoadScene("levelSelection");
        }
    }

    private void ExportGameData()
    {
        // 获取Assets文件夹的路径
        string dataPath = Application.dataPath;

        // 构建目标路径，保存到Assets/Scripts/Data下
        string folderPath = Path.Combine(dataPath, "Data");

        // 确保目标文件夹存在
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // 设置文件名和完整路径
        string fileName = "GameData.csv";
        string filePath = Path.Combine(folderPath, fileName);
        string levelName = SceneManager.GetActiveScene().name; // wy add获取当前关卡名称

        // 调用导出方法
        GameDataCollector.Instance.ExportDataToCSV(levelName, filePath);

        // 输出日志以确认操作
        Debug.Log($"GameData exported to {filePath}");
    }

}
