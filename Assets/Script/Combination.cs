using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{

    public GameObject playerA;
    public GameObject playerB;
    // 显示两个小球的位置, 调试用
    public Vector3 playerAPosition;
    public Vector3 playerBPosition;
    // 起跳力度
    public float jumpForce = 260f;

    // 抓地力度
    public float GripForce = -200f;
    // 左边力
    public float LeftForce = -10f;
    // 右边力
    public float RightForce = 10f;
    // 刚体
    Rigidbody2D mRigidbody2D;
    // 判断小球是否在地面上,从而决定是否能够起跳
    public bool isPlayerAOnGround = false;
    public bool isPlayerBOnGround = false;
    // 画出射线检测的调试线
    bool drawDebugLine = true;

    public int curCombination = 1;
    [SerializeField] private GameObject combinationPrefab;


    // Start is called before the first frame update
    void Start()
    {
        CombinationSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: press space to switch combination
        //TODO: add different update functions for each combination
        if(curCombination == 1){
            RodUpdate();
        }
    }

    void FixedUpdate()
    {
        //TODO: press space to switch combination
        //TODO: add different update functions for each combination
        if (curCombination == 1)
        {
            RodFixUpdate();
        }
    }

    void CombinationSwitch()
    {
        //TODO: add more combinations
        if(curCombination == 1){
            Instantiate(combinationPrefab, (playerA.transform.position + playerB.transform.position) / 2, playerA.transform.rotation, this.transform);
            gameObject.AddComponent<Rigidbody2D>();
        }

    }

    void RodFixUpdate()
    {
        // fetch rigidbody
        Rigidbody2D mRigidbody2D = GetComponent<Rigidbody2D>();

        // 当A键 按下时, 给playerA的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.A))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(LeftForce, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当D键 按下时, 给playerBlue 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.D))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(RightForce, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当左箭头 按下时, 给playerB的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(LeftForce, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当右箭头 按下时, 给playerB 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(RightForce, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }
    }

    void RodUpdate()
    {


        // fetch rigidbody
        Rigidbody2D mRigidbody2D = GetComponent<Rigidbody2D>();

        //检测玩家是否在地面上,决定是否能够起跳
        isPlayerBOnGround = Raycast(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));
        isPlayerAOnGround = Raycast(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));

        // Debug.Log(playerA.transform.position + "   " + playerBlue.transform.position);
        playerAPosition = playerA.transform.position;
        playerBPosition = playerB.transform.position;



        // 当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (isPlayerAOnGround)
            {
                //Debug.Log("Will add force to player A");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerA.transform.position, ForceMode2D.Force);
            }
        }


        // 当S 松开时, 给playerA 的位置施加一个Y 轴上 一个向下 GripForce 单位的力
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (isPlayerAOnGround)
            {
                //Debug.Log("Will add force to player A");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, GripForce), playerA.transform.position, ForceMode2D.Force);
            }
        }




        // 当上箭头 松开时, 给playerB 的位置施加一个Y 轴上的 150 单位的力
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (isPlayerBOnGround)
            {
                //Debug.Log("Will add force to player Green");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerB.transform.position, ForceMode2D.Force);
            }
        }

        // 当下箭头 松开时, 给playerB 的位置施加一个Y 轴上一个向下 GripForce 单位的力
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (isPlayerBOnGround)
            {
                //Debug.Log("Will add force to player Green");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, GripForce), playerB.transform.position, ForceMode2D.Force);
            }
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
