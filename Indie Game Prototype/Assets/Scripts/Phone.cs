using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Phone : MonoBehaviour
{
    public bool usingPhone;
    public Movement2 player;
    public GameObject phone;
    public Camera cam;
 

   

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            usingPhone = !usingPhone;


            PhoneToggle();

        }
       

        
    }

    void PhoneToggle()
    {
        if(usingPhone == true)
        {
            
            cam.GetComponent<CinemachineBrain>().enabled = false;
            player.GetComponent<Movement2>().enabled = false;
            UsingPhone();
        }

        if(usingPhone == false)
        {
            
            cam.GetComponent<CinemachineBrain>().enabled = true;
            player.GetComponent<Movement2>().enabled = true;
            PhoneAway();
        }
        
    }

   void UsingPhone()
    {
        Cursor.visible = true;
        phone.SetActive(true);
        
    }

    void PhoneAway()
    {
        Cursor.visible = false;
        phone.SetActive(false);
    }


    
}

