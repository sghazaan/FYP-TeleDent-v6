using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForDrill : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public GameObject PlayerObj;
    public AudioSource src;
    public AudioClip drillSound;
    

    // public TextMeshPro collisionText;
    public float speed;

    void Start(){
        // Vector3 targetPosition = PlayerObj.transform.position + new Vector3(0.12f, 0f, 1.1f);
        // Vector3 targetPosition = PlayerObj.transform.position + new Vector3(0.32f, 0.3f, 1.1f);
        // transform.position = targetPosition;

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
            PlayDrillAudio();
            CheckForCollisions();
        }
    }
     void CheckForCollisions()
    {
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, /*adjust radius as needed*/ 0.3f);
        
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("decayed"))
            {
                DestroyDecayedParticle(collider.gameObject);
            }
        }
    }
    void DestroyDecayedParticle(GameObject particle)
    {
        interactableObjects.Remove(particle);
        Destroy(particle);
    }
    void PlayDrillAudio(){
        src.clip = drillSound;
        src.Play();
    }
    
}
