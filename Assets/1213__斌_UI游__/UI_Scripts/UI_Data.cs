using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    public class UI_Data : MonoBehaviour
    {
        public delegate void OnGoldNumberChange(int i);
        public static OnGoldNumberChange onGoldNumberChange;

        public delegate void OnSpiritNumberChange(int i);
        public static OnSpiritNumberChange onSpiritNumberChange;

        public delegate void OnCurrentEnergyChange(float current,float max);
        public static OnCurrentEnergyChange onCurrentEnergyChange;
        int _goldNumber = 0;
        int _spiritPlantNumber = 0;
        float _currentEnergy=0;
        public int goldNumber
        {
            set
            {
                if (value <= 0) value = 0;
                //当有灵草的数目变化的时候自动修改maxEnergy
                _goldNumber = value;
                onGoldNumberChange(value);
            }
            get
            {
                return _goldNumber;
            }
        }
        public int spiritPlantNumber
        {
            set
            {
                if (value <= 0) value = 0;
                //当有灵草的数目变化的时候自动修改maxEnergy
                _spiritPlantNumber = value;
                onSpiritNumberChange(value);
            }
            get
            {
                return _spiritPlantNumber;
            }
        }
        public float currentEnergy
        {
            set
            {
                if (value <= 0) value = 0;
                if (value >= maxEnergy) value = maxEnergy;
                //当有灵草的数目变化的时候自动修改maxEnergy
                _currentEnergy = value;
                if(onCurrentEnergyChange!=null)
                onCurrentEnergyChange(value, maxEnergy);
            }
            get
            {
                return _currentEnergy;
            }
        }
        public float energyPerPlants = 60;
        public float maxEnergy = 0;
        public GameObject exittingUI;
        public GameObject gameUI;
        // Start is called before the first frame update
        void Start()
        {
            maxEnergy = 0;
            currentEnergy = maxEnergy;
            goldNumber = spiritPlantNumber = 0;
            onSpiritNumberChange += maxEnergySync;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)&&FindObjectOfType<HumanBirdStateConvert>().isHuman)
            {
                exittingUI.SetActive(true);
                gameUI.SetActive(false);
            }
        }
        void maxEnergySync(int value)
        {
            maxEnergy = energyPerPlants * value;
        }
    }
}
