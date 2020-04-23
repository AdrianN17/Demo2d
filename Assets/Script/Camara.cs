using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Collider2D col;
    private float z;

    public Camera cam; 

    public Vector2 Max;
    public Vector2 Min;

    // Start is called before the first frame update
    void Start()
    {
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        if (col)
        {
            var vec = new Vector3(col.bounds.center.x, col.bounds.center.y, z);

            transform.position = new Vector3(
                Mathf.Clamp(vec.x, Min.x , Max.x ),
                Mathf.Clamp(vec.y, Min.y , Max.y ),
                vec.z
            );

        }
            
    }
}
