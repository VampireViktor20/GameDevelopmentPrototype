using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public Slider slider;
    public float health;


 
    void Update()
    {
        slider.value = health;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Bullet"))
        {
            health -= 20f;
        }
      
       if(collision.gameObject.tag == ("Sword"))
        {
            health -= 20f;
        }
    }

    
 
    

}
