using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need to change it such that all tools are in a list and they are selected in a cycle
public class PickUpController : MonoBehaviour
{
     public MonoBehaviour controller;
    public Rigidbody rb;
    public MeshCollider coll;
    public Transform player, toolContainer, fpsCam;

    public Transform tray;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            controller.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            controller.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {

        // Check if the back button (Button Two) is pressed
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            // If equipped, drop; if not equipped, pick up
            if (equipped)
            {
                Drop();
            }
            else
            {
                // Check if player is in range
                Vector3 distanceToPlayerOculus = player.position - transform.position;
                if (distanceToPlayerOculus.magnitude <= pickUpRange && !slotFull)
                {
                    PickUp();
                }
            }
        }

        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(toolContainer);
        transform.localPosition = new Vector3(0f, -0.3f, 0.22f);
        // transform.localRotation = Quaternion.Euler(Vector3.zero);
        // transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable script
        controller.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);
           // Set player's Rigidbody velocity to zero to stop its movement
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = Vector3.zero;

       // Get the target position (e.g., anotherObject's position with a y-offset)
    Vector3 targetPosition = tray.position + new Vector3(0f, 0.3f, 0f);

    // Set the tool's position to the target position
    transform.position = targetPosition;

       

        //Disable script
        controller.enabled = false;
    }
}
