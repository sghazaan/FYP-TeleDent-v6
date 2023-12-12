using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractTooth : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;

    void Update()
    {
        
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

   
}
