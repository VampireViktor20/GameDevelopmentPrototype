using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject shopText;
    public GameObject shopPanel;
    public bool showtext;
    public bool usingShop;
    public Movement2 player;
    public Camera cam;

    void Start()
    {
        shopText.SetActive(false);
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if (showtext == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                shopPanel.SetActive(true);
                Cursor.visible = true;
                usingShop = true;
                cam.GetComponent<CinemachineBrain>().enabled = false;
                player.GetComponent<Movement2>().enabled = false;
            }
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            showtext = true;
            shopText.SetActive(true);

            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            showtext = false;
            shopText.SetActive(false);
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        cam.GetComponent<CinemachineBrain>().enabled = true;
        player.GetComponent<Movement2>().enabled = true;
    }
}
