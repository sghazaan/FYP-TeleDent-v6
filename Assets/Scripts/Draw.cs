//Shady
using UnityEngine;
using System.Collections.Generic;

namespace Shady
{
    public class Draw : MonoBehaviour
    {
        [SerializeField] Camera Cam               = null;
        [SerializeField] LineRenderer trailPrefab = null;

        private LineRenderer currentTrail;
        private List<Vector3> points = new List<Vector3>();

        void Start()
        {
            if(!Cam)
            {
                Cam = Camera.main;
            }//if end
        }//Start() eend

        // Update is called once per frame
        void Update()
        {
  if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
{
    CreateNewLine();
}

if (Input.GetMouseButton(0) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
{
    AddPoint();
}


            if(Input.GetKeyDown(KeyCode.R))
            {
                if(transform.childCount != 0)
                {
                    foreach(Transform R in transform)
                    {
                        Destroy(R.gameObject);
                    }//loop end
                }//if end
            }//if end
            UpdateLinePoints();

    //          if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
    // {
    //     CreateNewLine();
    // }

    // if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
    // {
    //     AddPoint();
    // }
        }//Update() end

        private void CreateNewLine()
        {
            currentTrail = Instantiate(trailPrefab);
            currentTrail.transform.SetParent(transform, true);
            points.Clear();
        }//CreateCurrentTrail() end
 
        private void UpdateLinePoints()
        {
            if(currentTrail != null && points.Count > 1)
            {
                currentTrail.positionCount = points.Count;
                currentTrail.SetPositions(points.ToArray());
            }//if end
        }//UpdateTrailPoints() end

        private void AddPoint()
        {
            // var Ray = Cam.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;
            // if(Physics.Raycast(Ray, out hit))
            // {
            //     if(hit.collider.CompareTag("Writeable"))
            //     {
            //         // points.Add(new Vector3(hit.point.x, 0f, hit.point.z));
            //         points.Add(hit.point);
            //         UpdateLinePoints();
            //         return;
            //     }//if end
            //     else
            //         return;
            // }//if end

             // Get the position of the Oculus Go controller
    Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote);
    Vector3 controllerDirection = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote) * Vector3.forward;

    // Raycast from the controller position in its forward direction
    Ray ray = new Ray(controllerPosition, controllerDirection);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
        if (hit.collider.CompareTag("Writeable"))
        {
            points.Add(hit.point);
            UpdateLinePoints();
            return;
        }
    }
        }//AddPoint() end

    }//class end
}//namespace end