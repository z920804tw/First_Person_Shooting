using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    Transform firPos;
    Rigidbody rb;
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //firPos = GameObject.Find("Player/Main Camera/Gun/Cube/firPos").transform;
        //rb.AddForce(firPos.forward * 100, ForceMode.Impulse);
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Destroy(gameObject);
        }
    }
}
