using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCollider : MonoBehaviour
{
    public BoxCollider internalBoxCollider;

    private void Awake()
    {
        internalBoxCollider = this.gameObject.GetComponent<BoxCollider>();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(internalBoxCollider.center, internalBoxCollider.size );
    }
}
