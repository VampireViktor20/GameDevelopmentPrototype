using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Slider slider;
    public float health;
    public Movement2 player;
    public CameraSwitch cam;
    public Animator anim;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        slider.value = health;

        if (health == 0)
        {
            anim.SetBool("IsDead1", true);
            player.GetComponent<Movement2>().enabled = false;
            cam.GetComponent<CameraSwitch>().enabled = false;
   
            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Knife"))
        {
            health = health - 10f;
        }

        if(collision.gameObject.tag == ("EnemyBullet"))
        {
            health = health - 20f;
        }
    }
    

}
