using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTool : MonoBehaviour
{
    public Transform toolContainer;
    public Transform selectedTool;

    
    public void Start()
    {
    GetToStartingPoint();

    }    
    public void GetToStartingPoint(){
    selectedTool.SetParent(toolContainer);
    selectedTool.localPosition = Vector3.zero + new Vector3(0f, -0.1f, 0.1f);
    selectedTool.localRotation = Quaternion.identity; // Reset rotation
    }
}
