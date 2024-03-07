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
    selectedTool.localPosition = Vector3.zero;
    selectedTool.localRotation = Quaternion.identity; // Reset rotation
    }
}
