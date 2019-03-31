using UnityEngine;
using System;
using System.Collections;

public class FloatBehavior : MonoBehaviour
{
    public float originalY;

    float floatStrength = 0.1f; 
                                    

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}