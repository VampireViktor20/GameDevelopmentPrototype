using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{




    public float speed = 6f;
    public float jump = 5f;
    public float climbSpeed = 5f;


    public float checkDisance;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform playermesh;
    public GameObject equipped;
    public GameObject sheathedSword;
    public bool isRunning;
    public bool canJump;
   

    public bool equipSword = false;

    public Animator anim;

    Rigidbody rb;

    float horizontal = 0f;
    float vertical = 0f;


    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {


        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();


        Vector3 moveDir = forward * vertical + right * horizontal;
        
        
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        if(moveDir != new Vector3 (0, 0, 0))
        {
            playermesh.rotation = Quaternion.LookRotation(moveDir);
        }
        else
        {
           
        }

    }


    void Update()
    {

        if(Input.GetKeyDown(KeyCode.V))
        {
            equipSword = !equipSword;
            anim.SetBool("IsDrawn", equipSword);
            anim.SetBool("IsSwordIdle", true);

        }
     
      

        if (equipSword == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                anim.SetBool("IsArmedWalking", true);
                anim.SetBool("IsSwordIdle", false);

            }
            else
            {

                anim.SetBool("IsArmedWalking", false);
                anim.SetBool("IsSwordIdle", true);

            }

            if (Input.GetMouseButtonDown(0) && anim.GetBool("IsSwordIdle") == true) 
            {
                anim.SetBool("IsAttacking1", true);
            }
            else
            {
                anim.SetBool("IsAttacking1", false);
            }

       
            sheathedSword.SetActive(false);
            equipped.SetActive(true);
            equipped.GetComponent<Sword>().UseSword();
        }

        else if(equipSword == false)
        {
            anim.SetBool("IsSwordIdle", false);
            equipped.SetActive(false);
            sheathedSword.SetActive(true);
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        canJump = Physics.CheckSphere(groundCheck.position, checkDisance, groundMask);

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }


        

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpAnim());
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * jump;
        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = !isRunning;
            anim.SetBool("IsRunning", isRunning);

            if (isRunning == true)
            {

                speed = 9f;
               

            }
            else if (isRunning == false) 
            {
                speed = 6f;
                
                
            }
        }

              

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkDisance);
    }

  

    IEnumerator JumpAnim()
    {
        anim.SetBool("IsJumping", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("IsJumping", false);

    }

    

}
