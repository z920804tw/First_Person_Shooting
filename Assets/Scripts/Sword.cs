using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("�C���ʵe�]�w")]
    public Animator swordAnim;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            swordAnim =GetComponent<Animator>();
            swordAnim.SetTrigger("Fire");
        }
    }
}
