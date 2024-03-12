using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSyringe : MonoBehaviour
{
    public Transform toolContainer;
    public Transform selectedTool;

    
    public void Start()
    {
    selectedTool.SetParent(toolContainer);
    selectedTool.localPosition = Vector3.zero;
    // Reset rotation (x: 0, y: 0, z: 0)
    Quaternion resetRotation = Quaternion.identity;

    // Set x-axis rotation to 180 degrees (or pi radians)
    resetRotation.eulerAngles = new Vector3(180f, resetRotation.eulerAngles.y, resetRotation.eulerAngles.z);

    selectedTool.localRotation = resetRotation;
    }    
}
