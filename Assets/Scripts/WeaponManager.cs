using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("參考物件")]
    public Camera PlayerCamera;
    public Transform firPos;

    public GameObject bullet;

    [Header("槍枝的設定")]
    public int magazineSize;        //彈匣數量
    public int bulletLeft;          //剩餘子彈
    public float reloadTime;
    public float recoilForce;   //反作用力

    bool Reloading;

    [Header("文字UI設定")]
    public TextMeshProUGUI reloadDisplay;
    public TextMeshProUGUI ammoDisplay;


    [Header("子彈速度")]
    public float bulletSpeed;

    [Header("動畫設定及特效")]
    public Animator reloadAnimation;
    public Animator fireAnimation;
    public ParticleSystem fireEffect;

    [Header("武器")]
    public GameObject[] weaponObjects;        // 武器清單

    int weaponNumber = 0;                     // 目前選擇武器的順序編號
    GameObject weaponInUse;                   // 目前選擇武器


    private void Start()
    {
        Reloading = false;
        bulletLeft = magazineSize;
        reloadDisplay.enabled = false;
        UpdateAmmoDisplay();
        
    }


    private void Update()
    {
        MyInput();
    }

    void Shoot()
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

        GameObject currentBullet = Instantiate(bullet, firPos.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(firPos.transform.forward * bulletSpeed, ForceMode.Impulse);
        currentBullet.GetComponent<Transform>().LookAt(targetPoint);        //子彈的角度以我targetPoint方向去旋轉
        


        bulletLeft--;
        UpdateAmmoDisplay();

        this.GetComponent<Rigidbody>().AddForce(-firPos.transform.forward * recoilForce, ForceMode.Impulse); //開槍後有後座力，方向為開火方向的相反面

        fireEffect.Play();                         //播放開火的特效
        if (fireAnimation != null)
        {
            fireAnimation.SetTrigger("Fire");
        }
    }

    void MyInput()
    {

        // 判斷有沒有按下左鍵
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (bulletLeft > 0 && Reloading == false)       //彈藥大於0且並沒有在裝填彈藥時可以Shoot
            {
                Shoot();

            }


        }
        if (Input.GetKeyDown(KeyCode.R) == true && bulletLeft < magazineSize && Reloading == false)  //按下R 且目前彈藥數量小於總彈藥 且沒有正在裝填時可以Reload                                                                
        {
            Reload();

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0, 0);

        // 判斷：按下數字鍵2，切換為武器1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(0, 1);

        // 判斷：滾動滑鼠滾輪
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)      // 往前滾動
            SwitchWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // 往後滾動
            SwitchWeapon(-1);
    }
    void Reload()
    {
        Reloading = true;                      // 把Reload的狀態改成True，代表正在裝填彈藥
        reloadDisplay.enabled = true;          //Reload的字顯示出來
        Invoke("ReloadFinished", reloadTime);  // 依照reloadTime所設定的換彈夾時間倒數，時間為0時執行ReloadFinished方法
    }

    void ReloadFinished()
    {
        bulletLeft = magazineSize;            // 將子彈重新填滿
        Reloading = false;                    // 將Reload狀態改成False，代表沒有在裝填
        reloadDisplay.enabled = false;        // Reload顯示關閉
        UpdateAmmoDisplay();
    }
    void UpdateAmmoDisplay()                  //更新顯示目前彈藥數量
    {
        if(ammoDisplay != null)              
        {
            if (bulletLeft == 0)
            {
                ammoDisplay.SetText($"Press R Reload Ammo");
            }
            else
            {
                ammoDisplay.SetText($"Ammo:{bulletLeft}/{magazineSize}");
            }

        }
    }


    void SwitchWeapon(int _addNumber, int _weaponNumber = 0)
    {
        // 將武器清單全部隱藏，先一次全部隱藏，再顯示需要的武器
        foreach (GameObject item in weaponObjects)
        {
            item.SetActive(false);
        }

        // switch 判斷式：以參數_addNumber判斷要怎麼切換武器
        switch (_addNumber)
        {
            case 0:                                                   // _addNumber == 0，代表用按鍵直接指定武器陣列位址
                weaponNumber = _weaponNumber;
                break;
            case 1:                                                   // _addNumber == 1，代表往上滾滑鼠滾輪
                if (weaponNumber == weaponObjects.Length - 1)         // 實現循環數字，假定原本的武器陣列位址已經是最後一個武器，則將武器陣列位址設定為0
                    weaponNumber = 0;
                else
                    weaponNumber += 1;
                //weaponNumber = (weaponNumber == weaponObjects.Length - 1) ? 0 : weaponNumber += 1; // 也可以把以上的判斷式寫成這樣
                break;
            case -1:                                                   // _addNumber == -1，代表往下滾滑鼠滾輪
                if (weaponNumber == 0)                                 // 實現循環數字，假定原本的武器陣列位址是第一個武器，則將武器陣列位址為清單的最後一個位址
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;
                //weaponNumber = (weaponNumber == 0) ? weaponObjects.Length - 1 : weaponNumber -= 1; // 也可以把以上的判斷式寫成這樣
                break;
        }
        weaponObjects[weaponNumber].SetActive(true);    // 顯示所指定的武器
        weaponInUse = weaponObjects[weaponNumber];      // 設定目前所選擇的武器物件(屆時可以用來執行武器所特定的方法，下一章節會介紹)
        fireAnimation = weaponInUse.GetComponent<Animator>();
        
    }
}
