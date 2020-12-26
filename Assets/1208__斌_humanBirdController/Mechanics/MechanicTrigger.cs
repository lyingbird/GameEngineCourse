using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicTrigger : MonoBehaviour
{
    public Mechanic targetMechanic;
    // Start is called before the first frame update
    void Start()
    {
        if (targetMechanic == null)
        {
            Debug.LogError("You MUST assign a targetMechanic to MechanicTrigger component.");
        }
    }

    void Update() 
    {
        transform.Rotate(0.0f, 45 * Time.deltaTime, 0.0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInChildren<PlayerMechanicTrigger>()|| other.GetComponentInParent<PlayerMechanicTrigger>())
        {
            Debug.Log("PlayerEnter" + name);
            targetMechanic.trigger();
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
