using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public Slider slider;
    public float health;
    public Movement2 player;
    public CameraSwitch cam;
    public Animator anim;
    public Enemy1 enemy1;
    public Enemy2 enemy2;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        slider.value = health;

        if (health == 0)
        {
            StartCoroutine(MainMenu());
            anim.SetBool("IsDead1", true);
            player.GetComponent<Movement2>().enabled = false;
            cam.GetComponent<CameraSwitch>().enabled = false;
            

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Knife"))
        {
            health = health - 5f;
        }

        if(collision.gameObject.tag == ("EnemyBullet"))
        {
            health = health - 5f;
        }
    }
    
    public IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }

}
