using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform canvas;

    void Start()
    {
        // Calculate the direction from the camera to the canvas center
        Vector3 directionToCanvas = canvas.position - transform.position;

        // Set the camera's position to a point along the direction towards the canvas center
        transform.position = canvas.position - directionToCanvas.normalized * 10f;

        // Ensure the camera is looking at the canvas center
        transform.LookAt(canvas);
    }
}
