using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("you met one coin");
            if (other.tag == "Player")
            {
                FindObjectOfType<UI_Data>().goldNumber++;
                //gameObject.SetActive(false);
                gameObject.GetComponent<Animator>().enabled = true;
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
}