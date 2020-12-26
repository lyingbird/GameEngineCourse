using UnityEngine;
using UnityEngine.UI;

public class SpiritCircle : MonoBehaviour
{
    //灵力的下降倍率/速度*deltatime
    [SerializeField]
    private float ReduceSpeed = 10.0f;

    //获取进度圆形图片与精度Text
    public Image progressCircle;
    public Text spiritNum;

    //获取当前灵力上限
    [SerializeField]
    public float maxSpirit = 300.0f;

    //游戏中灵力极限
    [SerializeField]
    public float spiritLimit = 600.0f;

    //开始倒数
    public bool start;

    //设置进度
    private float currentSpirit;
    private float targetSpirit;

    // Use this for initialization
    void Start()
    {
        spiritNum.text = maxSpirit.ToString();
        currentSpirit = maxSpirit;
        targetSpirit = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            //当当前进度小于目标进度时进入分支
            if (currentSpirit > targetSpirit)
            {
                //当前值进度增加
                currentSpirit -= Time.deltaTime * ReduceSpeed;

                //进度之等于或大于目标进度值时进入分支，设为目标进度
                if (currentSpirit <= targetSpirit)
                {
                    currentSpirit = 0.0f;
                }

                //更新填充圆形进度与文本显示进度
                progressCircle.GetComponent<Image>().fillAmount = currentSpirit / spiritLimit;
                spiritNum.GetComponent<Text>().text = ((int)currentSpirit).ToString();
            }
        }
        else
        {
            if (currentSpirit < maxSpirit)
            {
                //当前值进度增加
                currentSpirit += Time.deltaTime * 60;

                //进度之等于或大于目标进度值时进入分支，设为目标进度
                if (currentSpirit >= maxSpirit)
                {
                    currentSpirit = maxSpirit;
                }

                //更新填充圆形进度与文本显示进度
                progressCircle.GetComponent<Image>().fillAmount = currentSpirit / spiritLimit;
                spiritNum.GetComponent<Text>().text = ((int)currentSpirit).ToString();
            }
        }
    }
}