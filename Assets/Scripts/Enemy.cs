using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("�l�ܥؼг]�w")]
    public string targetName = "Player";                     // �]�w�ؼЪ��󪺼��ҦW��
    public float minimunTraceDistance = 5.0f;                // �]�w�̵u���l�ܶZ��

    GameObject targetObject = null;                          // �ؼЪ����ܼ�
    bool enableMove = false;                                 // �p�G�ؼЪ���Z�����u�A�o���ܼƬ�true

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
    }

    void Update()
    {
        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ��
        if (distance >= minimunTraceDistance)
        {
            enableMove = false;
        }
        else
        {
            enableMove = true;
        }

    }

    void FixedUpdate()
    {
        // enableMove��true�A�N�h�l�ܥؼ�
        if (enableMove == true)
        {
            transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, 0.02f); // ���ۤv���ؼЪ����y�в���
            
            
        }
           
    }
}
