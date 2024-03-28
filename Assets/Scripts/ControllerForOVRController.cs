using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForOVRController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject mainmenu;
    public GameObject historyMenu;
    public GameObject progressMenu;
    public ProgressTracker progressTracker;

    private void Start()
    {
        Vector3 targetPosition = playerObj.transform.position + new Vector3(0.15f, -0.13f, 0.15f);
        transform.position = targetPosition;
    }

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
                    if (hit.collider.gameObject.name == "resume-button")
                    {
                        mainmenu.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name ==  "patient-history-button")
                    {
                        historyMenu.SetActive(true);
                        mainmenu.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name ==  "back-button")
                    {
                        historyMenu.SetActive(false);
                        progressMenu.SetActive(false);
                        mainmenu.SetActive(true);

                    }
                    else if (hit.collider.gameObject.name ==  "end-game-button"){
                        mainmenu.SetActive(false);
                        progressMenu.SetActive(true);
                        progressTracker.PrintProgressReport();

                    }
                    else if (hit.collider.gameObject.name ==  "quit-game-button"){
                        progressMenu.SetActive(false);
                        mainmenu.SetActive(false);
                        historyMenu.SetActive(false);
                        gameObject.SetActive(false);
                        //add stuff to end game
                    }
                     else{

                    }
                }
            }
        }
    }
}
