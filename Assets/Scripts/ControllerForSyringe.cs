using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForSyringe : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public GameObject PlayerObj;
    public AudioSource src;
    public AudioClip syringeSound;
    // public GameObject centerCamera;
    public float speed;
    public float swellingAmount = 2f; // Adjust this value to control the amount of swelling


    void Start(){
        Vector3 targetPosition = PlayerObj.transform.position + new Vector3(0.12f, 0f, 0.07f);
        transform.position = targetPosition;

    }
    void Update()
    {
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

        Vector2 touchpadInput;
        // Get touchpad input for Oculus
        touchpadInput = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        // Calculate movement direction based on touchpad input
        Vector3 moveDirection = new Vector3(touchpadInput.x, 0f, touchpadInput.y);
        // Move the controller object
        transform.Translate(moveDirection * speed * Time.deltaTime);
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
            PlaySyringeAudio();
            CheckForCollisions();
        }
    }
     void CheckForCollisions()
    {
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, /*adjust radius as needed*/ 0.3f);
        
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("gum"))
            {
                if(collider.name == "Gum1"){
                    //code to change color of the collideed object
                    //FF0005
                // Swell the collided object
                SwellObject(collider.gameObject);

                 // Change color of the collided object
                ChangeObjectColor(collider.gameObject);

                }
            }
        }
    }
    void PlaySyringeAudio(){
        src.clip = syringeSound;
        src.Play();
    }

     void SwellObject(GameObject obj)
    {
        // Get the current scale of the object
        Vector3 originalScale = obj.transform.localScale;

        // Calculate the swollen scale
        Vector3 swollenScale = originalScale * swellingAmount;

        // Apply the swollen scale to the object
        obj.transform.localScale = swollenScale;
    }

     void ChangeObjectColor(GameObject obj)
    {
        // Get the Renderer component of the object
        Renderer renderer = obj.GetComponent<Renderer>();

        // Check if the Renderer component exists
        if (renderer != null)
        {
            // Set the material color of the object to hexadecimal color code FF0005 (bright red)
            renderer.material.color = new Color(1f, 0f, 0.019f); // Corresponds to FF0005 in hexadecimal
        }
    }
   
}
