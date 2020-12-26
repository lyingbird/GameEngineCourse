using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformsforBC : MonoBehaviour
{
    public float platformSpeed = 1;

    private Transform[] routes;
    private int index = 0;
    private bool goFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "MovingPlatB")
            routes = RoutePointsforB.points;
        if (this.gameObject.name == "MovingPlatC")
            routes = RoutePointsforC.points;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    void MoveTo()
    {
        if(index<(routes.Length-1)/2)
        {
            transform.Translate((routes[index+1].position - transform.position).normalized * Time.deltaTime * platformSpeed);
            if (Vector3.Distance(routes[index+1].position, transform.position) < 1)
            {
                index++;
                goFlag = true;
            }
        }

        else if(index==(routes.Length-1)/2)
        {
            if(goFlag)
            {
                transform.Translate((routes[index + 1].position - transform.position).normalized * Time.deltaTime * platformSpeed);
                if (Vector3.Distance(routes[index + 1].position, transform.position) < 2)
                {
                    index++;
                }
            }
            else
            {
                transform.Translate((routes[index - 1].position - transform.position).normalized * Time.deltaTime * platformSpeed);
                if (Vector3.Distance(routes[index - 1].position, transform.position) < 2)
                {
                    index--;
                }
            }
        }

        else if(index>(routes.Length - 1)/2)
        {
            transform.Translate((routes[index - 1].position - transform.position).normalized * Time.deltaTime * platformSpeed);
            if (Vector3.Distance(routes[index - 1].position, transform.position) < 2)
            {
                index--;
                goFlag = false;
            }
        }
    }

}
