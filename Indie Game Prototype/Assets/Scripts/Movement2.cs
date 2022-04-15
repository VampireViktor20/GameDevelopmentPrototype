using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float speed = 6f;
    public float jump = 5f;

    public float checkDisance;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform playermesh;

    public bool canMove;
    public bool canJump;

    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Rigidbody rb = GetComponent<Rigidbody>();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

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
        canJump = Physics.CheckSphere(groundCheck.position, checkDisance, groundMask);

        if(canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * jump;
        }

      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkDisance);
    }
}
