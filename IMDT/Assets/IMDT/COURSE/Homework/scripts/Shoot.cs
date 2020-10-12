using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100,Color.black);
        if (Input.GetMouseButton(0))
        {
            var hits = Physics.RaycastAll(ray);

            foreach( var hit in hits)
            {
                var enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    Debug.Log("hitted!!!");

                    enemy.DoRagdoll(true);
                }
            }

        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        //Gizmos.DrawRay(ray);
    }
}
