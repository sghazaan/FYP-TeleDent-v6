using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include this namespace


public class handController : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject toolkitMenu;

   

    private void Update()
    {
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.name == "cavity-prep-button")
                    {
                    SceneManager.LoadScene("MovingScene");
                    }
                    else if (hit.collider.gameObject.name ==  "ToolKit-button")
                    {
                        toolkitMenu.SetActive(true);
                        mainmenu.SetActive(false);
                    }

                    else if (hit.collider.gameObject.name ==  "Back")
                    {
                        toolkitMenu.SetActive(false);
                        mainmenu.SetActive(true);
                    }
                     else{

                    }
                }
            }
        }
    }
}
