using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public Vector3 mousePosition;
    public Camera screenCamera;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
