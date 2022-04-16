using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook thirdPersonCam;

    [SerializeField] private CinemachineVirtualCamera aimCam;

    [SerializeField] public bool aimMode;
    public Gun gun;

    public GameObject equipped;
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
            if(Input.GetMouseButtonDown(0))
            {
                gun.ShootGun();
            }
            equipped.SetActive(true);
            aimCrossHair.SetActive(true);
        }
        else if(aimMode == false)
        {
            equipped.SetActive(false);
            aimCrossHair.SetActive(false);
        }
        
    }
}
