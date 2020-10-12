using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator = null;
    private Rigidbody[] ragdollBodies;
    private Collider[] ragdollColliders;
    private Collider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        ToggleRagdoll(false);

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ToggleRagdoll(bool state)
    {
        animator.enabled = !state;

        foreach(Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }
        foreach(Collider collider in ragdollColliders)
        {
            collider.enabled = state;
        }
    }
    

    public void Die(Transform HitTransform)
    {
        ToggleRagdoll(true);

        foreach(var rb in ragdollBodies)
        {
            rb.AddExplosionForce(100f, HitTransform.position, 5f, 0f,ForceMode.Impulse);
        }
    }
}
