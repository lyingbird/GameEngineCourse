using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInitialization : MonoBehaviour
{
    [Header("初始化")]
    ///////会改的地方///////
    public BirdController birdController;
    public CameraController birdCameraController;
    ////////////////////////
    [SerializeField]
    [Tooltip("鸟的初始位置，世界坐标")]
    private Vector3 InitPosition = Vector3.zero;
    [SerializeField]
    [Tooltip("鸟的初始飞行方向，世界坐标")]
    private Vector3 InitForward = Vector3.forward;
    [SerializeField]
    [Tooltip("跟随相机的初始方向，世界坐标")]
    private Vector3 InitCameraLookDir = Vector3.forward;
    bool birdcontrolling = false;
    public bool birdControlling
    {
        get { return birdcontrolling; }
        private set { birdcontrolling = value; }
    }
    bool lastBirdControllingState = false;
    bool birdControlStart = false;
    bool birdControlEnd = false;
    private void Start()
    {
        if(birdController==null|| birdCameraController==null)
        {
            Debug.LogError("BirdController and CameraComtroller MUST be assigned");
        }
    }
    //1.进行位置初始化，方向初始化
    //2.相机的控制权转交给这里，相机的初始朝向，相机的焦距如何
    //3.开始之前预留出动画表现的空间
    ////把这个脚本真正和另外两个挂钩
    void Update()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        //有可能更改状态的代码放在最上面
        if (birdControlling)
        {
            birdControl();
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            birdControlling = !birdControlling;
        }
        if (birdControlling == true&&lastBirdControllingState==false)
        {
            birdControlStart = true;
        }
        if(birdControlling == false && lastBirdControllingState == true)
        {
            birdControlEnd = true;
        }
        if (birdControlStart)
        {
            birdControlStart = false;
            birdStart();
        }
        if (birdControlEnd)
        {
            birdControlEnd = false;
            birdEnd();
        }
        lastBirdControllingState = birdControlling;
    }
    void birdStart()
    {
        //Debug.Log("Start Bird");
        if (GetComponentInChildren<BirdController>() != null)
        {
            Debug.Log("Successfully get BirdController!");
            GetComponentInChildren<BirdController>().InitForward = InitForward;
            GetComponentInChildren<BirdController>().InitPosition = InitPosition;
        }
        birdController.InitForward = InitForward;
        birdController.InitPosition = InitPosition;
        if (GetComponentInChildren<CameraController>() != null)
        {
            Debug.Log("Successfully get CameraController!");
            GetComponentInChildren<CameraController>().InitLookDir = InitCameraLookDir;
        }
        birdCameraController.InitLookDir = InitCameraLookDir;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    void birdControl()
    {

    }
    void birdEnd()
    {
        //4.可能会做一些表现

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 外部如果要把控制权交给bird，就调用这个函数就好了，可以给出
    /// 鸟的初始飞行方向和位置，还有初始相机看向的方向。
    /// 但是也有默认值重载的版本，可以不给。
    /// </summary>
    /// <param name="_InitPosition"></param>
    /// <param name="_InitForward"></param>
    /// <param name="_InitCameraLookDir"></param>
    public void handleControlToBird(Vector3 _InitPosition, Vector3 _InitForward, Vector3 _InitCameraLookDir)
    {
        InitPosition = _InitPosition;
        InitForward = _InitForward;
        InitCameraLookDir = _InitCameraLookDir;

        //Debug.Log("InitPosition:" + InitPosition);
        //Debug.Log("_InitPosition" + _InitPosition);
        birdControlling = true;
    }
    /// <summary>
    /// 外部如果要把控制权交给bird，就调用这个函数就好了。
    /// 另外有一个重载版本可以指定鸟初始化时的位置方向和相机角度
    /// </summary>
    public void handleControlToBird()
    {
        InitPosition = Vector3.zero;
        InitForward = Vector3.forward;
        InitCameraLookDir = Vector3.forward;
        birdControlling = true;
    }
    public void handleControlToHuman()
    {
        //Debug.Log("Human controlling");
        birdControlling = false;
        //Debug.Log(birdControlling);

    }
}
