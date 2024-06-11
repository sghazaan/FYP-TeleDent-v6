using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include this to work with UI elements

public class DynamicCase : MonoBehaviour
{
    public GameObject object1; // Reference to the first game object
    public GameObject object2; // Reference to the second game object
    public GameObject object3; // Reference to the third game object

    // Lists to hold predefined positions, rotations, and scales
    public List<Vector3> positions = new List<Vector3>();
    public List<Quaternion> rotations = new List<Quaternion>();
    public List<Vector3> scales = new List<Vector3>();

    // Function to randomize the position, rotation, and scale of the game objects
    public void RandomizeObjects()
    {
        // Make a copy of the positions list to track used positions
        List<Vector3> availablePositions = new List<Vector3>(positions);

        // Randomize and set the properties for object1
        object1.transform.position = GetRandomPosition(availablePositions);
        // object1.transform.rotation = rotations[Random.Range(0, rotations.Count)];
        // object1.transform.localScale = scales[Random.Range(0, scales.Count)];

        // Randomize and set the properties for object2
        object2.transform.position = GetRandomPosition(availablePositions);
        // object2.transform.rotation = rotations[Random.Range(0, rotations.Count)];
        // object2.transform.localScale = scales[Random.Range(0, scales.Count)];

        // Randomize and set the properties for object3
        object3.transform.position = GetRandomPosition(availablePositions);
        // object3.transform.rotation = rotations[Random.Range(0, rotations.Count)];
        // object3.transform.localScale = scales[Random.Range(0, scales.Count)];
    }

    // Helper function to get a random position from the available list and remove it from the list
    private Vector3 GetRandomPosition(List<Vector3> availablePositions)
    {
        if (availablePositions.Count == 0)
        {
            Debug.LogWarning("No more unique positions available.");
            return Vector3.zero;
        }

        int randomIndex = Random.Range(0, availablePositions.Count);
        Vector3 randomPosition = availablePositions[randomIndex];
        availablePositions.RemoveAt(randomIndex);
        return randomPosition;
    }    
}
