 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementController : MonoBehaviour
{
    private float forwardInput;
    private float rightInput;

    public float movingSpeed = 0.05f;

    public float translateDistance = 0.02f;
    private Vector3 velocity;
    public cameraController cameraController;

    public movementHandler characterMovement;
    public animController_Akai characterAnimation;
    private void Start()
    {
        
    }
    private void Update()
    {
    }

    public void AddMovementInput(float forward, float right)
    {
        forwardInput = forward;
        rightInput = right;

        Vector3 camFwd = cameraController.transform.forward;
        Vector3 camRight = cameraController.transform.right;

        Vector3 translation = forward * camFwd;
        translation += right * cameraController.transform.right;
        translation.y = 0;

        if (translation.magnitude > 0)
        {
            velocity = translation;
        }
        else
        {
            velocity = Vector3.zero;
        }

        characterMovement.Velocity = translation * translateDistance;

    }

    public float getVelocity()
    {
        //Debug.Log("velocity:" + velocity);
        return characterMovement.Velocity.magnitude;
    }

    public void ToggleRun()
    {
        if(characterMovement.GetMovementMode()!= MovementMode.Running)
        {
            characterMovement.SetMovementMode(MovementMode.Running);
            characterAnimation.SetMovementMode(MovementMode.Running);
        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
            characterAnimation.SetMovementMode(MovementMode.Walking);

        }
    }

    public void ToggleCrouching()
    {
        if(characterMovement.GetMovementMode()!= MovementMode.Crouching)
        {
            characterMovement.SetMovementMode(MovementMode.Crouching);
            characterAnimation.SetMovementMode(MovementMode.Crouching);

        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Walking);
            characterAnimation.SetMovementMode(MovementMode.Walking);

        }
    }

    public void ToggleSprint(bool enable)
    {
        if (enable)
        {
            characterMovement.SetMovementMode(MovementMode.Sprinting);
            characterAnimation.SetMovementMode(MovementMode.Sprinting);

        }
        else
        {
            characterMovement.SetMovementMode(MovementMode.Running);
            characterAnimation.SetMovementMode(MovementMode.Walking);

        }
    }
}
