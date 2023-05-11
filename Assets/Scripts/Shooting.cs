using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("參考物件")]
    public Camera PlayerCamera;
    public Transform firPos;

    public GameObject bullet;
    [Header("子彈速度")]
    public float bulletSpeed;

    private void Start()
    {

    }
    private void Update()
    {
        // 判斷有沒有按下左鍵
        if (Input.GetMouseButtonDown(0) == true)
        {
            
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // 從攝影機射出一條射線
            RaycastHit hit;  // 宣告一個射擊點
            Vector3 targetPoint;  // 宣告一個位置點變數，到時候如果有打到東西，就存到這個變數


            // 如果射線有打到具備碰撞體的物件
            if (Physics.Raycast(ray, out hit) == true)
            {
                targetPoint = hit.point;         // 如果有打到東西，就把該物件的座標給targetPoint，射線就停在該物件的座標
                Debug.Log("打到物件");
            }
            else
            {
                targetPoint = ray.GetPoint(50);// 如果沒有打到東西，就設定射線終點為50
                Debug.Log("沒打到物件");
            }

            Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // 顯示射線，起點以螢幕中心到 目標點-起點的地方


            //計算射擊方向 從開火座標到目標點
            //Vector3 directionWithoutSpread = targetPoint - firPos.position; //"目標位置-開火位置=方向"
            GameObject currentBullet = Instantiate(bullet, firPos.position, Quaternion.identity); 

            //currentBullet.transform.forward = directionWithoutSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(firPos.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
