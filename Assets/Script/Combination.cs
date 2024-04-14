using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
// using UnityEditor.SceneManagement;
using UnityEngine;

public class Combination : MonoBehaviour
{

    public GameObject playerA;
    public GameObject playerB;
    // 显示两个小球的位置, 调试用
    public Vector3 playerAPosition;
    public Vector3 playerBPosition;
    // 左边力
    public float LeftForce = -10f;
    // 右边力
    public float RightForce = 10f;
    // 起跳力度
    public float jumpForce = 220f;

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
    // 判断玩家A是否能使用固定
    public bool onfixA = false;

    // 判断玩家B是否能使用固定
    public bool onfixB = false;

    //力放大倍数
    private float forcescale = 1.0f;
    public float scalefactor = 3.0f;

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


        // 检测玩家B是否能挂住
        RaycastHit2D hit0 = Raycast(playerB.transform.position - new Vector3(-0.55f, 0.0f, 0.0f), new Vector2(1, 0), 0.2f, -1);
        bool isPlayerBOnwallL = IsOnWall(hit0);

        RaycastHit2D hit1 = Raycast(playerB.transform.position - new Vector3(0.55f, 0.0f, 0.0f), new Vector2(-1, 0), 0.2f, -1);
        bool isPlayerBOnwallR = IsOnWall(hit1);

        RaycastHit2D hit2 = Raycast(playerB.transform.position - new Vector3(0.0f, -0.55f, 0.0f), new Vector2(0, 1), 0.2f, -1);
        bool isPlayerBOntop = IsOnWall(hit2);

        RaycastHit2D hit3 = Raycast(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, -1);
        bool isPlayerBOnground = IsOnWall(hit3);

        bool isPlayBcanfix = isPlayerBOnwallL || isPlayerBOnwallR || isPlayerBOntop || isPlayerBOnground;

        // 检测玩家A是否能挂住
        RaycastHit2D hit4 = Raycast(playerA.transform.position - new Vector3(-0.55f, 0.0f, 0.0f), new Vector2(1, 0), 0.2f, -1);
        bool isPlayerAOnwallL = IsOnWall(hit4);

        RaycastHit2D hit5 = Raycast(playerA.transform.position - new Vector3(0.55f, 0.0f, 0.0f), new Vector2(-1, 0), 0.2f, -1);
        bool isPlayerAOnwallR = IsOnWall(hit5);

        RaycastHit2D hit6 = Raycast(playerA.transform.position - new Vector3(0.0f, -0.55f, 0.0f), new Vector2(0, 1), 0.2f, -1);
        bool isPlayerAOntop = IsOnWall(hit6);

        RaycastHit2D hit7 = Raycast(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, -1);
        bool isPlayerAOnground = IsOnWall(hit7);

        bool isPlayAcanfix = isPlayerAOnwallL || isPlayerAOnwallR || isPlayerAOntop || isPlayerAOnground;


