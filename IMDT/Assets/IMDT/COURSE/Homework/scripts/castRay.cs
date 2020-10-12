using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castRay : MonoBehaviour
{
    public Camera gameCamera;
    public LayerMask Groundmask;
    Ray CameraRay;
    Ray TorrentRay;
    public Transform rayStartPoint;
    LineRenderer linerenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRay = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(CameraRay,out hitInfo,Groundmask))
        {
            TorrentRay = new Ray(rayStartPoint.position, hitInfo.transform.position - rayStartPoint.position);
            Debug.DrawLine(rayStartPoint.position, hitInfo.transform.position - rayStartPoint.position, Color.red);
        }
        else
        {
            TorrentRay = new Ray(rayStartPoint.position, hitInfo.transform.position - rayStartPoint.position);

            Debug.DrawLine(CameraRay.origin, CameraRay.origin + CameraRay.direction * 100, Color.red);
        }
    }
}
