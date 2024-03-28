using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTools : MonoBehaviour
{
    public GameObject trayAndTools;
    public GameObject toolsContainer;
    private GameObject selectedTool;
    private int currentIndex = 0;
    public GameObject mainmenu;
    public GameObject historyMenu;
    public GameObject progressMenu;
    public GameObject ovrcontrollerHand;
    public GameObject camera;
    public GameObject playerObj;
    private Vector3 fixedCameraActualPos; 

    void Start()
    {
        // Initialize selected tool as the first child of trayAndTools
        selectedTool = trayAndTools.transform.GetChild(currentIndex).gameObject;
        selectedTool.transform.SetParent(toolsContainer.transform);
        selectedTool.transform.localPosition = Vector3.zero + new Vector3(0f, -0.15f, 0.25f);
        fixedCameraActualPos = camera.transform.position;
    }

    void Update()
    {
        // Check for button press on Oculus VR controller (Button Two)
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.E))
        {
            if(!mainmenu.activeSelf && !historyMenu.activeSelf && !progressMenu.activeSelf)
            {
            Switch();
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            OpenMainMenu();
        }               
}


    void Switch(){
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
            nextTool.transform.localPosition = Vector3.zero + new Vector3(0f, -0.1f, 0.14f);


            // Update the selectedTool reference
            selectedTool = nextTool;
            currentIndex++;
    }


    void OpenMainMenu(){
            if(!mainmenu.activeSelf){
                //activate
                mainmenu.SetActive(true);
                Vector3 currentPosition = camera.transform.position;
                currentPosition.z -= 0.143f; 
                camera.transform.position = currentPosition;
                playerObj.transform.position = currentPosition;
                // Toggle the activation state of the UI GameObject
                selectedTool.SetActive(false);
                // Set the parent of the currently selected tool back to trayAndTools
                selectedTool.transform.SetParent(trayAndTools.transform);
                selectedTool.transform.localPosition = Vector3.zero;
                ovrcontrollerHand.SetActive(true);
            } else{
                //deactivate
                mainmenu.SetActive(false);
                ovrcontrollerHand.SetActive(false);
                camera.transform.position = fixedCameraActualPos;
                playerObj.transform.position = fixedCameraActualPos;
            }

        
           
    }
}
