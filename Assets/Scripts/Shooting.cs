using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("�ѦҪ���")]
    public Camera PlayerCamera;
    public Transform firPos;

    public GameObject bullet;
    [Header("�l�u�t��")]
    public float bulletSpeed;

    private void Start()
    {

    }
    private void Update()
    {
        // �P�_���S�����U����
        if (Input.GetMouseButtonDown(0) == true)
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


            //�p��g����V �q�}���y�Ш�ؼ��I
            //Vector3 directionWithoutSpread = targetPoint - firPos.position; //"�ؼЦ�m-�}����m=��V"
            GameObject currentBullet = Instantiate(bullet, firPos.position, Quaternion.identity); 

            //currentBullet.transform.forward = directionWithoutSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(firPos.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}
