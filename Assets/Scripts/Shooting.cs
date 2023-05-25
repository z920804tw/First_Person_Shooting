using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
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
        fireAnimation = GetComponent<WeaponManager>().fireAnim; //�o��|�qWeaponManager ����fireAnim����e�ȵ�fireAnimation
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


   
}
