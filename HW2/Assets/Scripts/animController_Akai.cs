using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(movementController))]
public class animController_Akai : MonoBehaviour
{
    public Animator anim;
    private movementController controller;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<movementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim == null)
        {
            return;
        }
        speed = Mathf.SmoothStep(speed, controller.getVelocity(), Time.deltaTime*20);
        Debug.Log("velocity:" + controller.getVelocity());
        anim.SetFloat("velocity",speed);
    }

    public void SetMovementMode(MovementMode mode)
    {

        switch (mode)
        {
            case MovementMode.Walking:
                {
                    anim.SetInteger("movementState", 0);
                    break;
                }
            case MovementMode.Running:
                {
                    anim.SetInteger("movementState", 0);
                    break;
                }
            case MovementMode.Crouching:
                {
                    anim.SetInteger("movementState", 1);
                    break;
                }
            case MovementMode.Proing:
                {
                    anim.SetInteger("movementState", 2);
                    break;
                }
            case MovementMode.Swimming:
                {
                    anim.SetInteger("movementState", 3);
                    break;
                }

            case MovementMode.Sprinting:
                {
                    anim.SetInteger("movementState", 0);
                    break;
                }

        }
    }
}
