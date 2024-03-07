using System.Collections;
using UnityEngine;
using TMPro;

public class ErrorDetectionForSyringe : MonoBehaviour
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
        if (collider.CompareTag("gum")){

       
        // Check if the collision is with the wrong object
        if (collision.gameObject.name != "Gum-LT-11" && collision.gameObject != gameObject )
        {
            // Display the error text
            errorText.text = "Error: wrong gum" + collision.gameObject.name;
            errorText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);




            // Start a coroutine to hide the error text and other objects after a delay
            StartCoroutine(HideObjectsAfterDelay(3f));
        }
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
