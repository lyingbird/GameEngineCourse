using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetCameraToParent : MonoBehaviour
{
    public bool activateCamera;

    [SerializeField]
    public CinemachineFreeLook TPScamera;

    [SerializeField]
    public GameObject parentObject;
    public GameObject childObject;

    public void SetCamera()
    {
        if (activateCamera)
        {
            Debug.Log("starting to set camera");
            TPScamera.LookAt = parentObject.transform;
            TPScamera.Follow = parentObject.transform;
        }
        else
        {
            TPScamera.LookAt = childObject.transform;
            TPScamera.Follow = childObject.transform;
        }
    }
}
