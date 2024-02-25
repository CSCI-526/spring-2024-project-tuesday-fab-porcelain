using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{

    // 杆的两端对象
    public GameObject playerA;
    public GameObject playerB;
    public Vector3 playerAPosition;
    public Vector3 playerBPosition;
    //控制跳跃力量
    public float jumpForce = 260f;
    // 刚体
    Rigidbody2D mRigidbody2D;

    //public int curCombination = 1;
    //[SerializeField] private GameObject combinationPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // 获取刚体组件
        mRigidbody2D = GetComponent<Rigidbody2D>();
        //CombinationSwitch();
    }

    //void CombinationSwitch()
    //{
    //    //TODO: add more combinations
    //    if (curCombination == 1)
    //    {
    //        Instantiate(combinationPrefab, (playerA.transform.position + playerB.transform.position) / 2, playerA.transform.rotation, this.transform);
    //        gameObject.AddComponent<Rigidbody2D>();
    //    }
    //}

    //FixedUpdate()是Unity自有函数[固定间隔时间内调用](类似于update函数),这里主要用来处理物理计算
    void FixedUpdate()
    {
        // 当A键按下时, 给playerA 的位置施加一个X 轴上的-5 单位的力
        if (Input.GetKey(KeyCode.A))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当D键 按下时, 给playerA 的位置施加一个X 轴上的5 单位的力
        if (Input.GetKey(KeyCode.D))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当左箭头 按下时, 给playerB 的位置施加一个X 轴上的-5 单位的力
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
    // Update is called once per frame
    void Update()
    {
        // 
        // //TODO: press space to switch combination
        // //TODO: add different update functions for each combination
        // if(curCombination == 1){
        //     RodUpdate();
        // }

        // Debug.Log(playerA.transform.position + "   " + playerB.transform.position);
        playerAPosition = playerA.transform.position;
        playerBPosition = playerB.transform.position;

        // 当 W键 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (playerA.transform.position.y <= -0.62f)
            {
                //Debug.Log("Will add force to player A");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerA.transform.position, ForceMode2D.Force);
            }
        }

        // 当上箭头 松开时, 给playerB 的位置施加一个Y 轴上的150 单位的力
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (playerB.transform.position.y <= -0.62f)
            {
                //Debug.Log("Will add force to player B");
                mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, jumpForce), playerB.transform.position, ForceMode2D.Force);
            }
        }


    }
}