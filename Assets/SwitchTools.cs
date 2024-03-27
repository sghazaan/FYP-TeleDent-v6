using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTools : MonoBehaviour
{
    public GameObject trayAndTools;
    public GameObject toolsContainer;
    private GameObject selectedTool;
    private int currentIndex = 0;

    void Start()
    {
        // Initialize selected tool as the first child of trayAndTools
        selectedTool = trayAndTools.transform.GetChild(currentIndex).gameObject;
        selectedTool.transform.SetParent(toolsContainer.transform);
        selectedTool.transform.localPosition = Vector3.zero + new Vector3(0f, -0.15f, 0.25f);
    }

    void Update()
    {
        // Check for button press on Oculus VR controller (Button Two)
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Current Index: " + currentIndex);
            Debug.Log("Current Tool: " + selectedTool.name);

            // Get the next tool in the series
            int nextIndex = (currentIndex + 1) % trayAndTools.transform.childCount;
            GameObject nextTool = trayAndTools.transform.GetChild(nextIndex).gameObject;
            nextTool.SetActive(true);
            Debug.Log("Next Index: " + nextIndex);
            Debug.Log("Next Tool: " + nextTool.name);
            selectedTool.SetActive(false);


            // Set the parent of the currently selected tool back to trayAndTools
            selectedTool.transform.SetParent(trayAndTools.transform);
            selectedTool.transform.localPosition = Vector3.zero;


            // Set the parent of the next tool to toolsContainer
            nextTool.transform.SetParent(toolsContainer.transform);
            nextTool.transform.localPosition = Vector3.zero + new Vector3(0f, -0.1f, 0.1f);


            // Update the selectedTool reference
            selectedTool = nextTool;
            currentIndex++;
        }
    }
}
