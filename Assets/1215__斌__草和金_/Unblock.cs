using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unblock : MonoBehaviour
{
    public GameObject Block;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("you met one coin");
        if (other.tag == "Player")
        {
            Block.GetComponent<Animator>().enabled = true;
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        //Debug.Log("you met and disabled one coin");
    }

    void Update()
    {
        transform.Rotate(0.0f, 90 * Time.deltaTime, 0.0f, Space.World);
    }
}
