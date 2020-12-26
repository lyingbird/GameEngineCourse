using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace YB
{
    public class HumanBirdStateConvert : MonoBehaviour
    {
        public delegate void OnIsHumanChange(bool value);
        static public OnIsHumanChange onIsHumanChange;
        public RenderPipelineAsset OriginalRenderPipeline;
        public RenderPipelineAsset BirdRenderPipeline;
        

        [SerializeField]
        bool _isHuman = true;
        public bool isHuman
        {
            set {
                _isHuman = value;
                onIsHumanChange(_isHuman);
            }
            get
            {
                return _isHuman;
            }
        }
        public BirdInitialization bird;
        public HumanControll humanControll;
        public float EnergyConsumeSpeed=20;
        public UI_Data uiData;

        // Start is called before the first frame update
        void Start()
        {
            uiData = FindObjectOfType<UI_Data>();
        }

        // Update is called once per frame
        void Update()
        {
            //change to Bird State
            if (Input.GetKeyUp(KeyCode.B))
            {
                if (isHuman)
                {
                    if (null != bird)
                    {
                        GraphicsSettings.renderPipelineAsset = BirdRenderPipeline;
                        Debug.Log("Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                        bird.handleControlToBird(humanControll.transform.position + Vector3.up * 1.5f, humanControll.transform.forward, humanControll.transform.forward);
                        humanControll.enabled = false;
                        isHuman = false;
                    }
                }
                else
                {
                    
                    bird.handleControlToHuman();
                    humanControll.enabled = true;
                    isHuman = true;
                }
            }


            if (!isHuman)
            {
                uiData.currentEnergy -= EnergyConsumeSpeed * Time.deltaTime;
                if (uiData.currentEnergy <= 0.1f)
                {
                    GraphicsSettings.renderPipelineAsset = OriginalRenderPipeline;
                    Debug.Log("Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);

                    bird.handleControlToHuman();
                    humanControll.enabled = true;
                    isHuman = true;
                }
            }
            else
            {
                uiData.currentEnergy = Mathf.Lerp(uiData.currentEnergy, uiData.maxEnergy, 5 * Time.deltaTime);
            }
        }
    }
}
