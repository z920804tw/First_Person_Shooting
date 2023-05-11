using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    [Header("移動速度設定")]
    public float moveSpeed;
    public float jumpSpeed;
    public float groundDrag;         // 地面的減速

    [Header("地板確認")]
    public float playerHeight;       // 設定玩家高度
    public LayerMask whatIsGround;   // 設定哪一個圖層是射線可以打到的
    public bool grounded;            // 布林變數：有沒有打到地面

    [Header("按鍵綁定")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("基本設定")]
    public Transform PlayerCamera;   // 攝影機

    float horizontalInput;   // 左右方向按鍵的數值(-1 <= X <= +1)
    float verticalInput;     // 上下方向按鍵的數值(-1 <= Z <= +1)


    Vector3 moveDirection;   // 移動方向

    Rigidbody rbFirstPerson; 

    private void Start()
    {

        rbFirstPerson = GetComponent<Rigidbody>();
        rbFirstPerson.freezeRotation = true;         // 鎖定第一人稱物件剛體旋轉，不讓膠囊體因為碰到物件就亂轉
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        // 使用一個向下的射線來偵測Layer叫Ground，沒射到就回傳false ，如果有射到就true
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround); //第一個值=射線座標，2.方向 3.最大長度 4.要選擇判斷哪個圖層   
        Debug.DrawRay(transform.position,new Vector3(0,-playerHeight,0), Color.red);//顯示射線在遊戲中，方向是往下顯示到playerHeight

        if (grounded == true)   //如果碰到射線碰到地板，就改rb的摩擦力，沒有就歸0
        {
            rbFirstPerson.drag = groundDrag;
        }
        else
        {
            rbFirstPerson.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(); // 只要是物件移動，建議你放到FixedUpdate()

    }

    // 方法：取得目前玩家按方向鍵上下左右的數值
    private void MyInput() //按鍵取得
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //判斷如果jumpKey被按下，就執行Jump()的函式
        if (Input.GetKeyDown(jumpKey) == true&&grounded==true)         
        {
            Jump();
        }
    }

    private void MovePlayer() //移動功能
    {
        // 計算移動方向(其實就是計算X軸與Z軸兩個方向的力量)
        moveDirection = PlayerCamera.forward * verticalInput + PlayerCamera.right * horizontalInput;
        Vector3 mDirection = new Vector3(moveDirection.x, 0, moveDirection.z); //限制當攝影機向上、下看不會飛天，Y軸固定0

        // 推動第一人稱物件 normalized會讓值最大值=1或0或-1
        rbFirstPerson.AddForce(mDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    }
    private void SpeedControl() //速度限制
    {
        Vector3 flatVel = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z); // 取得僅X軸與Z軸的平面速度
        
        if (flatVel.magnitude > moveSpeed) //如果平面速度大於預設速度值，就將物件的速度限定於預設速度值，magnitude=方向性的速度 
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbFirstPerson.velocity = new Vector3(limitedVel.x, rbFirstPerson.velocity.y, limitedVel.z);
        }

    }
    private void Jump()  //跳躍功能
    {

        //先設定Y軸設定0 ，之後再往上推。Impulse 為瞬間的推力 ，Transform.up為以Y軸移動。
        rbFirstPerson.velocity = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z);
        rbFirstPerson.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
    }

    

}