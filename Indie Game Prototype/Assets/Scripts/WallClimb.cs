using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimb : MonoBehaviour
{

    public float open = 100f;
    public float range = 10f;
    public bool collideWall = false;
    public float climbSpeed = 6f;
    public Transform player;
    public Animator anim;


    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }


    void Update()
    {

        Shoot();

        if(Input.GetKey(KeyCode.W) & collideWall == true)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsClimbing", true);
            
            transform.position += Vector3.up * Time.deltaTime * climbSpeed;
            GetComponent<Rigidbody>().isKinematic = true;
            collideWall = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("IsClimbing", false);
            GetComponent<Rigidbody>().isKinematic = false;
            collideWall = false;
        }
                
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(player.transform.position, player.transform.forward, out hit, range))
        {

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                collideWall = true;
            }
        }
    }
}
