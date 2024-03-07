using System.Collections;
using UnityEngine;
using TMPro;

public class ErrorDetectionForDrill : MonoBehaviour
{
    public TextMeshProUGUI errorText;
    public GameObject panel; // Declare a UI panel object
 

    void Start()
    {
        // Hide the error text initially
        errorText.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
       
        // Check if the collision is with the wrong object
        if (collision.gameObject.name != "LT-11 cavity tooth" && collision.gameObject != gameObject )
        {
            // Display the error text
            errorText.text = "Error: wrong tooth" + collision.gameObject.name;
            errorText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);




            // Start a coroutine to hide the error text and other objects after a delay
            StartCoroutine(HideObjectsAfterDelay(3f));
        }
    }

    IEnumerator HideObjectsAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Hide the error text
        errorText.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }

    
}
