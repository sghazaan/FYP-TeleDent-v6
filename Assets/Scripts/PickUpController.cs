using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Transform toolContainer;
    public GameObject trayAndTools;
    public MonoBehaviour controller;

    public const int totalCountOfTools = 5; // Total count of tools
    public static int currentToolIndex = 0; // Static variable to hold the index of the currently selected tool
    public static bool[] isToolEquipped = new bool[totalCountOfTools]; // Array to track whether each tool is equipped
    public static int[] myToolIndex = new int[totalCountOfTools]; // Array to hold the index of each tool

    public static List<Transform> tools = new List<Transform>(); // List to hold all the tools

    public void Start()
    {
        // Initialize arrays
        for (int i = 0; i < totalCountOfTools; i++)
        {
            isToolEquipped[i] = false; // None of the tools are initially equipped
            myToolIndex[i] = -1; // Set default value
        }

        // Populate the list of tools from the "trayAndTools" GameObject
        foreach (Transform child in trayAndTools.transform)
        {
            if (child.CompareTag("Tool"))
            {
                tools.Add(child);

                // Set myToolIndex based on the name of the tool
                switch (child.name)
                {
                    case "syringe":
                        myToolIndex[0] = 0;
                        Debug.Log("0 setup");
                        break;
                    case "drill":
                        myToolIndex[1] = 1;
                        Debug.Log("1 setup");
                        break;
                    case "sickle":
                        myToolIndex[2] = 2;
                        Debug.Log("2 setup");
                        break;
                    case "PinzaBiangulada":
                        myToolIndex[3] = 3;
                        Debug.Log("3 setup");
                        break;
                    case "picker":
                        myToolIndex[4] = 4;
                        Debug.Log("4 setup");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Update()
    {
        // Check if the back button (Button Two) is pressed
        // if (OVRInput.GetDown(OVRInput.Button.Two))
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if the tool is already equipped
            if (!isToolEquipped[currentToolIndex])
            {
                Drop(); // Drop the currently equipped tool
                Debug.Log("Dropped: " + myToolIndex[currentToolIndex] + ":" + currentToolIndex);
                CycleTool(); // Cycle to the next tool
                PickUp(); // Pick up the new current tool
                Debug.Log("Picked: " + myToolIndex[currentToolIndex] + ":" + currentToolIndex);
            }
        }
    }

    public void CycleTool()
    {
        // Increment the current tool index and wrap around if needed
        currentToolIndex = (currentToolIndex + 1) % totalCountOfTools;
    }

    public void PickUp()
    {
        if (myToolIndex[currentToolIndex] >= 0 && myToolIndex[currentToolIndex] < tools.Count)
        {
            Transform selectedTool = tools[myToolIndex[currentToolIndex]];

            // Make sure the tool is not already equipped
            if (!isToolEquipped[currentToolIndex])
            {
                // Make the selected tool a child of the "toolsContainer" GameObject
                selectedTool.SetParent(toolContainer);

                // Set the position and rotation of the selected tool
                selectedTool.localPosition = Vector3.zero;
                selectedTool.localRotation = Quaternion.identity; // Reset rotation
                controller.enabled = true;

                // Mark the tool as equipped
                isToolEquipped[currentToolIndex] = true;
            }
        }
    }

    public void Drop()
    {
        if (currentToolIndex != -1 && currentToolIndex < tools.Count && myToolIndex[currentToolIndex] != -1)
        {
            // Get the currently equipped tool
            Transform equippedTool = tools[myToolIndex[currentToolIndex]];

            // Make sure the tool is equipped
            if (isToolEquipped[currentToolIndex])
            {
                // Make the equipped tool a child of the "trayAndTools" GameObject
                equippedTool.SetParent(trayAndTools.transform);

                // Set the position of the equipped tool
                equippedTool.localPosition = trayAndTools.transform.position;

                controller.enabled = false;

                // Mark the tool as unequipped
                isToolEquipped[currentToolIndex] = false;
            }
        }
    }
}
