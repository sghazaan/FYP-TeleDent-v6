using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForSyringe : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public AudioSource audiosource;
    public float speed;
    public float swellingAmount = 0.1f; // Adjust this value to control the amount of swelling
    public GameObject BloodSlimeAnim;
    public GameObject BloodSlimeStay;
    private float animationStartTime;
    private ProgressTracker progressTracker;
    public AudioSource errorSoundSource;
    public GameObject thumbsUp;
    public GameObject thumbsDown;

    private bool isCoroutineRunning = false; // Flag to track if a coroutine is running
    private bool canCheckCollisions = true; // Flag to control collision processing

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
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (canCheckCollisions)
            {
                CheckForCollisions();
            } 
        }
        if (animationStartTime > 0)
        {
            if (Time.time - animationStartTime >= 3)
            {
                BloodAnimation(0);
                animationStartTime = 0;
            }
        }
    }

    void CheckForCollisions()
    {
        canCheckCollisions = false;
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.15f);

        foreach (Collider collider in colliders)
        {
           
            if (collider.name == "Gum-LT-11")
            {
                if (!isCoroutineRunning) 
                {
                    StartCoroutine(ActivateObjectForTime(thumbsUp, 2f));
                }
                BloodAnimation(1);
                PlaySyringeAudio();
                animationStartTime = Time.time;
                progressTracker.LogInteraction(gameObject, true);
            }
            else
            {
                PlayErroneousSound();
                progressTracker.LogInteraction(gameObject, false);
                if (!isCoroutineRunning) // Check if coroutine is not already running
                {
                    StartCoroutine(ActivateObjectForTime(thumbsDown, 2f));
               }
            }
        }

        StartCoroutine(DelayedCollisionCheck(2f));
    }

    void PlaySyringeAudio()
    {
        audiosource.Play();
    }

    void PlayErroneousSound()
    {
        errorSoundSource.Play();
    }

    void BloodAnimation(int i)
    {
        if (i == 1)
        {
            BloodSlimeAnim.SetActive(true);
            BloodSlimeStay.SetActive(true);
        }
        else if (i == 0)
        {
            BloodSlimeAnim.SetActive(false);
        }
        else
        {
            BloodSlimeAnim.SetActive(false);
        }

    }

    IEnumerator ActivateObjectForTime(GameObject obj, float duration)
    {
        // Set the coroutine flag to true
        isCoroutineRunning = true;

        // Activate the GameObject
        obj.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the GameObject after the specified duration
        obj.SetActive(false);

        // Reset the coroutine flag to false
        isCoroutineRunning = false;
    }

     IEnumerator DelayedCollisionCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCheckCollisions = true;
    }
}


 //code to change color of the collideed object
                    //FF0005
                // Swell the collided object
               // SwellObject(collider.gameObject);

                 // Change color of the collided object
                //ChangeObjectColor(collider.gameObject);
                // Start the animation and wait for 3 seconds before stopping it
