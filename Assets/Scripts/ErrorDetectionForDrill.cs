using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorDetectionForDrill : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    public AudioSource drillSoundSource;
    public AudioSource errorSoundSource;
    public float speed;
    public GameObject smokeAnim;
    private float animationStartTime;
    private ProgressTracker progressTracker;
    private int destroyedParticlesCount = 0; // Counter to track the number of particles destroyed

    void Start()
    {
        // Find the ProgressTracker instance in the scene
        progressTracker = FindObjectOfType<ProgressTracker>();
    }

    void Update()
    {
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

        Vector2 touchpadInput;
        touchpadInput = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        Vector3 moveDirection = new Vector3(touchpadInput.x, 0f, touchpadInput.y);
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            CheckForCollisions();
        }

        if (animationStartTime > 0)
        {
            if (Time.time - animationStartTime >= 3)
            {
                SmokeAnimation(0);
                animationStartTime = 0;
            }
        }
    }

    void CheckForCollisions()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("decayed"))
            {
                PlayDrillAudio();
                DestroyDecayedParticle(collider.gameObject);
                SmokeAnimation(1);
                animationStartTime = Time.time;
                progressTracker.LogInteraction(gameObject, true);
                // Increment the destroyed particles count
                destroyedParticlesCount++;
            }
            else
            {
                PlayErroneousSound();
                progressTracker.LogInteraction(gameObject, false);
            }
        }
    }

    void DestroyDecayedParticle(GameObject particle)
    {
        interactableObjects.Remove(particle);
        Destroy(particle);
    }

    void PlayDrillAudio()
    {
        drillSoundSource.Play();
    }

    void PlayErroneousSound()
    {
        errorSoundSource.Play();
    }

    void SmokeAnimation(int i)
    {
        if (i == 1)
        {
            smokeAnim.SetActive(true);
        }
        else if (i == 0)
        {
            smokeAnim.SetActive(false);
        }
        else
        {
            smokeAnim.SetActive(false);
        }
    }
}
