using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerForDrill : MonoBehaviour
{
    public List<GameObject> interactableObjects; // List to hold interactable objects
    // public GameObject PlayerObj;
    public AudioSource drillSoundSource;
    public AudioSource errorSoundSource;
    // public TextMeshPro collisionText;
    public float speed;
    public GameObject smokeAnim;
    private float animationStartTime;
    private ProgressTracker progressTracker;
    public GameObject thumbsUp;
    public GameObject thumbsDown;



    void Start()
    {
        // Find the ProgressTracker instance in the scene
        progressTracker = FindObjectOfType<ProgressTracker>();
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
        // Perform collision detection logic here
        Collider[] colliders = Physics.OverlapSphere(transform.position, /*adjust radius as needed*/ 0.3f);
        
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("decayed"))
            {
                PlayDrillAudio();
                DestroyDecayedParticle(collider.gameObject);
                SmokeAnimation(1);
                animationStartTime = Time.time;
                progressTracker.LogInteraction(gameObject, true);
                StartCoroutine(ActivateObjectForTime(thumbsUp, 2f));
            }else{
                
                // PlayErroneousSound();
                progressTracker.LogInteraction(gameObject, false);  
                // StartCoroutine(ActivateObjectForTime(thumbsDown, 2f));      
            }
        }
    }
    void DestroyDecayedParticle(GameObject particle)
    {
        interactableObjects.Remove(particle);
        Destroy(particle);
    }
    void PlayDrillAudio(){
        drillSoundSource.Play();
    }
    void PlayErroneousSound(){
        errorSoundSource.Play();
    }
     void SmokeAnimation(int i){
        if(i==1){
            smokeAnim.SetActive(true);
        } else if(i==0){
            smokeAnim.SetActive(false);
        } else{
            smokeAnim.SetActive(false);
        }

    }

    IEnumerator ActivateObjectForTime(GameObject obj, float duration)
    {
        // Activate the GameObject
        obj.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the GameObject after the specified duration
        obj.SetActive(false);
    }
    
}