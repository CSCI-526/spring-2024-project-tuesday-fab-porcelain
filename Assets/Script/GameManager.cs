using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float LevelStartTime { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持游戏管理器在加载新场景时不被销毁
        }
    }

    // 调用这个方法来记录每个关卡的开始时间
    public void RecordLevelStartTime()
    {
        LevelStartTime = Time.time;
    }
}
