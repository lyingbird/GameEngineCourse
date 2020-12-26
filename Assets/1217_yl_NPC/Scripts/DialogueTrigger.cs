using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    private Interface_Manager ui;
    private NPC_Animation currentNPC;

    public CinemachineTargetGroup targetGroup;

    // Start is called before the first frame update
    void Start()
    {
        ui = Interface_Manager.instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ui.inDialogue && currentNPC != null)
        {
            targetGroup.m_Targets[1].target = currentNPC.transform;
            ui.SetCharNameAndColor();
            ui.inDialogue = true;
            ui.CameraChange(true);
            ui.ClearText();
            ui.FadeUI(true, .2f, .65f);//让ui展现的同时 就开始了第一次read text
            currentNPC.TurnToPlayer(transform.position);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {

            currentNPC = other.GetComponent<NPC_Animation>();
            ui.currentNPC = currentNPC;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
            ui.currentNPC = currentNPC;
        }
    }
}
