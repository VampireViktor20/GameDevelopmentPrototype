using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{




    public HealthBar healthbar;
    public float speed = 6f;
    public float jump = 5f;
    public float climbSpeed = 5f;
    public int health = 100;


    public float checkDisance;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform playermesh;
    public GameObject equipped;
    public GameObject sheathedSword;
    public bool canMove;
    public bool canJump;
    public bool isClimbing = false;

    public bool equipSword = false;

    public Animator anim;

    Rigidbody rb;

    float horizontal = 0f;
    float vertical = 0f;


    void Start()
    {
        anim = GetComponent<Animator>();
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

    }


    void Update()
    {

        if(Input.GetKeyDown(KeyCode.V))
        {
            equipSword = !equipSword;

        }

        if (equipSword == true)
        {
            sheathedSword.SetActive(false);
            equipped.SetActive(true);
            equipped.GetComponent<Sword>().UseSword();
        }

        else if(equipSword == false)
        {
            equipped.SetActive(false);
            sheathedSword.SetActive(true);
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        canJump = Physics.CheckSphere(groundCheck.position, checkDisance, groundMask);


        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * jump;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 9f;
        }
        else 
        {
            speed = 6f;
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkDisance);
    }

  




}
