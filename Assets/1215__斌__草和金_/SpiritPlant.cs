using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    public class SpiritPlant : MonoBehaviour
    {

        public int trigger = 0;
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("other.tag:"+other.tag);
            //Debug.Log("other.tag == 'Player':" + (other.tag == "Player"));
            trigger = 1;
            if (other.tag == "Player")
            {
                //Debug.Log("FindObjectOfType<UI_Data>()" + FindObjectOfType<UI_Data>());
                FindObjectOfType<UI_Data>().spiritPlantNumber++;
                //Debug.Log(" FindObjectOfType<UI_Data>():  " + FindObjectOfType<UI_Data>());
                //Debug.Log("---------------------spiritPlantNumber++-------------------------");
                //gameObject.SetActive(false);
                gameObject.GetComponent<Animator>().enabled = true;
            }
        }
        private void OnDisable()
        {
           // Debug.Log("you met and disabled one SpiritPlant");
        }
    }

}