using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    CinemachineBrain brain;
    Transform camerasGroup;

    public CinemachineFreeLook TPScamera;

    [Header("Camera Settings")]
    public bool activatesCamera = false;
    public CinemachineVirtualCamera UsingCamera;

    public bool cut;

    private void Start()
    {
        camerasGroup = GameObject.Find("Cameras").transform;
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    public void SetCamera()
    {
        Debug.Log("trigger setcamera");
        Debug.Log(this.gameObject);
        brain.m_DefaultBlend.m_Style = cut ? CinemachineBlendDefinition.Style.Cut : CinemachineBlendDefinition.Style.EaseOut;

        if (camerasGroup.childCount <= 0)
        {
            Debug.Log("not find cameras!");
            return;

        }

        for (int i = 0; i < camerasGroup.childCount; i++)
        {
            camerasGroup.GetChild(i).gameObject.SetActive(false);
        }
        UsingCamera.gameObject.SetActive(activatesCamera);

        TPScamera.enabled = !activatesCamera;
        Debug.Log("reporting path!");
        Debug.Log(this.GetComponent<CinemachineDollyCart>().m_Path);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .1f);
    }
}
