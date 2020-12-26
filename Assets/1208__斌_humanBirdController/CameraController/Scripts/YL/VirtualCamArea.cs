using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(BoxCollider))]

public class VirtualCamArea : MonoBehaviour
{
    
    public CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private Collider BoxCollider;
    private void start()
    {
        BoxCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!virtualCamera) return;
        //Debug.Log($"entered {this.gameObject.name}!!");
        //Debug.Log("1<< other.gameObject.layer:" + (1 << other.gameObject.layer));
        if (other.gameObject.tag == "Player" )
        {
            Debug.Log("detected player!");
            //Debug.Log($"entered {this.gameObject.name}!!");
            CameraManager.instance.onenterArea(virtualCamera);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
