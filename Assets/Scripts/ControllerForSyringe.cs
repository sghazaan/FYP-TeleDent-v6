using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForSyringe : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public AudioSource audiosource;
    // public GameObject centerCamera;
    public float speed;
    public float swellingAmount = 0.1f; // Adjust this value to control the amount of swelling
    public GameObject BloodSlimeAnim;
    public GameObject BloodSlimeStay;
    private float animationStartTime;
    private ProgressTracker progressTracker;



    void Start()
    {
        // Find the ProgressTracker instance in the scene
        progressTracker = FindObjectOfType<ProgressTracker>();
        if (progressTracker == null)
        {
            Debug.LogError("ProgressTracker not found in the scene!");
        }
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
        // StartCoroutine(PlayBloodAnimationCoroutine(3.0f));
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
            PlaySyringeAudio();
            CheckForCollisions();
        }
        if (animationStartTime > 0) 
        {   
            if (Time.time - animationStartTime >= 3)
             { 
             BloodAnimation(0);
            animationStartTime = 0; 
             } 
        } 
        //  BloodAnimation(1);
        // animationStartTime = Time.time;
        
        }
     void CheckForCollisions()
    {
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.05f);
        
        foreach (Collider collider in colliders)
        {
                if(collider.name == "Gum-LT-11"){
                    //code to change color of the collideed object
                    //FF0005
                // Swell the collided object
               // SwellObject(collider.gameObject);

                 // Change color of the collided object
                //ChangeObjectColor(collider.gameObject);
                // Start the animation and wait for 3 seconds before stopping it

                BloodAnimation(1);
                animationStartTime = Time.time;
                if (progressTracker != null)
                {
                    progressTracker.LogInteraction(gameObject, true);
                }
                } else{
                 if (progressTracker != null)
                {
                    progressTracker.LogInteraction(gameObject, false);
                }
            }
        }
    }
    void PlaySyringeAudio(){
        audiosource.Play();
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
            renderer.material.color = new Color(0f, 0f, 0.0f); // Corresponds to FF0005 in hexadecimal
        }
    }
    void BloodAnimation(int i){
        if(i==1){
            BloodSlimeAnim.SetActive(true);
            BloodSlimeStay.SetActive(true);
        } else if(i==0){
            BloodSlimeAnim.SetActive(false);
        } else{
            BloodSlimeAnim.SetActive(false);
        }

    }
    IEnumerator PlayBloodAnimationCoroutine(float waitTime)
    {
        BloodAnimation(1);  // start anim
        yield return new WaitForSeconds(waitTime);
        BloodAnimation(0);  // stop anim
    }
   
}
