using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    // 抓地力度
    public float GripForce = -200f;
    // 左边力
    public float LeftForce = -10f;
    // 右边力
    public float RightForce = 10f;
    // 起跳力度
    public float jumpForce = 260f;
    // 刚体
    Rigidbody2D rigidbody2DPlayerA;
    Rigidbody2D rigidbody2DPlayerB;
    //A-B之间画线
    public LineRenderer lineRenderer;
    //存储AB之间的距离
    public float distanceAB = 0.0f;
    ////存储抛物线方程
    //public double[] abc = new double[] { 0, 0, 0 };
    //关节对象&绳子起点&终点对象
    public GameObject articulation;
    GameObject ropeStart;
    GameObject ropeEnd;

    //~~~~~~
    //public GameObject stick;

    public Transform stick;


    List<GameObject> ropeParts = new List<GameObject>();

    // 判断小球是否在地面上,从而决定是否能够起跳
    public bool isPlayerAOnGround = false;
    public bool isPlayerBOnGround = false;
    // 画出射线检测的调试线
    bool drawDebugLine = true;
    //连接物索引
    public int connectorIndex = 0;
    [SerializeField] private GameObject combinationPrefab;



    void Start()
    {
        //初始化玩家A&B; 初始化绘制连接线LineRenderer
        rigidbody2DPlayerA = playerA.GetComponent<Rigidbody2D>();
        rigidbody2DPlayerB = playerB.GetComponent<Rigidbody2D>();
        lineRenderer = playerA.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
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
    //构建连接体
    void makeRope()
    {
        GameObject lastPart = null;
        for (int i = 0; i < 16; i++)
        {
            //创建预制体实例,把单个绳子单个部件给part
            GameObject part = Instantiate(articulation);
            //给部件添加物理约束
            part.AddComponent<DistanceJoint2D>();

            //测试~~~~~~~~~~~~~~
            //part.AddComponent<Rigidbody2D>();
            //part.AddComponent<BoxCollider2D>();


            //设置重力为0.01&受重力影响0.1
            part.GetComponent<Rigidbody2D>().mass = 0.01f;
            part.GetComponent<Rigidbody2D>().gravityScale = 0.1f;

            if (lastPart == null)
            {
                lastPart = part;
            }

            if (i == 0)
            {
                ropeStart = part;
            }
            if (i == 15)
            {
                ropeEnd = part;
            }
            //配置部件连接
            if (i != 0)
            {
                DistanceJoint2D partJoint = lastPart.GetComponent<DistanceJoint2D>();
                partJoint.autoConfigureDistance = false;
                partJoint.distance = 0.25f;
                partJoint.connectedBody = part.GetComponent<Rigidbody2D>();
            }
            ropeParts.Add(part);
            lastPart = part;
        }
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


        foreach (var item in ropeParts)
        {
            //item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //item.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
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
    //抛物线逻辑 y=ax2 + bx + c 求b,c
    double[] getParabola(float a, float x1, float y1, float x2, float y2)
    {
        double b = ((y2 - y1) - (a * (x2 * x2 - x1 * x1))) / (x2 - x1);
        double c = y1 - a * x1 * x1 - b * x1;

        return new double[] { b, c };
    }
    //抛物线逻辑 y = ax2 + bx + c 求 y
    double getY(double a, double b, double c, double x)
    {
        return x * x * a + x * b + c;
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
                case 0: switchToStick(); break;
                case 1: switchToRope(); break;
                case 2: SwitchToSpring(); break;
            }
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

    void JumpHandler()
    {
        //检测玩家是否在地面上,决定是否能够起跳
        //isPlayerBOnGround = Raycast(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));
        //isPlayerAOnGround = Raycast(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));
        isPlayerBOnGround = Physics2D.RaycastAll(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.GetMask("Ground")).Length > 0;
        isPlayerAOnGround = Physics2D.RaycastAll(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.GetMask("Ground")).Length > 0;
        playerAPosition = playerA.transform.position;
        playerBPosition = playerB.transform.position;

        // 当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (isPlayerAOnGround)
            {
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
        }

        // 当S 松开时, 给playerA 的位置施加一个Y 轴上 一个向下 GripForce 单位的力
        if (Input.GetKeyUp(KeyCode.S))
        {   //下面代码如果抓地力太大可以取消索引
            //if (isPlayerAOnGround)
            //{
            //    rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerA.transform.position, ForceMode2D.Force);
            //}
            //一个在上一个在下的时候增加的跳跃力

            rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, GripForce), playerA.transform.position, ForceMode2D.Force);
        }

        // 当上箭头 松开时, 给playerB 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (isPlayerBOnGround)
            {
                // 计算小球B和小球A在X轴上的差值
                float horiDiff = playerB.transform.position.x - playerA.transform.position.x;

                // 检查小球B是否在小球A的正下方
                // 并且小球B和小球A在X轴上的差值绝对值在1以内
                if (playerB.transform.position.y < playerA.transform.position.y && Mathf.Abs(horiDiff) <= 1)
                {
                    // 如果小球B在下方且小球B在地面上，则应用2倍跳跃力
                    rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, jumpForce * 2), playerB.transform.position, ForceMode2D.Force);
                }
                else
                {
                    // 否则应用正常跳跃力
                    rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerB.transform.position, ForceMode2D.Force);
                    // print("B");
                  
                }
            }
        }
        // 当下箭头 松开时, 给playerB 的位置施加一个Y 轴上一个向下 GripForce 单位的力
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //下面代码如果抓地力太大可以取消索引
            //if (isPlayerBOnGround)
            //{
            //    rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerB.transform.position, ForceMode2D.Force);
            //}
            rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, GripForce), playerB.transform.position, ForceMode2D.Force);

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
}