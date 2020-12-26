using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePointsforA : MonoBehaviour
{
    public static Transform[] points;

    // Start is called before the first frame update
    void Awake()
    {
        int count = transform.childCount;
        points = new Transform[count];

        for (int i = 0; i < count; i++)
            points[i] = transform.GetChild(i);
    }
}
