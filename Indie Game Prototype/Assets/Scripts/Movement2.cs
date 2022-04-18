using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{

    public enum PlayerState
    {
        WALKING,
        FALLING,
        CLIMBING
    }


    public PlayerState state = PlayerState.CLIMBING;
    public float speed = 6f;
    public float jump = 5f;
    public float climbSpeed = 5f;

    public float checkDisance;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform playermesh;
    public GameObject equipped;
    public GameObject sheathedSword;
    public bool canMove;
    public bool canJump;
    public bool equipSword = false;
    

    Rigidbody rb;

    float horizontal = 0f;
    float vertical = 0f;
    bool jumpDown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {


        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;


        Vector2 input = SquareToCircle(new Vector2(horizontal, vertical));
        Transform cam = Camera.main.transform;
        Vector3 moveDir = Quaternion.FromToRotation(cam.up, Vector3.up) * cam.TransformDirection(new Vector3(input.x, 0f, input.y));

        switch (state)
        {
            case PlayerState.WALKING: { HandleWalking(moveDir);  } break;
            case PlayerState.FALLING: { HandleFalling();  } break;
            case PlayerState.CLIMBING: { HandleClimbing(input);  } break;
            
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.02f))
        {
            state = PlayerState.WALKING;
        }
        else if (state == PlayerState.WALKING)
        {
            state = PlayerState.FALLING;
        }

        rb.useGravity = state != PlayerState.CLIMBING;

        jumpDown = false;


        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;

        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        if(moveDir != new Vector3 (0, 0, 0))
        {
            playermesh.rotation = Quaternion.LookRotation(moveDir);
        }




    }

    void HandleWalking(Vector3 moveDir)
    {
        Vector3 oldV = rb.velocity;
        Vector3 newV = moveDir * speed;
        newV.y = oldV.y;
        if(jumpDown)
        {
            newV.y = 5f;
            state = PlayerState.FALLING;
        }
        rb.velocity = newV;

        if(moveDir.sqrMagnitude > 0.01f)
        {
            transform.forward = moveDir;
        }
    }

    void HandleFalling()
    {
        if(jumpDown && Physics.Raycast(transform.position, transform.forward * 0.4f))
        {
            state = PlayerState.CLIMBING;
        }
    }

    void HandleClimbing(Vector2 input)
    {
        Vector3 offset = transform.TransformDirection(Vector2.one * 0.5f);
        Vector3 checkDir = Vector3.zero;
        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            RaycastHit checkHit;
            if(Physics.Raycast(transform.position + offset, transform.forward, out checkHit))
            {
                checkDir += checkHit.normal;
                k++;
            }
            offset = Quaternion.AngleAxis(90f, transform.forward) * offset;
        }
        checkDir /= k;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, -checkDir, out hit))
        {
            float dot = Vector3.Dot(transform.forward, -hit.normal);

            rb.position = Vector3.Lerp(rb.position, hit.point + hit.normal * 0.55f, 5f * Time.fixedDeltaTime);
            transform.forward = Vector3.Lerp(transform.forward, -hit.normal, 10f * Time.fixedDeltaTime);

            rb.useGravity = false;
            rb.velocity = transform.TransformDirection(input) * climbSpeed;
            if(jumpDown)
            {
                rb.velocity = Vector3.up * 5f + hit.normal * 2f;
                state = PlayerState.FALLING;
            }
        }
        else
        {
            state = PlayerState.FALLING;
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


        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    jumpDown = true;
        //}
        canJump = Physics.CheckSphere(groundCheck.position, checkDisance, groundMask);

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * jump;
        }

        if(Input.GetKey(KeyCode.LeftShift))
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

    Vector2 SquareToCircle(Vector2 input)
    {
        return (input.sqrMagnitude >= 1f) ? input.normalized : input;
    }


}
