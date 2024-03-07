using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bufferPickUpController : MonoBehaviour
{
    public Transform toolContainer, tray;
    public GameObject trayAndTools;
    public MonoBehaviour controller;

    private int myToolIndex; // Variable to hold the index of this particular tool
    private static int currentToolIndex = 0; // Static variable to hold the index of the currently selected tool
    private static bool isEquipped = false; // Static variable to track if a tool is currently equipped

    private static List<Transform> tools = new List<Transform>(); // List to hold all the tools

    private void Start()
    {
        // Populate the list of tools from the "trayAndTools" GameObject
        foreach (Transform child in trayAndTools.transform)
        {
            if (child.CompareTag("Tool"))
            {
                tools.Add(child);
                if (child == transform) // Check if this tool matches the current child in the loop
                {
                    myToolIndex = tools.Count - 1; // Assign the index of this tool
                }
            }
        }
    }

    private void Update()
    {
        // Check if the back button (Button Two) is pressed
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (isEquipped)
            {
                Drop(); // Drop the currently equipped tool
                isEquipped = false; // Mark that no tool is currently equipped
            }
            else
            {
                CycleTool(); // Cycle to the next tool
                PickUp(); // Pick up the new current tool
                isEquipped = true; // Mark that a tool is currently equipped
            }
        }

        // Check if "E" is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isEquipped)
            {
                PickUp(); // Pick up the current tool if no tool is equipped
                isEquipped = true; // Mark that a tool is currently equipped
            }
        }

        // Check if "Q" is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isEquipped)
            {
                Drop(); // Drop the currently equipped tool
                isEquipped = false; // Mark that no tool is currently equipped
            }
        }
    }

    private void CycleTool()
    {
        // Increment the current tool index and wrap around if needed
        currentToolIndex = (currentToolIndex + 1) % tools.Count;
    }

    private void PickUp()
    {
        // Check if the current tool index matches this tool's index
        // if (currentToolIndex == myToolIndex)
        // {
            // Get the currently selected tool
            Transform selectedTool = tools[currentToolIndex];

            // Make the selected tool a child of the "toolsContainer" GameObject
            selectedTool.SetParent(toolContainer);

            // Set the position and rotation of the selected tool
            transform.localPosition = new Vector3(0f, 0f, 0f);
            selectedTool.localRotation = Quaternion.identity; // Reset rotation

            // Disable the Rigidbody and Collider of the selected tool
            Rigidbody rb = selectedTool.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            Collider coll = selectedTool.GetComponent<Collider>();
            if (coll != null)
            {
                coll.isTrigger = true;
            }

            controller.enabled = true;
        //}
    }

    private void Drop()
    {
        // Get the currently equipped tool
        Transform equippedTool = toolContainer.GetChild(currentToolIndex); 

        // Make the equipped tool a child of the "trayAndTools" GameObject
        equippedTool.SetParent(null);
        equippedTool.SetParent(tray);

        // Set the position of the equipped tool
        equippedTool.localPosition = tray.transform.position + new Vector3(0f, 0.3f, 0f); // You may need to adjust this based on the tool's position in the tray

        // Enable the Rigidbody and Collider of the equipped tool
        Rigidbody rb = equippedTool.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        Collider coll = equippedTool.GetComponent<Collider>();
        if (coll != null)
        {
            coll.isTrigger = false;
        }

        controller.enabled = false;
    }
}
