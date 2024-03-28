using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForCarrier : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public AudioSource AudioSource;
    public AudioSource errorSoundSource;
    public float speed;
    private int amalgamCounter = 0;
    private ProgressTracker progressTracker;
    public GameObject thumbsUp;
    public GameObject thumbsDown;

    private bool isCoroutineRunning = false; // Flag to track if a coroutine is running

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
            CheckForCollisions();
        }
    }

    void CheckForCollisions()
    {
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.name == "LT-11 cavity tooth")
            {
                PlayCarrierAudio();
                amalgamCounter++;
                InsertAmalgam();
                progressTracker.LogInteraction(gameObject, true);
                if (!isCoroutineRunning) // Check if coroutine is not already running
                {
                    StartCoroutine(ActivateObjectForTime(thumbsUp, 2f));
                }
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
    }

    void InsertAmalgam()
    {
        if (amalgamCounter <= interactableObjects.Count)
        {
            interactableObjects[amalgamCounter - 1].SetActive(true);
        }
    }

    void PlayCarrierAudio()
    {
        AudioSource.Play();
    }

    void PlayErroneousSound()
    {
        errorSoundSource.Play();
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
}
