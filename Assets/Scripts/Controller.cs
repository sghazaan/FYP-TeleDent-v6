using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    public List<GameObject> interactableObjects; // List to hold interactable objects

    public GameObject Player;

    void Update()
    {
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        Vector3 targetPosition = Player.transform.position + new Vector3(0.5f, -0.1f, 0.8f);
        transform.position = targetPosition;

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (!isGrabbing)
            {
                TryGrabObject();
            }
        }
        else
        {
            if (isGrabbing)
            {
                ReleaseObject();
            }
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                GrabObject(hit.collider.gameObject);
            }

            if (hit.collider.CompareTag("Tartar"))
            {
                RemoveInteractableObject(hit.collider.gameObject);
            }
        }
    }

    void GrabObject(GameObject obj)
    {
        grabbedObject = obj;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.transform.SetParent(transform);

        isGrabbing = true;
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;

            isGrabbing = false;
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
