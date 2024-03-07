using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCopy : MonoBehaviour
{
    // Reference to the GameObject whose color you want to copy
    public GameObject sourceObject;

    // Reference to the GameObject to which you want to apply the copied color
    public GameObject targetObject;

    void Start()
    {
        if (sourceObject != null && targetObject != null)
        {
            // Check if the source object has a Renderer component
            Renderer sourceRenderer = sourceObject.GetComponent<Renderer>();
            if (sourceRenderer != null)
            {
                // Get the material attached to the source object
                Material sourceMaterial = sourceRenderer.material;

                // Get the color from the material
                Color sourceColor = sourceMaterial.color;

                // Apply the color to the target object
                Renderer targetRenderer = targetObject.GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    // Create a new material instance for the target object
                    targetRenderer.material = new Material(sourceMaterial);

                    // Apply the color to the material of the target object
                    targetRenderer.material.color = sourceColor;
                }
            }
        }
    }
}
