using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;
using Cinemachine;

public class Interface_Manager : MonoBehaviour
{
    public bool inDialogue;

    public static Interface_Manager instance;

    public CanvasGroup canvasGroup;
    public TMP_Animated animatedText;
    public Image nameBubble;
    public TextMeshProUGUI nameTMP;

    [HideInInspector]
    public NPC_Animation currentNPC;

    private int dialogueIndex;
    public bool canExit;
    public bool nextDialogue;

    [Space]

    [Header("Cameras")]
    public GameObject gameCam;
    public GameObject dialogueCam;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        animatedText.onDialogueFinish.AddListener(() => FinishDialogue());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inDialogue)
        {
            if (canExit)
            {
                print("ready to append callback");
                CameraChange(false);
                FadeUI(false, .2f, 0);
                Sequence s = DOTween.Sequence();
                s.AppendInterval(.8f);
                s.AppendCallback(() => ResetState());
                
            }

            if (nextDialogue)
            {
                animatedText.ReadText(currentNPC.dialogue.conversationBlock[dialogueIndex]);
            }
        }
    }



    public void FadeUI(bool show, float time, float delay)
    {
        Sequence s = DOTween.Sequence();
        s.AppendInterval(delay);
        s.Append(canvasGroup.DOFade(show ? 1 : 0, time));
        if (show)
        {
            dialogueIndex = 0;
            s.Join(canvasGroup.transform.DOScale(0, time * 2).From().SetEase(Ease.OutBack));
            s.AppendCallback(() => animatedText.ReadText(currentNPC.dialogue.conversationBlock[0]));
        }
    }

    public void SetCharNameAndColor()
    {
        //设置NPC的姓名/颜色  姓名框的颜色
        nameTMP.SetText(currentNPC.data.villagerName);

        //这三行可以让所有画布重新计算自己应该占据的尺寸，即Content size fitter的刷新，https://forum.unity.com/threads/content-size-fitter-refresh-problem.498536/
        Canvas.ForceUpdateCanvases();
        nameTMP.transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;
        nameTMP.transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = true;

        //nameTMP.text = currentNPC.data.villagerName;
        nameTMP.color = currentNPC.data.villagerNameColor;
        nameBubble.color = currentNPC.data.villagerColor;

    }

    public void CameraChange(bool dialogue)
    {
        gameCam.SetActive(!dialogue);
        dialogueCam.SetActive(dialogue);

        }

    public void ClearText()
    {
        animatedText.text = string.Empty;
    }

    public void ResetState()
    {
        print("called Reset State");
        currentNPC.Reset();
        inDialogue = false;
        canExit = false;
        print("starting turntoState");
        YB.StateBase.talkingState.turnToState(YB.StateBase.groundedState);

    }

    private void FinishDialogue()
    {
        if(dialogueIndex < currentNPC.dialogue.conversationBlock.Count - 1)
        {
            dialogueIndex++;
            nextDialogue = true;
        }
        else
        {
            nextDialogue = false;
            canExit = true;
        }
    }
}
