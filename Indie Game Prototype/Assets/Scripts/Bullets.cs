using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed = 50f;
    public float destroyTime = 3f;
    public int  bulletDamage = 25;

    public Vector3 target { get; set; }
    public bool hit { get; set; }


    private void OnEnable()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
