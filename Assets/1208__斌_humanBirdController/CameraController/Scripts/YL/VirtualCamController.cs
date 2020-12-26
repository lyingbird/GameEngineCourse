using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject Player;

    CinemachineVirtualCamera cam;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        cam = GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = Player.transform;
        cam.Follow = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
