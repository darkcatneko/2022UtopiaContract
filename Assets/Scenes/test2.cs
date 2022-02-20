using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    [SerializeField] Camera cam;
    void Start()
    {
        Debug.Log(cam.ViewportToWorldPoint(new Vector3(1, 1)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
