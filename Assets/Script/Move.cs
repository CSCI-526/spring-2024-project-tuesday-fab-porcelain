using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //设置小平台上下移动的 起点/终点
    public Vector3 pointDown = new Vector3(14.4f, -5.0f, 0);
    public Vector3 pointUpper = new Vector3(14.4f, 9.63f, 0);
    //平台速度
    public float speed = 2.0f;

    private Vector3 currentTarget;
    void Start()
    {
        // 初始化当前目标为点右
        currentTarget = pointUpper;
    }

    // Update is called once per frame
    void Update()
    {
        // 每帧移动平台,确保xz轴不动
        Vector3 newPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, currentTarget.y, transform.position.z), speed * Time.deltaTime);
        transform.position = newPosition;

        // 检查是否到达目标点
        if (Mathf.Approximately(transform.position.y, currentTarget.y))
        {
            // 如果当前目标是点Upper，则切换到点Down
            if (currentTarget == pointUpper)
            {
                currentTarget = pointDown;
            }
            else // 否则，切换到点Upper
            {
                currentTarget = pointUpper;
            }
        }
    }
}
