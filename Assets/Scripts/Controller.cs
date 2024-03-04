using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    public List<GameObject> interactableObjects; // List to hold interactable objects

    public GameObject PlayerObj;

    void Update()
    {
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        Vector3 targetPosition = PlayerObj.transform.position + new Vector3(0.5f, -0.1f, 0.4f);
        transform.position = targetPosition;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            TryGrabObject();

        }
        else
        {

        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Tartar"))
            {
                RemoveInteractableObject(hit.collider.gameObject);
            }
        }
    }


    void RemoveInteractableObject(GameObject obj)
    {
        interactableObjects.Remove(obj); // Remove from the list
        Destroy(obj); // Destroy the object

        // If you want to just deactivate the object (similar to SetActive(false))
        // obj.SetActive(false);
    }
}
