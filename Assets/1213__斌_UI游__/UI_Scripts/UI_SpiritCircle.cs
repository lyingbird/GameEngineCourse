using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YB
{
    public class UI_SpiritCircle : MonoBehaviour
    {
        public Image spiritCircle;
        bool syncCurrent = false;
        bool humanchange = false;
        bool ishuman = false;
        float currentPercentage = 0f;
        // Start is called before the first frame update
        void Start()
        {
            if (spiritCircle == null)
            {
                spiritCircle = GetComponentInChildren<Image>();
            }
            HumanBirdStateConvert.onIsHumanChange += onIsHumanChange;
            UI_Data.onCurrentEnergyChange += syncSpiritCircleFills;
        }
        void onIsHumanChange(bool _ishuman)
        {
            spiritCircle.gameObject.SetActive(!_ishuman);
            //humanchange = true;
            //ishuman = _ishuman;
        }
        private void Update()
        {
            //if (syncCurrent)
            //{
            //    syncCurrent = false;
            //    spiritCircle.fillAmount = Mathf.Lerp(spiritCircle.fillAmount, currentPercentage, 5 * Time.deltaTime);
            //}
            //if (humanchange)
            //{
            //}
        }
        void syncSpiritCircleFills(float current,float max)
        {
            if (max != 0)
                spiritCircle.fillAmount = Mathf.Lerp(spiritCircle.fillAmount, current / max, 5 * Time.deltaTime);
            //syncCurrent = true;
            //if (max!=0)
            //    currentPercentage = current / max;
        }
        private void OnDestroy()
        {
            HumanBirdStateConvert.onIsHumanChange -= onIsHumanChange;
            UI_Data.onCurrentEnergyChange -= syncSpiritCircleFills;
        }
    }
}
