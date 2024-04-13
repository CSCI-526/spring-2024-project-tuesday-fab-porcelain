using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public CheckpointManager checkpointManager; // 在Inspector中设置这个变量以引用CheckpointManager对象
    public int checkpointNumber; // 在每个检查点的Inspector中设置这个变量以指定检查点编号

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PlayerA")
        {
            checkpointManager.CheckpointTriggered(checkpointNumber);
        }
    }
}



