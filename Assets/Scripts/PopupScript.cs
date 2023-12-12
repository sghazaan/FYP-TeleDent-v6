using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScript : MonoBehaviour
{
    private bool open;
    public Vector3 restPos;
    public Vector3 otherPos;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
        otherPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(!open){
        transform.position = Vector3.Lerp(transform.position, otherPos, Time.deltaTime * 5f);
        }
        if(open){
            transform.position = Vector3.Lerp(transform.position, restPos, Time.deltaTime * 5f);
        }
        
    }
    void OnVRTriggerDown(){
        open = true;
    }
}
