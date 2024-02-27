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

    
    
    //��2///////////////////////////////////////////////////////////////////////////////////////////////////////
   
    //public LineRenderer rope;
    //public int ropeSegmentCount = 10; // ���Ӷε�����
    //private Vector3[] ropePositions; // ���Ӹ����ڵ��λ��

    //// �ڵ�֮��ľ���
    //public float nodeDistance = 0.5f;
    //// ��������
    //public float springForce = 50f;
    //// ����
    //public float damping = 5f;





    public float jumpForce = 260f;

    // ����
    Rigidbody2D mRigidbody2D;
    // �ж�С���Ƿ��ڵ�����,�Ӷ������Ƿ��ܹ�����
    public bool isPlayerAOnGround = false;
    public bool isPlayerBOnGround = false;
    // �������߼��ĵ�����
    bool drawDebugLine = true;

    void Start()
    {
        //��2///////////////////////////////////////////////////////////////////////////////////////////////////////
        //CreateRope();
        ropePositions = new Vector3[ropeSegmentCount];
        // ��ȡ�������
        mRigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // ����С��λ��
        UpdatePlayerPositions();
        // ��������λ��
        UpdateRopePositions();
    }

    void FixedUpdate()
    {

        // ��A�� ����ʱ, ��playerA��λ��ʩ��һ��X ���ϵ�-5 ��λ����
        if (Input.GetKey(KeyCode.A))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // ��D�� ����ʱ, ��playerBlue ��λ��ʩ��һ��X ���ϵ�5 ��λ����
        if (Input.GetKey(KeyCode.D))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerA.transform.position, ForceMode2D.Force);
        }

        // �����ͷ ����ʱ, ��playerB��λ��ʩ��һ��X ���ϵ�-5 ��λ����
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(-5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }

        // ���Ҽ�ͷ ����ʱ, ��playerB ��λ��ʩ��һ��X ���ϵ�5 ��λ����
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mRigidbody2D.AddForceAtPosition(new Vector2(5.0f, 0.0f), playerB.transform.position, ForceMode2D.Force);
        }
    }

    void UpdatePlayerPositions()
    {

        //�������Ƿ��ڵ�����,�����Ƿ��ܹ�����
        isPlayerBOnGround = Raycast(playerB.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));
        isPlayerAOnGround = Raycast(playerA.transform.position - new Vector3(0.0f, 0.55f, 0.0f), new Vector2(0, -1), 0.2f, LayerMask.NameToLayer("Ground"));

        //// ���� playerA С���ƶ�
        //if (Input.GetKey(KeyCode.A))
        //{
        //    playerA.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    playerA.transform.Translate(5f * Time.deltaTime, 0f, 0f);
        //}
        //��W �ɿ�ʱ, ��playerA ��λ��ʩ��һ��Y ���ϵ�150 ��λ����
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

        //// ���� playerB С���ƶ�
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    playerB.transform.Translate(-5f * Time.deltaTime, 0f, 0f);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    playerB.transform.Translate(5f * Time.deltaTime, 0f, 0f);
        //}
        // ���ϼ�ͷ �ɿ�ʱ, ��playerB ��λ��ʩ��һ��Y ���ϵ�150 ��λ����
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
        // ʹ��Physics2D.Raycast �������߼������Ƿ�Ϊ����
        RaycastHit2D hit = Physics2D.Raycast(sourcePosition, rayDirection, length, layer);
        // ���Ƽ������
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

    //    // �������Ӹ����ڵ��λ��
    //    for (int i = 1; i < ropeSegmentCount - 1; i++)
    //    {
    //        float t = i / (float)(ropeSegmentCount - 1);
    //        ropePositions[i] = Vector3.Lerp(playerA.transform.position, playerB.transform.position, t);
    //        ropePositions[i] += new Vector3(0, Mathf.Sin(Time.time * 5f + i) * 0.1f, 0); // ������ӵ�������Ч��
    //    }

    //    // ���� Line Renderer ��λ��
    //    rope.positionCount = ropeSegmentCount;
    //    rope.SetPositions(ropePositions);
    //}


    //void UpdateRopePositions()
    //{
    //    ropePositions[0] = playerA.transform.position;
    //    ropePositions[ropeSegmentCount - 1] = playerB.transform.position;

    //    // �������Ӹ����ڵ��λ��
    //    for (int i = 1; i < ropeSegmentCount - 1; i++)
    //    {
    //        float t = i / (float)(ropeSegmentCount - 1);
    //        Vector3 interpolatedPosition = Vector3.Lerp(playerA.transform.position, playerB.transform.position, t);
    //        // ������ӵ�������Ч��
    //        interpolatedPosition += new Vector3(0, Mathf.Sin(Time.time * 5f + i) * 0.1f, 0);
    //        ropePositions[i] = interpolatedPosition;
    //    }

    //    // ͨ������ģ�����������״
    //    for (int i = 0; i < ropePositions.Length; i++)
    //    {
    //        Vector3 point = ropePositions[i];
    //        point.y -= Mathf.Abs(Mathf.Cos((i * 0.1f) + (Time.time * 5))) * 0.1f;
    //        ropePositions[i] = point;
    //    }

    //    // ���� Line Renderer ��λ��
    //    rope.positionCount = ropePositions.Length;
    //    rope.SetPositions(ropePositions);
    //}


    void UpdateRopePositions()
    {
        // ������������λ��
        ropePositions[0] = playerA.transform.position;
        ropePositions[ropeSegmentCount - 1] = playerB.transform.position;

        // �������Ӹ����ڵ��λ��
        for (int i = 1; i < ropeSegmentCount - 1; i++)
        {
            // ����ڵ�λ�õĲ�ֵ
            float t = i / (float)(ropeSegmentCount - 1);
            Vector3 interpolatedPosition = Vector3.Lerp(playerA.transform.position, playerB.transform.position, t);

            // ������ӵ�������Ч��
            interpolatedPosition += new Vector3(0, Mathf.Sin(Time.time * 5f + i) * 0.1f, 0);
            ropePositions[i] = interpolatedPosition;
        }

        // ͨ������ģ�����������״
        for (int i = 0; i < ropePositions.Length; i++)
        {
            Vector3 point = ropePositions[i];
            point.y -= Mathf.Abs(Mathf.Cos((i * 0.1f) + (Time.time * 5))) * 0.1f;
            ropePositions[i] = point;
        }

        // ���� Line Renderer ��λ��
        rope.positionCount = ropePositions.Length;
        rope.SetPositions(ropePositions);
    }






    //��2///////////////////////////////////////////////////////////////////////////////////////////////////////
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

