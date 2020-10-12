using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Collider MainCollider;
    public Collider[] AllColliders;
    
    void Init()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        DoRagdoll(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DoRagdoll(true);
        }
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach(var col in AllColliders)
        {
            col.enabled = isRagdoll;
            MainCollider.enabled = !isRagdoll;
            GetComponent<Rigidbody>().useGravity = !isRagdoll;
            GetComponent<Animator>().enabled = !isRagdoll;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("hit by bullet!");
            DoRagdoll(true);

        }
    }
}
