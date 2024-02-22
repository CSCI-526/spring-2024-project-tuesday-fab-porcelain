using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{

    // 杆的两端对象
    public GameObject playerA;
    public GameObject playerB;

    // 刚体
    Rigidbody2D mRigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        // 获取刚体组件
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 当上箭头松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if(Input.GetKeyUp(KeyCode.UpArrow)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, 150.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当左箭头按下时, 给playerA 的位置施加一个X 轴上的-5 单位的力
        if(Input.GetKey(KeyCode.LeftArrow)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当右箭头按下时, 给playerA 的位置施加一个X 轴上的5 单位的力
        if(Input.GetKey(KeyCode.RightArrow)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }



        // 当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if(Input.GetKeyUp(KeyCode.W)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, 150.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当A 按下时, 给playerA 的位置施加一个X 轴上的-5 单位的力
        if(Input.GetKey(KeyCode.A)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当D 按下时, 给playerA 的位置施加一个X 轴上的5 单位的力
        if(Input.GetKey(KeyCode.D)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }


    }
}
