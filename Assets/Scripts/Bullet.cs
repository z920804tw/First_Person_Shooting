using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bulletRotation;
    void Start()
    {
        bulletRotation = GameObject.Find("Player/Main Camera");
        transform.rotation = Quaternion.Euler(90f, 0,0);
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
