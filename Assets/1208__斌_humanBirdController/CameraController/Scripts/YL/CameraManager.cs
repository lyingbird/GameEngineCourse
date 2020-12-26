using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public CinemachineVirtualCamera[] cameras;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null)
        {
            Debug.Log($"instance of {this.GetType()} already exists, destroying object");
            Destroy(this.gameObject);
        }
    }
    void Start()   
    {
        cameras = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onenterArea(CinemachineVirtualCamera cam)
    {
        for (int i =0;i< cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(cameras[i] == cam);
            
        }
    }
}
