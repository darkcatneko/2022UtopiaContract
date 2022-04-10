using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField]GameObject target;
    void Start()
    {
        Debug.Log(cam.ViewportToWorldPoint(new Vector3(1, 1)));
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform);
        transform.RotateAround(target.transform.position, Vector3.up, 1 * Time.deltaTime);

    }
}
