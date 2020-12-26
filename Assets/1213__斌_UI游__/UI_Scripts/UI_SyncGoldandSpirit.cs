using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace YB
{
    public class UI_SyncGoldandSpirit : MonoBehaviour
    {
        [HideInInspector]
        public UI_Data data;
        public Text text_goldnumber;
        public Text text_spiritnumber;
        bool goldchange = false;
        bool spiritchange = false;
        int goldvalue = 0;
        int spiritvalue = 0;
        // Start is called before the first frame update
        void Start()
        {
            data = GetComponentInParent<UI_Data>();
            UI_Data.onGoldNumberChange += syncGold;

            UI_Data.onSpiritNumberChange += syncSpirit;
        }

        void syncGold(int value)
        {
            text_goldnumber.text = value.ToString();
        }
        void syncSpirit(int value)
        {
            text_spiritnumber.text = value.ToString();
        }        
        // Update is called once per frame
        void Update()
        {
        }
        private void OnDestroy()
        {
            UI_Data.onGoldNumberChange -= syncGold;
            UI_Data.onSpiritNumberChange -= syncSpirit;
        }
    }

}