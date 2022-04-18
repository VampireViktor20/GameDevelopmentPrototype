using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform cam;
    public  GameObject bulletPrefab;
    public Transform barrel;
    public Transform bulletParent;
    public float bulletHitMissDistance = 25f;
    // Update is called once per frame
   

    public void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrel.position, Quaternion.identity, bulletParent);
        Bullets bullets = bullet.GetComponent<Bullets>();

        if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity))
        {

            bullets.target = hit.point;
            bullets.hit = true;
        }
        else 
        {
            bullets.target = cam.position + cam.forward * bulletHitMissDistance;
            bullets.hit = false;
        }
        
    }
}
