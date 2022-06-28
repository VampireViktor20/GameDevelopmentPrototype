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


    public Animator anim;

    public GameObject equipped;
    public GameObject aimCrossHair;
    public GameObject sheathedPistol;
    public Movement2 movement;

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
            anim.SetBool("IsAiming", aimMode);

        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsAimWalking", true);
        }
        else
        {
            anim.SetBool("IsAimWalking", false);
        }

        if (aimMode == true)
        {

            if(Input.GetMouseButtonDown(0))
            {
                anim.SetBool("IsShooting", true);
                gun.ShootGun();
            }
            else
            {
                anim.SetBool("IsShooting", false);
            }

            sheathedPistol.SetActive(false);
            movement.equipSword = false;
            equipped.SetActive(true);
            aimCrossHair.SetActive(true);
        }
        else if(aimMode == false)
        {
            sheathedPistol.SetActive(true);
            equipped.SetActive(false);
            aimCrossHair.SetActive(false);
        }
        
    }
}
