using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cinemachine;

public class NPC_Animation : MonoBehaviour
{
    public NPC_Data data;
    public Dialogue_Data dialogue;

    public bool npcIsTalking;

    private TMP_Animated animatedText;
    private Animator animator;

    [Space]
    [Header("表情需要的各种参数")]

    public SkinnedMeshRenderer Emoji;
    public Material originalFace;
    public Material scaredFace;
    public Material happyFace;

    void Start()
    {
        animator = GetComponent<Animator>();
        animatedText = Interface_Manager.instance.animatedText;

        animatedText.onEmotionChange.AddListener((newEmotion) => TriggerEmotion(newEmotion));
        animatedText.onAction.AddListener((action) => TriggerAction(action));


    }

    public void TriggerEmotion(Emotion e)
    {
        if (this != Interface_Manager.instance.currentNPC)
            return;

        animator.SetTrigger(e.ToString());

    }

    public void TriggerAction(string action)
    {
        if (this != Interface_Manager.instance.currentNPC)
            return;

        if (action == "shake")
        {
            Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }

        if(action == "scared")
        {
            Emoji.material = scaredFace;
        }

    }


    public void Reset()
    {
        animator.SetTrigger("normal");
        Emoji.material = originalFace;
    }

    public void TurnToPlayer(Vector3 playerPos)
    {
        transform.DOLookAt(playerPos, Vector3.Distance(transform.position, playerPos) / 5);
    }

}
