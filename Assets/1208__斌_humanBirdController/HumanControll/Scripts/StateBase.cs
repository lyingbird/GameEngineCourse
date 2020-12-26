using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    //
    // 摘要:
    //     所有人物状态的基类，包含handleInput，Enter，Exit等virtual函数
    [RequireComponent(typeof(HumanControll))]
    public class StateBase : MonoBehaviour
    {
        //首先要包含所有的子类的单例，不过这是在只有一个主角的情况下了，如果不是的话，这样的写法就够呛
        //这几个不用自己复制，子类的实例会自己来处理单例模式的赋值问题
        static public GroundedState groundedState;
        static public JumpState jumpState;
        static public YL.FlyState flyState;
        static public YL.TalkingState talkingState;


        //另外包含一下人类控制脚本用来获得输入，还有相机位置用来处理输入
        static public HumanControll humanControll;
        static public Transform camTransform;

        /// <summary>
        /// 一个状态处理输入的方式
        /// </summary>
        public virtual void handleInput()
        {
            //基类先获取一下人类控制脚本和相机位置
            humanControll = GetComponent<HumanControll>();
            camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
            animate();
        }
        /// <summary>
        /// 用于初始化一个状态
        /// </summary>
        public virtual void enter() {
            //Debug.Log("Entering " + stateName());
        }
        /// <summary>
        /// 离开一个状态的时候应有的操作
        /// </summary>
        public virtual void exit()
        {
            //Debug.Log("Exitting " + stateName());
        }
        /// <summary>
        /// 某个状态的类型名字
        /// </summary>
        /// <returns></returns>
        public string stateName() {
            return GetType().ToString();
        }
        /// <summary>
        /// 如果一个状态有动画同步需求，就在这里处理
        /// </summary>
        public virtual void animate() { }

        /// <summary>
        /// 状态切换时调用该函数，首先调用当前状态的Exit函数析构，然后切换成目标状态，调用目标状态的Enter函数完成初始化
        /// </summary>
        public void turnToState(StateBase nextState)
        {
            print("Im really into this function");
            print(humanControll && nextState);
            if (humanControll && nextState)
            {
                print("get here");
                humanControll.state.exit();
                print(humanControll.state);
                humanControll.state = nextState;
                print(humanControll.state);
                humanControll.state.enter();
            }
        }

    }
}
