using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    public class HumanControll : MonoBehaviour
    {
        /// <summary>
        /// HumanControll 会保留保有一个state表示当前的状态，会用来处理state的handleInput
        /// </summary>
        public StateBase state;
        /// <summary>
        /// 移动速度，会被用来同步动画和实际移动，会在moveSpeed和walkSpeed之间插值
        /// </summary>
        [HideInInspector]
        public float moveSpeed = 1f;
        /// <summary>
        /// 走路速度
        /// </summary>
        [Header("走 跑 跳跃")]
        [Tooltip("走路速度")]
        [Range(0.1f,10)]
        public float walkSpeed = 1f;
        /// <summary>
        /// 跑步速度
        /// </summary>
        [Tooltip("跑步速度")]
        [Range(2f, 20)]
        public float runSpeed = 2f;
        /// <summary>
        /// 速度切换按键
        /// </summary>
        [Tooltip("用来切换速度的按键")]
        public KeyCode speedSwitch = KeyCode.LeftShift;
        /// <summary>
        /// 表示是否起跳，中间加一个变量来表示是否获得了跳跃输入，这样可以把所有输入相关的代码锁在humanControll里
        /// </summary>
        [HideInInspector]
        public bool getJumpKey = false;
        /// <summary>
        /// 跳跃按键
        /// </summary>
        [Tooltip("用来跳跃的按键")]
        public KeyCode jumpKey = KeyCode.Space;
        /// <summary>
        /// 是否在跑动，跑动的时候速度会用runSpeed，反之则用walkSpeed
        /// </summary>
        bool running = false;
        [Header("动画同步")]
        /// <summary>
        /// 动画的速度同步，值越大动画播放速度越慢
        /// </summary>
        [Tooltip("动画的速度同步，值越大动画播放速度越慢")]
        public float animationSynchrolizer = 1;
        /// <summary>
        /// 动画的同步属性lerping的速度
        /// </summary>
        [Tooltip("动画的同步属性lerping的速度")]
        public float animationMergeSpeed = 1f;
        private float x;
        private float y;
        /// <summary>
        /// 外部只能get，但是不能set
        /// </summary>
        public float xInput {
            get { return x; }
            private set { x = value; }
        }
        /// <summary>
        /// 外部只能get，但是不能set
        /// </summary>
        public float yInput
        {
            get { return y; }
            private set { y = value; }
        }
        // Start is called before the first frame update
        void Start()
        {
            //初始化的时候只需要让state指向一个地面状态就好了
            state = StateBase.groundedState;
        }

        // Update is called once per frame
        void Update()
        {
            //首先获得输入
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
            //跳跃按键也要获得以下
            getJumpKey = Input.GetKeyDown(jumpKey);
            //然后检测是否切换速度
            if (Input.GetKeyDown(speedSwitch))
            {
                running = !running;
            }
            //真正的行进速度在跑步和走路速度之间来回切换，之中要算上动画的因素
            moveSpeed = (running ? runSpeed : walkSpeed)/animationSynchrolizer;
            //所有的输入都处理好了，调用当前的state的handleInput函数，会根据state的类型去处理所有的输入输出，并做各种状态切换
            state.handleInput();


        }
        private void OnDisable()
        {
            //人物控制会被切换成鸟类控制，但是这时候也不能直接撒手，会卡在那
            //所以必须要按照一定的默认输入处理一段时间。
            if (Application.isPlaying) StartCoroutine("Disable");

        }
        IEnumerator Disable()
        {
           
            while (enabled == false)
            {
                yield return 0;
                //默认输入的更改
                if (state.GetType() == typeof(GroundedState))
                {
                    xInput = yInput = 0;
                }
                //继续handleInput一段时间
                state.handleInput();
            }
        }
    }

}