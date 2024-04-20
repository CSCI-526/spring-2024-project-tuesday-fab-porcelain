using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class prompt1Controller : MonoBehaviour
{
    public GameObject playerA;
    public float loc1xmin;
    public float loc1xmax;
    public float loc1ymin;
    public float loc1ymax;


    public float loc2xmin;
    public float loc2xmax;
    public float loc2ymin;
    public float loc2ymax;


    private TextMeshProUGUI promptText;
    private bool isVisible = false;

    void Start()
    {
        promptText = GetComponent<TextMeshProUGUI>(); // 获取组件方法
        promptText.enabled = false; // 初始时文本不可见
    }

    void Update()
    {

        // 获取 PlayerA 的位置
        Vector3 playerAPosition = playerA.transform.position;
        // Debug.Log("playerAPosition.x:" + playerAPosition.x);
        // Debug.Log("playerAPosition.y:" + playerAPosition.y);
        // 检查 PlayerA 的位置是否在第一个范围内
        if (inarea(playerAPosition))
        {
            // 如果在范围内，显示提示文本
            promptText.enabled = true;
            isVisible = true;
        }
        else
        {
            // 如果不在范围内，隐藏提示文本
            if (isVisible)
            {
                promptText.enabled = false;
                isVisible = false;
            }
        }
    }


    bool inarea(Vector3 playerAPosition){
        if (playerAPosition.x >= loc1xmin && playerAPosition.x <= loc1xmax && 
            playerAPosition.y >= loc1ymin && playerAPosition.y <= loc1ymax)
        {
            

            return true;
            
        }
        if (playerAPosition.x >= loc2xmin && playerAPosition.x <= loc2xmax && 
            playerAPosition.y >= loc2ymin && playerAPosition.y <= loc2ymax)
        {
            return true;
        }

        return false;

    }
}
