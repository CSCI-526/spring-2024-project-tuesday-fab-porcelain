using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatFallingBalls : MonoBehaviour
{
    // 设置小球预制件
    public GameObject BallPrefab;
    private bool Falling = true;
    // 生成位置
    public Vector3 FallingPosition = new Vector3(-30.11f, 15.66f, 0); 

    private void Start()
    {
        // 每2秒调用一次; (调用,调用起始时间, 调用时间间隔)
        InvokeRepeating("FllingBall", 0f, 4f);
    }

    void FllingBall()
    {
        //Debug.Log("Spawning ball at " + FallingPosition);
        //Instantiate(BallPrefab, FallingPosition, Quaternion.identity);
        // 只有当Falling为true时才生成小球
        if (Falling) 
        {
            //Debug.Log("Spawning ball at " + FallingPosition);
            Instantiate(BallPrefab, FallingPosition, Quaternion.identity);
        }

    }

    public void StopFlling() {
        //Falling = false;
        Falling = !Falling;
    }



}
