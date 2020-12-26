using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSpirit : MonoBehaviour
{
    public GameObject character;
    public Image spirit;
    public int spiritAdd = 60;
    public bool collision;

    // Start is called before the first frame update
    void Start()
    {
        collision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collision)
        {
            spirit.GetComponent<SpiritCircle> ().maxSpirit += spiritAdd;
            GetComponent<Animator>().CrossFade("SpiritGet", 1);
            collision = false;
        }
    }
}
