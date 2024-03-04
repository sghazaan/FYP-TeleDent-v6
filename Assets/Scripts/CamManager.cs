using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject toolsContainer;
    public GameObject playerObject;
    public GameObject spToolsContainer;
    public GameObject spPlayerObj;
    private int backPressCount = 0;
    private float lastBackPressTime = 0f;
    private float doublePressTimeThreshold = 2f;
    private bool isSwitching = false;

    void Update()
    {
        // Check for keyboard input (for testing)
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCameras();
        }

        // Check for button press on Oculus Go controller
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        //thats the one I should be using (GPT)

            if (!isSwitching)
            {
                StartCoroutine(SwitchCamerasCoroutine());
            }
        }
    }

    IEnumerator SwitchCamerasCoroutine()
    {
        isSwitching = true;


            // Calculate time since last back button press
            float currentTime = Time.time;
            float timeSinceLastPress = currentTime - lastBackPressTime;

            // Check if it's a double press within the time threshold
            if (timeSinceLastPress <= doublePressTimeThreshold)
            {
                // Increment press count
                backPressCount++;

                // Check if it's the second press
                if (backPressCount == 2)
                {
                    SwitchCameras();
                    // Reset press count and time
                    backPressCount = 0;
                    lastBackPressTime = 0f;
                }
            }
            else
            {
                // Reset press count and update last press time
                backPressCount = 1;
                lastBackPressTime = currentTime;
            }

        // Wait for a short delay to prevent rapid switching
        yield return new WaitForSeconds(0.2f);

        isSwitching = false;
    }



        void SwitchCameras()
    {
        // Toggle between cameras
        if (cam1.activeSelf)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
            Move(cam2.transform.position);

        }
        else if(cam2.activeSelf)
        {
            cam2.SetActive(false);
            cam1.SetActive(true);
            Move(cam1.transform.position);


        }
    }

    
    private void Move(Vector3 position)
    {
        
        if (toolsContainer != null && playerObject != null)
        {
            Rigidbody playerRigidbody = playerObject.GetComponent<Rigidbody>();

            if(cam2.activeSelf){
                playerRigidbody.isKinematic = true;
                toolsContainer.transform.position = position;
                playerObject.transform.position = position;
            }
            if(cam1.activeSelf){
                playerRigidbody.isKinematic = false;
                toolsContainer.transform.position = spToolsContainer.transform.position;
                playerObject.transform.position = spPlayerObj.transform.position;
            }
        }
        else
        {
            Debug.LogError("ToolsContainer/PlayerObject GameObject is not assigned!");
        }
    }
}
