using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CamScroll : MonoBehaviour
{
    private CinemachineInputProvider CinemachineInputProvider;
    private CinemachineVirtualCamera virtualCamera;

    public GameObject camObj;
    public float sc = 2;
    void Start()
    {
        virtualCamera = camObj.GetComponent<CinemachineVirtualCamera>();
        CinemachineInputProvider = camObj.GetComponent<CinemachineInputProvider>();
        CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is Cinemachine3rdPersonFollow)
        {
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = 1; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        setDistance();
        CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is Cinemachine3rdPersonFollow)
        {
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = sc;
        }
    }

    void setDistance()
    {       
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            sc += 0.1f;
            sc = Mathf.Clamp(sc, 1.2f, 2f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            sc -= 0.1f;
            sc = Mathf.Clamp(sc, 1.2f, 2f);
        }
       
    }
}
