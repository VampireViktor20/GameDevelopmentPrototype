using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed = 300f;

    Rigidbody rb;

    Movement2 target;
    Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindObjectOfType<Movement2>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(moveDirection.x, 0, moveDirection.z);
        Destroy(gameObject, 3f);
    }
}

