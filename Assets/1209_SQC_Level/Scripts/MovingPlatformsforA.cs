using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformsforA : MonoBehaviour
{
    public float platformSpeed = 1;
    public int index = 0;

    private Transform[] routes;

    // Start is called before the first frame update
    void Start()
    {
        routes = RoutePointsforA.points;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    void MoveTo()
    {
        if (index > routes.Length - 1)
        {
            index = 0;
        }

        transform.Translate((routes[index].position - transform.position).normalized * Time.deltaTime * platformSpeed);

        if (Vector3.Distance(routes[index].position, transform.position) < 2)
        {
            index++;
            /*if (index == routes.Length)
            {
                transform.position = routes[index - 1].position;
            }*/
        }
    }
}
