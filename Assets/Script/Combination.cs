using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combination : MonoBehaviour
{

    public GameObject playerA;
    public GameObject playerB;
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

    void CombinationSwitch()
    {
        //TODO: add more combinations
        if(curCombination == 1){
            Instantiate(combinationPrefab, (playerA.transform.position + playerB.transform.position) / 2, playerA.transform.rotation, this.transform);
            gameObject.AddComponent<Rigidbody2D>();
        }

    }

    void RodUpdate()
    {
        // fetch rigidbody
        Rigidbody2D mRigidbody2D = GetComponent<Rigidbody2D>();

        // 当上箭头松开时, 给playerB 的位置施加一个Y 轴上的150 单位的力
        if(Input.GetKeyUp(KeyCode.UpArrow)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, 150.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当左箭头按下时, 给playerB 的位置施加一个X 轴上的-5 单位的力
        if(Input.GetKey(KeyCode.LeftArrow)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // 当右箭头按下时, 给playerB 的位置施加一个X 轴上的5 单位的力
        if(Input.GetKey(KeyCode.RightArrow)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }


        // 当W 松开时, 给playerA 的位置施加一个Y 轴上的150 单位的力
        if(Input.GetKeyUp(KeyCode.W)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(0.0f, 150.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当A 按下时, 给playerA 的位置施加一个X 轴上的-5 单位的力
        if(Input.GetKey(KeyCode.A)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // 当D 按下时, 给playerA 的位置施加一个X 轴上的5 单位的力
        if(Input.GetKey(KeyCode.D)) {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }
    }
}
