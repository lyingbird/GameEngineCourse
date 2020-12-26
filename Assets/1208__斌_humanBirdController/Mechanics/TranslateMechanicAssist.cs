using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TranslateMechanicAssist : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public bool initializing = false;
    // Start is called before the first frame update
    void Start()
    {
        if (initializing && start == null && end == null)
        {
            start = new GameObject();
            end = new GameObject();
            start.transform.position = transform.position;
            start.name = "StartPoint";
            end.transform.position = transform.position;
            end.name = "EndPoint";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        startPosition = start.transform.position;
        endPosition = end.transform.position;
    }

}
