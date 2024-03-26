using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    // The name of the scene to load when the player collides with the door
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("Collided");
        }
    }
}
