using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    public float RotationSpeed;
    Vector3 rotationSky;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationSky.x+=RotationSpeed*Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationSky.x, 0, 0);
    }
}
