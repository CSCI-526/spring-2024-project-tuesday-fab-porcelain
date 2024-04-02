using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public CheckpointManager checkpointManager; // 在Inspector中设置这个变量以引用CheckpointManager对象
    public int checkpointNumber; // 在每个检查点的Inspector中设置这个变量以指定检查点编号

    private void OnTriggerEnter2D(Collider2D other) // 对于2D游戏使用这个方法
    // private void OnTriggerEnter(Collider other) // 对于3D游戏，使用这个方法并注释掉上面的方法
    {
        if (other.CompareTag("Combination")) // 确保你的玩家对象有一个标签叫"Player"
        {
            // 调用CheckpointManager的CheckpointTriggered方法并传递当前检查点编号
            checkpointManager.CheckpointTriggered(checkpointNumber);
        }
    }
}
