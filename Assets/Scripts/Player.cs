using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 joystick;
    public GameObject centerCamera;
    public GameObject OVRCamera;
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
          if (Application.platform == RuntimePlatform.WindowsPlayer ||
           Application.platform == RuntimePlatform.WindowsEditor)
        {
            // Handle input for Windows (e.g., keyboard input)
            joystick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else{
            joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
        }
        transform.eulerAngles = new Vector3(0, centerCamera.transform.localEulerAngles.y, 0);
        transform.Translate(Vector3.forward * speed * -joystick.y * Time.deltaTime);
        transform.Translate(Vector3.right * speed * -joystick.x * Time.deltaTime);

         // Move OVRCamera position smoothly towards the target position
         OVRCamera.transform.position = Vector3.Lerp(OVRCamera.transform.position, transform.position, 10f * Time.deltaTime);    }
}
