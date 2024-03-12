using UnityEngine;
using TMPro;

public class CollisionDetectionForSyringe : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI collisionText;
    string originalText;

    void Start()
    {
        // Store the original text
        originalText = collisionText.text;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the object this script is attached to
        if (collision.gameObject != gameObject && collision.gameObject.CompareTag("gum"))
        {
            // Log the name of the other GameObject involved in the collision
           // Debug.Log("Collision with: " + collision.gameObject.name);

            // Update the TextMeshProUGUI component with the name of the collided object
            collisionText.text = "Detection: " + collision.gameObject.name;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision involves the object this script is attached to
        if (collision.gameObject != gameObject)
        {
            // Reset the text to its original value when the collision ends
            collisionText.text = originalText;
        }
    }
}
