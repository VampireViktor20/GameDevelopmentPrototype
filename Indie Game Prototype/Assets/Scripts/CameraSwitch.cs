using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera thirdPersonCam;

    [SerializeField] private CinemachineVirtualCamera aimCam;

    [SerializeField] private bool aimMode;


    public GameObject aimCrossHair;

    void Start()
    {
        aimCam.enabled = aimMode;
        thirdPersonCam.enabled = !aimMode;
        
    }

    void Update()
    {



        if (Input.GetMouseButtonDown(1))
        {
            aimMode = !aimMode;
            thirdPersonCam.enabled = !aimMode;
            aimCam.enabled = aimMode;

        }

        if (aimMode == true)
        {
            float mouse = Input.GetAxis("Mouse Y");
            transform.Rotate(new Vector3(-mouse, 0, 0));
            aimCrossHair.SetActive(true);
        }
        else if(aimMode == false)
        {
            aimCrossHair.SetActive(false);
        }
        
    }
}
