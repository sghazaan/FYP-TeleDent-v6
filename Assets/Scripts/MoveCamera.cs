using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    private void Update(){
        transform.position = cameraPosition.position;
    }
}