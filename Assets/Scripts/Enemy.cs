using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [Header("追蹤目標設定")]
    public string targetName = "Player";                     // 設定目標物件的標籤名稱
    public float minimunTraceDistance = 5.0f;                // 設定最短的追蹤距離
    NavMeshAgent Nav;

    GameObject targetObject = null;                          // 目標物件的變數                              

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag(targetName);   // 以帶有特定的標籤名稱為目標物件
        Nav=GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 計算目標物件和自己的距離
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // 判斷式：判斷距離是否低於最短追蹤距離
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
        

        //transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, 0.02f); // 讓自己往目標物的座標移動
        if (Nav.enabled == true)
        {
           
            Nav.SetDestination(targetObject.transform.position);
        }

           
    }
}
