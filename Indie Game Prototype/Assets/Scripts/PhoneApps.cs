using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 
public class PhoneApps : MonoBehaviour
{
    public GameObject panel;
    public GameObject previousPanel;
    public bool usingPanel;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void MissionsPanel()
    {
        if (usingPanel)
        {
            OpenPanel();
        }
        else
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        previousPanel.SetActive(false);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        previousPanel.SetActive(true);
    }

    

}
