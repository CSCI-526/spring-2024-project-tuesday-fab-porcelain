using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBehavior : MonoBehaviour
{
    public GameObject playerA;
    public GameObject playerB;
    public LineRenderer rope;
    public float ropeLength = 5.0f; 
    public float ropeSegmentLength = 0.025f; 
    public int ropeSegmentCount = 10; 
    private Vector3[] ropePositions;

    
    
    //法2///////////////////////////////////////////////////////////////////////////////////////////////////////
   
    //public LineRenderer rope;
    //public int ropeSegmentCount = 10; // 绳子段的数量
    //private Vector3[] ropePositions; // 绳子各个节点的位置

    //// 节点之间的距离
    //public float nodeDistance = 0.5f;
    //// 弹簧力度
    //public float springForce = 50f;
    //// 阻尼
    //public float damping = 5f;





    public float jumpForce = 260f;

    // 刚体
    Rigidbody2D mRigidbody2D;
    // 判断小球是否在地面上,从而决定是否能够起跳
    public bool isPlayerAOnGround = false;
    public bool isPlayerBOnGround = false;
    // 画出射线检测的调试线
    bool drawDebugLine = true;

    void Start()
    {
        //法2///////////////////////////////////////////////////////////////////////////////////////////////////////
        //CreateRope();
        ropePositions = new Vector3[ropeSegmentCount];
        // 获取刚体组件
        mRigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // 更新小球位置
        UpdatePlayerPositions();
        // 更新绳子位置
        UpdateRopePositions();
    }

    void FixedUpdate()
    {

        // 当A键 按下时, 给playerA的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.A))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当D键 按下时, 给playerBlue 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.D))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当左箭头 按下时, 给playerB的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当右箭头 按下时, 给playerB 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }
    }

    void UpdatePlayerPositions()
    {

        //检测玩家是否在地面上,决定是否能够起跳
        isPlayerBOnGround = Raycast(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));
        isPlayerAOnGround = Raycast(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));

        //// 控制 playerA 小球移动
        //if (Input.GetKey(KeyCode.A))
        //{
        //    playerA.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    playerA.transform.Translate(5f * Time.deltaTime, 0f, 0f);
        //}
        //当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (Input.GetKeyUp(KeyCode.W) && isPlayerAOnGround)
            {
                //Debug.Log("Will add force to player A");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerA.transform.position, ForceMode2D.Force);
            }
        }
        //if (Input.GetKey(KeyCode.S))
        //{
        //    playerA.transform.Translate(0f, -5f * Time.deltaTime, 0f);
        //}

        //// 控制 playerB 小球移动
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    playerB.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    playerB.transform.Translate(5f * Time.deltaTime, 0f, 0f);
        //}
        // 当上箭头 松开时, 给playerB 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) && isPlayerBOnGround)
            {
                //Debug.Log("Will add force to player Green");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerB.transform.position, ForceMode2D.Force);
            }
        }
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    playerB.transform.Translate(0f, -5f * Time.deltaTime, 0f);
        //}
    }

    RaycastHit2D Raycast(Vector2 sourcePosition, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        // 使用Physics2D.Raycast 发射射线检测脚下是否为地面
        RaycastHit2D hit = Physics2D.Raycast(sourcePosition, rayDirection, length, layer);
        // 绘制检测射线
        Color color = hit ? Color.red : Color.green;

        if (drawDebugLine)
        {
            Debug.DrawRay(sourcePosition, rayDirection * length, color);
        }
        return hit;
    }

    //void UpdateRopePositions()
    //{
    //    ropePositions[0] = playerA.transform.position;
    //    ropePositions[ropeSegmentCount - 1] = playerB.transform.position;

    //    // 计算绳子各个节点的位置
    //    for (int i = 1; i < ropeSegmentCount - 1; i++)
    //    {
    //        float t = i / (float)(ropeSegmentCount - 1);
    //        ropePositions[i] = Vector3.Lerp(playerA.transform.position, playerB.transform.position, t);
    //        ropePositions[i] += new Vector3(0, Mathf.Sin(Time.time * 5f + i) * 0.1f, 0); // 添加绳子的柔软性效果
    //    }

    //    // 更新 Line Renderer 的位置
    //    rope.positionCount = ropeSegmentCount;
    //    rope.SetPositions(ropePositions);
    //}


    //void UpdateRopePositions()
    //{
    //    ropePositions[0] = playerA.transform.position;
    //    ropePositions[ropeSegmentCount - 1] = playerB.transform.position;

    //    // 计算绳子各个节点的位置
    //    for (int i = 1; i < ropeSegmentCount - 1; i++)
    //    {
    //        float t = i / (float)(ropeSegmentCount - 1);
    //        Vector3 interpolatedPosition = Vector3.Lerp(playerA.transform.position, playerB.transform.position, t);
    //        // 添加绳子的柔软性效果
    //        interpolatedPosition += new Vector3(0, Mathf.Sin(Time.time * 5f + i) * 0.1f, 0);
    //        ropePositions[i] = interpolatedPosition;
    //    }

    //    // 通过曲线模拟调整绳子形状
    //    for (int i = 0; i < ropePositions.Length; i++)
    //    {
    //        Vector3 point = ropePositions[i];
    //        point.y -= Mathf.Abs(Mathf.Cos((i * 0.1f) + (Time.time * 5))) * 0.1f;
    //        ropePositions[i] = point;
    //    }

    //    // 更新 Line Renderer 的位置
    //    rope.positionCount = ropePositions.Length;
    //    rope.SetPositions(ropePositions);
    //}


    void UpdateRopePositions()
    {
        // 更新绳子两端位置
        ropePositions[0] = playerA.transform.position;
        ropePositions[ropeSegmentCount - 1] = playerB.transform.position;

        // 计算绳子各个节点的位置
        for (int i = 1; i < ropeSegmentCount - 1; i++)
        {
            // 计算节点位置的插值
            float t = i / (float)(ropeSegmentCount - 1);
            Vector3 interpolatedPosition = Vector3.Lerp(playerA.transform.position, playerB.transform.position, t);

            // 添加绳子的柔软性效果
            interpolatedPosition += new Vector3(0, Mathf.Sin(Time.time * 5f + i) * 0.1f, 0);
            ropePositions[i] = interpolatedPosition;
        }

        // 通过曲线模拟调整绳子形状
        for (int i = 0; i < ropePositions.Length; i++)
        {
            Vector3 point = ropePositions[i];
            point.y -= Mathf.Abs(Mathf.Cos((i * 0.1f) + (Time.time * 5))) * 0.1f;
            ropePositions[i] = point;
        }

        // 更新 Line Renderer 的位置
        rope.positionCount = ropePositions.Length;
        rope.SetPositions(ropePositions);
    }






    //法2///////////////////////////////////////////////////////////////////////////////////////////////////////
    //void CreateRope()
    //{
    //    for (int i = 0; i < ropeSegmentCount; i++)
    //    {
    //        GameObject node = new GameObject("RopeNode_" + i);
    //        node.transform.position = Vector3.Lerp(playerA.transform.position, playerB.transform.position, i / (float)(ropeSegmentCount - 1));
    //        node.transform.parent = transform;
    //    }
    //}

    //void UpdateRopePositions()
    //{
    //    for (int i = 0; i < ropeSegmentCount; i++)
    //    {
    //        Transform node = transform.GetChild(i);
    //        Vector3 force = Vector3.zero;

    //        if (i == 0)
    //            force = playerA.transform.position - node.position;
    //        else if (i == ropeSegmentCount - 1)
    //            force = playerB.transform.position - node.position;
    //        else
    //        {
    //            Vector3 forceLeft = transform.GetChild(i - 1).position - node.position;
    //            Vector3 forceRight = transform.GetChild(i + 1).position - node.position;
    //            force = (forceLeft + forceRight) / 2f;
    //        }

    //        Vector3 acceleration = force * (springForce / nodeDistance);
    //        Vector3 dampingForce = -damping * node.GetComponent<Rigidbody>().velocity;

    //        node.GetComponent<Rigidbody>().velocity += (acceleration + dampingForce) * Time.deltaTime;
    //    }

    //    for (int i = 0; i < ropeSegmentCount; i++)
    //    {
    //        ropePositions[i] = transform.GetChild(i).position;
    //    }

    //    rope.positionCount = ropePositions.Length;
    //    rope.SetPositions(ropePositions);
    //}


}

