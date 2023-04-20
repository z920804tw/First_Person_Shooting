using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    [Header("移動速度設定")]
    public float moveSpeed;
    public float jumpSpeed;


    [Header("按鍵綁定")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("基本設定")]
    public Transform PlayerCamera;   // 攝影機

    float horizontalInput;   // 左右方向按鍵的數值(-1 <= X <= +1)
    float verticalInput;     // 上下方向按鍵的數值(-1 <= Z <= +1)

    bool canJump=true;

    Vector3 moveDirection;   // 移動方向

    Rigidbody rbFirstPerson; // 第一人稱物件(膠囊體)的剛體

    private void Start()
    {
        rbFirstPerson = GetComponent<Rigidbody>();
        rbFirstPerson.freezeRotation = true;         // 鎖定第一人稱物件剛體旋轉，不讓膠囊體因為碰到物件就亂轉
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

    }

    private void FixedUpdate()
    {
        MovePlayer(); // 只要是物件移動，建議你放到FixedUpdate()

    }

    // 方法：取得目前玩家按方向鍵上下左右的數值
    private void MyInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //判斷如果jumpKey被按下，就執行Jump()的函式
        if (Input.GetKeyDown(jumpKey) == true&&canJump==true)         
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        // 計算移動方向(其實就是計算X軸與Z軸兩個方向的力量)
        moveDirection = PlayerCamera.forward * verticalInput + PlayerCamera.right * horizontalInput;

        // 推動第一人稱物件 normalized會讓值最大值=1或0或-1
        rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z); // 取得僅X軸與Z軸的平面速度
        // 如果平面速度大於預設速度值，就將物件的速度限定於預設速度值
        if (flatVel.magnitude > moveSpeed) //magnitude=方向性的速度 
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbFirstPerson.velocity = new Vector3(limitedVel.x, rbFirstPerson.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        //先設定Y軸設定0 ，之後再往上推。
        //Impulse 為瞬間的推力 ，Transform.up為以Y軸移動。
        rbFirstPerson.velocity = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z);
        rbFirstPerson.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
        canJump = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

}