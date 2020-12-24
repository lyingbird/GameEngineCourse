using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionWithWall : MonoBehaviour
{
    public UnityEvent onTriggerEnter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Track")
        {
            Debug.Log(other.gameObject.name);
            onTriggerEnter.Invoke();

        }

    }

}
