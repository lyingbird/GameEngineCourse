using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddGold : MonoBehaviour
{
    public GameObject character;
    public Text gold;
    public bool collision;

    private int goldAmount;

    // Start is called before the first frame update
    void Start()
    {
        collision = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 90 * Time.deltaTime, 0.0f, Space.World);
        if (collision)
        {
            goldAmount++;
            gold.text = goldAmount.ToString();
            GetComponentInChildren<Animator>().enabled = true;
            collision = false;
        }
    }
}
