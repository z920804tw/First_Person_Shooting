using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [Header("�l�ܥؼг]�w")]
    public string targetName = "Player";                     // �]�w�ؼЪ��󪺼��ҦW��
    public float minimunTraceDistance = 5.0f;                // �]�w�̵u���l�ܶZ��
    NavMeshAgent Nav;

    GameObject targetObject = null;                          // �ؼЪ����ܼ�                              

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // �H�a���S�w�����ҦW�٬��ؼЪ���
        Nav=GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // �p��ؼЪ���M�ۤv���Z��
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // �P�_���G�P�_�Z���O�_�C��̵u�l�ܶZ��
        if (distance <= minimunTraceDistance)
        {
            Nav.enabled = true;
        }
        else
        {
            Nav.enabled = false;
        }

    }

    void FixedUpdate()
    {
        

        //transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, 0.02f); // ���ۤv���ؼЪ����y�в���
        if (Nav.enabled == true)
        {
           
            Nav.SetDestination(targetObject.transform.position);
        }

           
    }
}
