using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YB;
using Cinemachine;

namespace YL
{
    public class TalkingState : StateBase
    {

        public Interface_Manager ui;
        public NPC_Animation currentNPC;

        public CinemachineTargetGroup targetGroup;

        // Start is called before the first frame update
        private void Awake()
        {
            if (talkingState == null)
            {
                talkingState = this;
            }
            else
            {
                Destroy(this);
            }
        }

        void Start()
        {
            ui = Interface_Manager.instance;

        }

        // Update is called once per frame
        void Update()
        {
        }
        public override void handleInput()
        {
            base.handleInput();
        }

        private void OnTriggerEnter(Collider other)
        {
            print(other.name);
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

        public override void enter()
        {
            print("entered talking state");

            this.GetComponent<CharacterController>().enabled = false;
            this.GetComponent<Animator>().SetFloat("Velocity",0);

            base.enter();
            targetGroup.m_Targets[1].target = currentNPC.transform;
            ui.SetCharNameAndColor();
            ui.inDialogue = true;
            ui.CameraChange(true);
            ui.ClearText();
            ui.FadeUI(true, .2f, .65f);//让ui展现的同时 就开始了第一次read text
            currentNPC.TurnToPlayer(transform.position);
        }

        public override void exit()
        {
            print("exited talking state");
            this.GetComponent<CharacterController>().enabled = true;

        }

    }
}

