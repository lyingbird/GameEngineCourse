using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementMode { Walking, Running, Crouching, Proing, Swimming, Sprinting }
[RequireComponent(typeof(Rigidbody))]

public class movementHandler : MonoBehaviour
{


    public Transform t_mesh;
    public float maxSpeed = 0.1f;
    private Rigidbody rb;
    private MovementMode movementMode;

    private Vector3 velocity;

    private float smoothSpeed;

    public float walkSpeed = 0.5f;
    public float runSpeed = 6.7f;
    public float sprintSpeed = 9.5f;
    public float crouchSpeed = 3.3f;
    public float proneSpeed = 1f;
    public float swimSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // init rb reference
        SetMovementMode(MovementMode.Walking);
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(velocity.normalized * maxSpeed);

        if(velocity.magnitude > 0)
        {
            rb.velocity = new Vector3(velocity.normalized.x * smoothSpeed, 0, velocity.normalized.z * smoothSpeed);
            smoothSpeed = Mathf.Lerp(smoothSpeed, maxSpeed, Time.deltaTime);
            t_mesh.rotation = Quaternion.Lerp(t_mesh.rotation,Quaternion.LookRotation(velocity),Time.deltaTime * 10);
        }
        else
        {
            smoothSpeed = Mathf.Lerp(smoothSpeed, 0, Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Time.timeScale = 1f;
        }
    }

    public Vector3  Velocity
    {
        get => rb.velocity;
        set => velocity = value;
    }

    public void SetMovementMode(MovementMode mode)
    {
        movementMode = mode;
        switch (mode)
        {
            case MovementMode.Walking:
                {
                    maxSpeed = walkSpeed;
                    break;
                }
            case MovementMode.Running:
                {
                    maxSpeed = runSpeed;
                    break;
                }
            case MovementMode.Crouching:
                {
                    maxSpeed = crouchSpeed;
                    break;
                }
            case MovementMode.Proing:
                {
                    maxSpeed = proneSpeed;
                    break;
                }
            case MovementMode.Swimming:
                {
                    maxSpeed = swimSpeed;
                    break;
                }
               
            case MovementMode.Sprinting:
                {
                    maxSpeed = sprintSpeed;
                    break;
                }
                
        }
    }

    public MovementMode GetMovementMode()
    {
        return movementMode;
    }
}
