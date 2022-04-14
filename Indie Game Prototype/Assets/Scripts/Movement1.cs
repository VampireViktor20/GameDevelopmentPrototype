using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement1 : MonoBehaviour
{
    public float speed = 6f;
    public float rotationSpeed = 720f;
    public float jumpSpeed = 5f;

    public float climbingSpeed = 5f;

 

    public Transform cam;

    private CharacterController controller;
    private float ySpeed;
    private float originalStepOffSet;



    void Start()
    {
       
        controller = GetComponent<CharacterController>();
        originalStepOffSet = controller.stepOffset;
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

      
     
        
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * speed;
        moveDirection = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;


        if (controller.isGrounded)
        {
            ySpeed = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.stepOffset = originalStepOffSet;
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            controller.stepOffset = 0;
        }

        Vector3 velocity = moveDirection * magnitude;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);

        if(moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

   
}
