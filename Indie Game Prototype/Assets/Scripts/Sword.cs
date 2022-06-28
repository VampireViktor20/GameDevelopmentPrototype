using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        UseSword();
        
    }

    public void UseSword()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("IsAttacking1", true);
        }
        else
        {
            anim.SetBool("IsAttacking1", false);
        }
    }


    
}
