using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForCarrier : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public AudioSource AudioSource;
    

    // public TextMeshPro collisionText;
    public float speed;
    private int amalgamCounter = 0;

    void Start(){
       

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
            PlayCarrierAudio();
            CheckForCollisions();
        }



        
    }
     void CheckForCollisions()
    {
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.05f);
        
        foreach (Collider collider in colliders)
        {
        if (collider.gameObject.name == "LT-11 cavity tooth")
        {
            amalgamCounter++;
            InsertAmalgam();
        }
        }

    }
     void InsertAmalgam()
    {
        if (amalgamCounter <= interactableObjects.Count)
        {
            interactableObjects[amalgamCounter - 1].SetActive(true);
        }
        else
        {
            // Do nothing
        }
    }
    void PlayCarrierAudio(){
        AudioSource.Play();
    }
    
}