        if (Input.GetKey(KeyCode.M) && isPlayBcanfix)
        {
            // rigidbody2DPlayerB.AddForceAtPosition(new Vector2(RightForce*100, 0.0f), playerB.transform.position, ForceMode2D.Force);


            rigidbody2DPlayerB.constraints = RigidbodyConstraints2D.FreezePosition;
            onfixB = true;

        }
        else
        {

            rigidbody2DPlayerB.constraints = RigidbodyConstraints2D.None;
            onfixB = false;

        }
        if (Input.GetKey(KeyCode.V) && isPlayAcanfix)
        {
            // rigidbody2DPlayerB.AddForceAtPosition(new Vector2(RightForce*100, 0.0f), playerB.transform.position, ForceMode2D.Force);


            rigidbody2DPlayerA.constraints = RigidbodyConstraints2D.FreezePosition;
            onfixA = true;

        }
        else
        {

            rigidbody2DPlayerA.constraints = RigidbodyConstraints2D.None;
            onfixA = false;

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

        rigidbody2DPlayerA.centerOfMass = Vector3.zero;
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
    // void SwitchToSpring()
    // {
    //     stickCollider.SetActive(false);
    //     //Debug.Log("Switch to SPRING mode");
    //     lineRenderer.positionCount = 2;
    //     // 模拟金属颜色
    //     Color metalColor = new Color(0.75f, 0.75f, 0.8f, 1f);
    //     lineRenderer.startColor = metalColor;
    //     lineRenderer.endColor = metalColor;

    //     //玩家A启用距离4,禁用距离矫正器,启用弹簧关节
    //     playerA.GetComponent<DistanceJoint2D>().enabled = false;
    //     playerA.GetComponent<SpringJoint2D>().enabled = true;
    //     playerA.GetComponent<SpringJoint2D>().distance = 4;

    //     playerB.GetComponent<DistanceJoint2D>().enabled = false;
    //     playerB.GetComponent<SpringJoint2D>().enabled = true;
    //     playerB.GetComponent<SpringJoint2D>().distance = 4;
    // }

    void CombinationSwitch()
    {
        if (connectorIndex == 1 || connectorIndex == 2)
        {
            if (Physics2D.Linecast(playerA.transform.position, playerB.transform.position, LayerMask.GetMask("Ground")))
                return;
        }

        //按空格切换连接方式
        if (Input.GetKeyDown(KeyCode.Y))
        {
            connectorIndex += 1;
            connectorIndex %= 3;

            switch (connectorIndex)
            {
                case 0: switchToStick(); break;
                case 1: switchToRope(); break;
                    // case 2: SwitchToSpring(); break;
            }
        }
    }


    void MoveHandler()
    {
        // 当A键 按下时, 给playerA的位置施加一个X 轴上的-5 单位的力


        if (onfixA == true || onfixB == true)
        {
            //当A或者B固定时，左右力放大为3倍
            forcescale = scalefactor;
        }
        else
        {
            forcescale = 1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {

            rigidbody2DPlayerA.AddForceAtPosition(new Vector2(LeftForce, 0.0f) * forcescale, playerA.transform.position, ForceMode2D.Force);
        }

        // 当D键 按下时, 给playerBlue 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody2DPlayerA.AddForceAtPosition(new Vector2(RightForce, 0.0f) * forcescale, playerA.transform.position, ForceMode2D.Force);
        }

        // 当左箭头 按下时, 给playerB的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2DPlayerB.AddForceAtPosition(new Vector2(LeftForce, 0.0f) * forcescale, playerB.transform.position, ForceMode2D.Force);
        }

        // 当右箭头 按下时, 给playerB 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2DPlayerB.AddForceAtPosition(new Vector2(RightForce, 0.0f) * forcescale, playerB.transform.position, ForceMode2D.Force);
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

        // bool bool1=(onfixA==false&&isPlayerAOnGround) || (onfixA==false&&onfixB==true);
        // Debug.Log("bool:"+bool1);
        // Debug.Log("onfixA:"+onfixA);
        // Debug.Log("onfixB:"+onfixB);

        if (onfixA == false && isPlayerAOnGround)
        {
            // 防止B固定的时候A无限跳
            //当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
            if (Input.GetKeyUp(KeyCode.W))//!!!!!!!!!!!!!!!!!!!!!    GetKeyUp====》GetKeyDown
            {

                // 先计算小球B和小球A在X轴和Y轴上的差值
                float horiDiff = playerB.transform.position.x - playerA.transform.position.x;
                float vertDiff = playerB.transform.position.y - playerA.transform.position.y;

                // 判断是否应该应用加倍跳跃力
                bool shouldApplyBoost = vertDiff > 0 && Mathf.Abs(horiDiff) <= 1;

                // 应用跳跃力，如果满足条件则应用加倍力
                float appliedForce = shouldApplyBoost ? jumpForce * 3 : jumpForce;
                rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, appliedForce), playerA.transform.position, ForceMode2D.Impulse);


            }

            if (Input.GetKeyDown(KeyCode.W)) //改成W试一下
            {
                aDownPressTime = Time.time;
                aGroundDown = true;

            }
            if (Input.GetKey(KeyCode.W) && aGroundDown)   //改成W试一下
            {
                float time = Time.time - aDownPressTime;
                AFace.SetFaceRed(Mathf.Clamp01(time / 1.5f));
            }
            if (Input.GetKeyUp(KeyCode.W) && aGroundDown)  //改成W试一下
            {
                float time = Time.time - aDownPressTime;
                float times = Mathf.Clamp01(time / 1.5f) * jumpMaxTimes;

                rigidbody2DPlayerA.AddForceAtPosition(new Vector2(0.0f, times * jumpForce), playerA.transform.position, ForceMode2D.Impulse);
                AFace.SetFaceRed(0);
            }
        }
        else
        {
            aGroundDown = false;
            AFace.SetFaceRed(0);
        }

        // 当上箭头 松开时, 给playerB 的位置施加一个Y 轴上的150 单位的力

        if ((onfixB == false && isPlayerBOnGround))
        {
            // 防止A固定的时候B无限跳
            if (Input.GetKeyUp(KeyCode.UpArrow))//!!!!!!!!!!!!!!!!!!!!!    GetKeyUp====》GetKeyDown
            {

                // 先计算小球B和小球A在X轴和Y轴上的差值
                float horiDiff = playerB.transform.position.x - playerA.transform.position.x;
                float vertDiff = playerB.transform.position.y - playerA.transform.position.y;

                // 判断是否应该应用加倍跳跃力
                bool shouldApplyBoost = vertDiff < 0 && Mathf.Abs(horiDiff) <= 1;

                // 应用跳跃力，如果满足条件则应用加倍力
                float appliedForce = shouldApplyBoost ? jumpForce * 3 : jumpForce;
                rigidbody2DPlayerB.AddForceAtPosition(new Vector2(0.0f, appliedForce), playerB.transform.position, ForceMode2D.Impulse);



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
            if (Input.GetKeyUp(KeyCode.UpArrow) && bGroundDown)
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
}