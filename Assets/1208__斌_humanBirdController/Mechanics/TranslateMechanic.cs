using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TranslateMechanicAssist))]
public class TranslateMechanic : Mechanic
{
    public float transformSpeed = 1;
    TranslateMechanicAssist assist;

    private void Start()
    {
        assist = GetComponent<TranslateMechanicAssist>();
        transform.position = assist.startPosition;
    }
    public override void triggeredAction()
    {
        //base.triggeredAction();
        Debug.Log("TranslateMechanic");
        StartCoroutine("TriggerAct");

    }
    IEnumerator TriggerAct()
    {
        while ((transform.position-assist.endPosition).magnitude> 0.01f)
        {
            yield return 0;
            //Debug.Log("lerping");
            transform.position = Vector3.Lerp(transform.position, assist.endPosition, Time.deltaTime * transformSpeed);
        }
        transform.position = assist.endPosition;
    }
    private void OnDrawGizmos()
    {
        var assist = GetComponent<TranslateMechanicAssist>();
        Gizmos.color = Color.green;
        Gizmos.DrawLine(assist.startPosition, assist.endPosition);
        Gizmos.color = Color.red;
        Gizmos.DrawIcon(assist.startPosition, "StartIcon.png");
        Gizmos.color = Color.blue;
        Gizmos.DrawIcon(assist.endPosition, "EndIcon.png");
    }
}
