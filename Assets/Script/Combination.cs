using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Combination : MonoBehaviour
{

    public GameObject playerA;
    public GameObject playerB;
    // 显示两个小球的位置, 调试用
    public Vector3 playerAPosition;
    public Vector3 playerBPosition;

    //用于统计材质使用次数
    public int stickUsageCount = 0;
    public int ropeUsageCount = 0;
    public int springUsageCount = 0;

    private string csvFilePath = Path.Combine(Application.dataPath, "Data", "MaterialCountData.csv");


    // 左边力
    public float LeftForce = -10f;
    // 右边力
    public float RightForce = 10f;
    // 起跳力度
    public float jumpForce = 20f;

    public Face AFace;
    public Face BFace;

    private float aDownPressTime;
    private float bDownPressTime;
    
    public float jumpMaxTimes = 3;
    // 刚体
    Rigidbody2D rigidbody2DPlayerA;
    Rigidbody2D rigidbody2DPlayerB;
    //A-B之间画线
    public LineRenderer lineRenderer;
    //存储AB之间的距离
    public float distanceAB = 0.0f;

    public Vector2 speedLimitA;
    public Vector2 speedLimitB;


    public float fallSpeed = 10;
    List<GameObject> ropeParts = new List<GameObject>();

    // 判断小球是否在地面上,从而决定是否能够起跳
    public bool isPlayerAOnGround = false;
    public bool isPlayerBOnGround = false;
    // 画出射线检测的调试线
    bool drawDebugLine = true;
    //连接物索引
    public int connectorIndex = 0; 


    void Start()
    {
        csvFilePath = Path.Combine(Application.dataPath, "Data", "MaterialCountData.csv");
        //初始化玩家A&B; 初始化绘制连接线LineRenderer
        rigidbody2DPlayerA = playerA.GetComponent<Rigidbody2D>();
        rigidbody2DPlayerB = playerB.GetComponent<Rigidbody2D>();
        lineRenderer = playerA.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        ////因为最开始是使用木棍，所以木棍的使用次数初始就为1
        //Debug.Log("Stick Usage Count: " + stickUsageCount + " | Rope Usage Count: " + ropeUsageCount + " | Spring Usage Count: " + springUsageCount);
        stickUsageCount = 1;
        Debug.Log("Stick Usage Count: " + stickUsageCount + " | Rope Usage Count: " + ropeUsageCount + " | Spring Usage Count: " + springUsageCount);

        

        //调用切换功能,初始调用棍子链接
        CombinationSwitch();
        switchToStick();



    }

    void Update()
    {
        //调用切换功能,跳跃功能
        CombinationSwitch();
        JumpHandler();

        hookHandler();


        lineRenderer.SetPosition(0, playerA.transform.position);
        lineRenderer.SetPosition(1, playerB.transform.position);

        
    }
    void hookHandler()
    {


        // 检测玩家B是否在墙边
        RaycastHit2D hit0 = Raycast(playerB.transform.position - new Vector3(-0.55f, 0.0f, 0.0f), new Vector2(1, 0), 0.2f, -1);
        bool isPlayerBOnwallL = IsOnWall(hit0);

        RaycastHit2D hit1 = Raycast(playerB.transform.position - new Vector3(0.55f, 0.0f, 0.0f), new Vector2(-1, 0), 0.2f, -1);
        bool isPlayerBOnwallR = IsOnWall(hit1);

        // 检测玩家A是否在墙边
        RaycastHit2D hit2 = Raycast(playerA.transform.position - new Vector3(-0.55f, 0.0f, 0.0f), new Vector2(1, 0), 0.2f, -1);
        bool isPlayerAOnwallL = IsOnWall(hit2);

        RaycastHit2D hit3 = Raycast(playerA.transform.position - new Vector3(0.55f, 0.0f, 0.0f), new Vector2(-1, 0), 0.2f, -1);
        bool isPlayerAOnwallR = IsOnWall(hit3);


        if ((isPlayerBOnwallL || isPlayerBOnwallR) && Input.GetKey(KeyCode.M))
        {
            // rigidbody2DPlayerB.AddForceAtPosition(new Vector2(RightForce*100, 0.0f), playerB.transform.position, ForceMode2D.Force);


            rigidbody2DPlayerB.constraints = RigidbodyConstraints2D.FreezePosition;

        }
        else
        {

            rigidbody2DPlayerB.constraints = RigidbodyConstraints2D.None;

        }
        if ((isPlayerAOnwallL || isPlayerAOnwallR) && Input.GetKey(KeyCode.V))
        {
            // rigidbody2DPlayerB.AddForceAtPosition(new Vector2(RightForce*100, 0.0f), playerB.transform.position, ForceMode2D.Force);


            rigidbody2DPlayerA.constraints = RigidbodyConstraints2D.FreezePosition;

        }
        else
        {

            rigidbody2DPlayerA.constraints = RigidbodyConstraints2D.None;

        }
    }

    //检测玩家是否接触到墙
    bool IsOnWall(RaycastHit2D hit)
    {
        if (hit != null)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    return true;
                }
            }

        }

        return false;
    }
    void FixedUpdate()
    {
        //调用左右移动功能
        MoveHandler();

        stickCollider.transform.right = (playerB.transform.position - playerA.transform.position).normalized;

        rigidbody2DPlayerA.centerOfMass=Vector3.zero;
        rigidbody2DPlayerB.centerOfMass = Vector3.zero;


        SpeedLimit();


        Physics2D.gravity = new Vector2(0, -fallSpeed);
    }

    void SpeedLimit()
    {
        Vector2 aVel = rigidbody2DPlayerA.velocity;
        Vector2 bVel = rigidbody2DPlayerB.velocity;

        aVel.x = Mathf.Clamp(aVel.x, -speedLimitA.x, speedLimitA.x);
        aVel.y = Mathf.Clamp(aVel.y, -speedLimitA.y, speedLimitA.y);

        rigidbody2DPlayerA.velocity = aVel;

        bVel.x = Mathf.Clamp(bVel.x, -speedLimitB.x, speedLimitB.x);
        bVel.y = Mathf.Clamp(bVel.y, -speedLimitB.y, speedLimitB.y);

        rigidbody2DPlayerB.velocity = bVel;
    }
    public GameObject stickCollider;
    //转换到棍子
    void switchToStick()
    {
        //Debug.Log("Switch to STICK mode");
        //画线
        lineRenderer.positionCount = 2;
        //模拟木头颜色
        Color woodColor = new Color(0.72f, 0.52f, 0.04f, 1f);
        lineRenderer.startColor = woodColor;
        lineRenderer.endColor = woodColor;


        stickCollider.SetActive(true);
        stickCollider.transform.right = (playerB.transform.position - playerA.transform.position).normalized;
        //玩家A启用最大距离4,启用距离矫正器,禁用弹簧关节
        playerA.GetComponent<DistanceJoint2D>().enabled = true;
        playerA.GetComponent<DistanceJoint2D>().distance = 4;
        playerA.GetComponent<DistanceJoint2D>().maxDistanceOnly = false;
        playerA.GetComponent<SpringJoint2D>().enabled = false;

        playerB.GetComponent<DistanceJoint2D>().enabled = true;
        playerB.GetComponent<DistanceJoint2D>().distance = 4;
        playerB.GetComponent<DistanceJoint2D>().maxDistanceOnly = false;
        playerB.GetComponent<SpringJoint2D>().enabled = false;

    }
   

    //转换到绳子
    void switchToRope()
    {
        stickCollider.SetActive(false);
        //Debug.Log("Switch to ROPE mode");
        lineRenderer.positionCount = 2;
        //模拟绳子颜色
        Color ropeColor = new Color(0.9f, 0.85f, 0.5f, 1f);
        lineRenderer.startColor = ropeColor;
        lineRenderer.endColor = ropeColor;

        //玩家A启用最大距离4,启用距离矫正器,禁用弹簧关节
        playerA.GetComponent<DistanceJoint2D>().enabled = true;
        playerA.GetComponent<DistanceJoint2D>().distance = 4;
        playerA.GetComponent<DistanceJoint2D>().maxDistanceOnly = true;
        playerA.GetComponent<SpringJoint2D>().enabled = false;


        playerB.GetComponent<DistanceJoint2D>().enabled = true;
        playerB.GetComponent<DistanceJoint2D>().distance = 4;
        playerB.GetComponent<DistanceJoint2D>().maxDistanceOnly = true;
        playerB.GetComponent<SpringJoint2D>().enabled = false;

         
    }

    //转换到弹簧
    void SwitchToSpring()
    {
        stickCollider.SetActive(false);
        //Debug.Log("Switch to SPRING mode");
        lineRenderer.positionCount = 2;
        // 模拟金属颜色
        Color metalColor = new Color(0.75f, 0.75f, 0.8f, 1f);
        lineRenderer.startColor = metalColor;
        lineRenderer.endColor = metalColor;

        //玩家A启用距离4,禁用距离矫正器,启用弹簧关节
        playerA.GetComponent<DistanceJoint2D>().enabled = false;
        playerA.GetComponent<SpringJoint2D>().enabled = true;
        playerA.GetComponent<SpringJoint2D>().distance = 4;

        playerB.GetComponent<DistanceJoint2D>().enabled = false;
        playerB.GetComponent<SpringJoint2D>().enabled = true;
        playerB.GetComponent<SpringJoint2D>().distance = 4;
    }
   
    void CombinationSwitch()
    { 
        if (connectorIndex == 1 || connectorIndex ==2)
        {
            if (Physics2D.Linecast(playerA.transform.position, playerB.transform.position, LayerMask.GetMask("Ground")))
                return;
        }
         
        //按空格切换连接方式
        if (Input.GetKeyDown(KeyCode.Space))
        {
            connectorIndex += 1;
            connectorIndex %= 3;

            switch (connectorIndex)
            {
                case 0: stickUsageCount++; switchToStick(); break;
                case 1: ropeUsageCount++; switchToRope(); break;
                case 2: springUsageCount++; SwitchToSpring(); break;
            }
            // 输出统计结果到控制台
            Debug.Log("Stick Usage Count: " + stickUsageCount + " | Rope Usage Count: " + ropeUsageCount + " | Spring Usage Count: " + springUsageCount);
        }
    }


    void MoveHandler()
    {
        // 当A键 按下时, 给playerA的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody2DPlayerA.AddForceAtPosition(new Vector2(LeftForce, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当D键 按下时, 给playerBlue 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2DPlayerA.AddForceAtPosition(new Vector2(RightForce, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当左箭头 按下时, 给playerB的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2DPlayerB.AddForceAtPosition(new Vector2(LeftForce, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当右箭头 按下时, 给playerB 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2DPlayerB.AddForceAtPosition(new Vector2(RightForce, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }
    }

    private bool aGroundDown;
    private bool bGroundDown;
    void JumpHandler()
    {
        //检测玩家是否在地面上,决定是否能够起跳
        isPlayerBOnGround = Physics2D.RaycastAll(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.GetMask("Ground")).Length > 0;
        isPlayerAOnGround = Physics2D.RaycastAll(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.GetMask("Ground")).Length > 0;
        playerAPosition = playerA.transform.position;
        playerBPosition = playerB.transform.position;
         
        if (isPlayerAOnGround)
        { 
            // 当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
            if (Input.GetKeyUp(KeyCode.W))
            {
                // rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerA.transform.position, ForceMode2D.Impulse);
             
                    // 计算小球B和小球A在X轴上的差值
                    float horiDiff = playerB.transform.position.x - playerA.transform.position.x;

                    // 检查小球B是否在小球A的正上方
                    // 并且小球B和小球A在X轴上的差值绝对值在1以内
                    if (playerB.transform.position.y > playerA.transform.position.y && Mathf.Abs(horiDiff) <= 1)
                    {
                        // 如果小球B在上方且小球A在地面上，则应用2倍跳跃力
                        rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, jumpForce * 2), playerA.transform.position, ForceMode2D.Force);
                    }
                    else
                    {
                        // 否则应用正常跳跃力
                        rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerA.transform.position, ForceMode2D.Force);

                        // print("A");
                    }
                


            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                aDownPressTime = Time.time;
                aGroundDown = true;

            }
            // 按S蓄力跳跃
            if (Input.GetKey(KeyCode.W) && aGroundDown)
            {
                float time = Time.time - aDownPressTime; 
                AFace.SetFaceRed(Mathf.Clamp01(time / 1.5f));
            }
            if (Input.GetKeyUp(KeyCode.W) && aGroundDown)
            {
                float time = Time.time- aDownPressTime;
                float times =Mathf.Clamp01( time / 1.5f) *jumpMaxTimes;
                 
                rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, times* jumpForce), playerA.transform.position, ForceMode2D.Impulse);
                AFace.SetFaceRed(0);
            }
        }
        else
        {
            aGroundDown = false;
            AFace.SetFaceRed(0);
        }

        // 当上箭头 松开时, 给playerB 的位置施加一个Y 轴上的150 单位的力

        if (isPlayerBOnGround)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                // 否则应用正常跳跃力
                rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerB.transform.position, ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                bDownPressTime = Time.time;
                bGroundDown = true;
            }
            if (Input.GetKey(KeyCode.UpArrow) && bGroundDown)
            {
                float time = Time.time - bDownPressTime;
                BFace.SetFaceRed(Mathf.Clamp01(time / 1.5f));
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) &&bGroundDown)
            {
                float time = Time.time - bDownPressTime;
                float times = Mathf.Clamp01(time / 1.5f) * jumpMaxTimes;
                rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, times * jumpForce), playerB.transform.position, ForceMode2D.Impulse);
                BFace.SetFaceRed(0);
            }
        }
        else
        {
            bGroundDown = false;
            BFace.SetFaceRed(0);
        }


    }

    // 射线检测:
    // 从球下方发射一个射线检测是否能起跳, 参数分别是:
    // 发射点
    // 发射射线
    // 发射的射线长度
    // 检测的层
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


    void OnApplicationQuit()
    {
        // 游戏结束时统计数据并写入CSV文件
        WriteStatsToCSV();
    }

    // 统计数据并写入CSV文件
    void WriteStatsToCSV()
    {
        // 如果文件不存在，则创建并写入列标题
        if (!File.Exists(csvFilePath))
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath, false))
            {
                writer.WriteLine("Stick Usage Count,Rope Usage Count,Spring Usage Count");
            }
        }

        // 将统计数据写入CSV文件
        using (StreamWriter writer = new StreamWriter(csvFilePath, true))
        {
            writer.WriteLine($"{stickUsageCount},{ropeUsageCount},{springUsageCount}");
        }
    }
}