using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(movementController))]
[RequireComponent(typeof(movementHandler))]
public class PlayerInput : MonoBehaviour
{
    private movementController controller;
    private movementHandler movementHandler;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<movementController>();
        movementHandler = GetComponent<movementHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.AddMovementInput( Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            controller.ToggleRun();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.ToggleCrouching();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.ToggleSprint(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            controller.ToggleSprint(false);
        }



    }
}
