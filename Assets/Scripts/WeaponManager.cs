using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("�ѦҪ���")]
    public Camera PlayerCamera;
    public Transform firPos;

    public GameObject bullet;

    [Header("�j�K���]�w")]
    public int magazineSize;        //�u�X�ƶq
    public int bulletLeft;          //�Ѿl�l�u
    public float reloadTime;
    public float recoilForce;   //�ϧ@�ΤO

    bool Reloading;

    [Header("��rUI�]�w")]
    public TextMeshProUGUI reloadDisplay;
    public TextMeshProUGUI ammoDisplay;


    [Header("�l�u�t��")]
    public float bulletSpeed;

    [Header("�ʵe�]�w�ίS��")]
    public Animator reloadAnimation;
    public Animator fireAnimation;
    public ParticleSystem fireEffect;

    [Header("�Z��")]
    public GameObject[] weaponObjects;        // �Z���M��

    int weaponNumber = 0;                     // �ثe��ܪZ�������ǽs��
    GameObject weaponInUse;                   // �ثe��ܪZ��


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
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));  // �q��v���g�X�@���g�u
        RaycastHit hit;  // �ŧi�@�Ӯg���I
        Vector3 targetPoint;  // �ŧi�@�Ӧ�m�I�ܼơA��ɭԦp�G������F��A�N�s��o���ܼ�


        // �p�G�g�u�������ƸI���骺����
        if (Physics.Raycast(ray, out hit) == true)
        {
            targetPoint = hit.point;         // �p�G������F��A�N��Ӫ��󪺮y�е�targetPoint�A�g�u�N���b�Ӫ��󪺮y��
            
            Debug.Log("���쪫��");
        }
        else
        {
            targetPoint = ray.GetPoint(50);// �p�G�S������F��A�N�]�w�g�u���I��50
            Debug.Log("�S���쪫��");
        }
        Debug.DrawRay(ray.origin, targetPoint - ray.origin, Color.red, 10); // ��ܮg�u�A�_�I�H�ù����ߨ� �ؼ��I-�_�I���a��

        GameObject currentBullet = Instantiate(bullet, firPos.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(firPos.transform.forward * bulletSpeed, ForceMode.Impulse);
        currentBullet.GetComponent<Transform>().LookAt(targetPoint);        //�l�u�����ץH��targetPoint��V�h����
        


        bulletLeft--;
        UpdateAmmoDisplay();

        this.GetComponent<Rigidbody>().AddForce(-firPos.transform.forward * recoilForce, ForceMode.Impulse); //�}�j�ᦳ��y�O�A��V���}����V���ۤϭ�

        fireEffect.Play();                         //����}�����S��
        if (fireAnimation != null)
        {
            fireAnimation.SetTrigger("Fire");
        }
    }

    void MyInput()
    {

        // �P�_���S�����U����
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (bulletLeft > 0 && Reloading == false)       //�u�Ĥj��0�B�èS���b�˶�u�Įɥi�HShoot
            {
                Shoot();

            }


        }
        if (Input.GetKeyDown(KeyCode.R) == true && bulletLeft < magazineSize && Reloading == false)  //���UR �B�ثe�u�ļƶq�p���`�u�� �B�S�����b�˶�ɥi�HReload                                                                
        {
            Reload();

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0, 0);

        // �P�_�G���U�Ʀr��2�A�������Z��1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(0, 1);

        // �P�_�G�u�ʷƹ��u��
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)      // ���e�u��
            SwitchWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // ����u��
            SwitchWeapon(-1);
    }
    void Reload()
    {
        Reloading = true;                      // ��Reload�����A�令True�A�N���b�˶�u��
        reloadDisplay.enabled = true;          //Reload���r��ܥX��
        Invoke("ReloadFinished", reloadTime);  // �̷�reloadTime�ҳ]�w�����u���ɶ��˼ơA�ɶ���0�ɰ���ReloadFinished��k
    }

    void ReloadFinished()
    {
        bulletLeft = magazineSize;            // �N�l�u���s��
        Reloading = false;                    // �NReload���A�令False�A�N��S���b�˶�
        reloadDisplay.enabled = false;        // Reload�������
        UpdateAmmoDisplay();
    }
    void UpdateAmmoDisplay()                  //��s��ܥثe�u�ļƶq
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
        // �N�Z���M��������áA���@���������áA�A��ܻݭn���Z��
        foreach (GameObject item in weaponObjects)
        {
            item.SetActive(false);
        }

        // switch �P�_���G�H�Ѽ�_addNumber�P�_�n�������Z��
        switch (_addNumber)
        {
            case 0:                                                   // _addNumber == 0�A�N��Ϋ��䪽�����w�Z���}�C��}
                weaponNumber = _weaponNumber;
                break;
            case 1:                                                   // _addNumber == 1�A�N���W�u�ƹ��u��
                if (weaponNumber == weaponObjects.Length - 1)         // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�w�g�O�̫�@�ӪZ���A�h�N�Z���}�C��}�]�w��0
                    weaponNumber = 0;
                else
                    weaponNumber += 1;
                //weaponNumber = (weaponNumber == weaponObjects.Length - 1) ? 0 : weaponNumber += 1; // �]�i�H��H�W���P�_���g���o��
                break;
            case -1:                                                   // _addNumber == -1�A�N���U�u�ƹ��u��
                if (weaponNumber == 0)                                 // ��{�`���Ʀr�A���w�쥻���Z���}�C��}�O�Ĥ@�ӪZ���A�h�N�Z���}�C��}���M�檺�̫�@�Ӧ�}
                    weaponNumber = weaponObjects.Length - 1;
                else
                    weaponNumber -= 1;
                //weaponNumber = (weaponNumber == 0) ? weaponObjects.Length - 1 : weaponNumber -= 1; // �]�i�H��H�W���P�_���g���o��
                break;
        }
        weaponObjects[weaponNumber].SetActive(true);    // ��ܩҫ��w���Z��
        weaponInUse = weaponObjects[weaponNumber];      // �]�w�ثe�ҿ�ܪ��Z������(���ɥi�H�ΨӰ���Z���үS�w����k�A�U�@���`�|����)
        fireAnimation = weaponInUse.GetComponent<Animator>();
        
    }
}
