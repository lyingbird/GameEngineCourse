using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
namespace YB
{
    public class GroundedState : DynamicParentingState
    {
        /// <summary>
        ///  确实是m/s的速度,亲测
        /// </summary>
        Vector3 vec3_Move;
        [HideInInspector]
        public float yspeed = 0;
        private bool insideLaunchStar = false;
        //在awake函数里面实现单例模式
        private void Awake()
        {
            //如果目前这个静态变量是空的，那就用这个赋值
            if (groundedState == null)
            {
                groundedState = this;
            }
            //如果已经被别人赋值过了，那就把当前这个实例删掉
            else
            {
                Destroy(this);
            }
        }
        public override void handleInput()
        {
            base.handleInput();
            insideLaunchStar = flyState.InsideLaunchStar;

            //判断是否起跳，如果起跳，那么就转到跳跃状态jumpState

            var jumpKey = humanControll.jumpKey;

            if (!talkingState.ui.inDialogue && talkingState.currentNPC != null && humanControll.getJumpKey)
            {
                turnToState(talkingState);
            }

            else if (!insideLaunchStar && humanControll.getJumpKey)
            {
                print("entered jumpstate");
                turnToState(jumpState);
            }
            else if (insideLaunchStar && humanControll.getJumpKey)
            {
                turnToState(flyState);
            }




            //用humancontroll的输入xy来转换到实际世界坐标下的前进方向
            //方向与相机挂钩，且在Unity中yz互换了，因此这里要注意
            var x = humanControll.xInput * camTransform.right;
            var z = humanControll.yInput * camTransform.forward;
            //直接获取humanControll处理好的速度
            float moveSpeed = humanControll.moveSpeed;

            //加上x和z的影响，消掉y轴的
            vec3_Move = x + z;
            vec3_Move.y = 0;
            //开始考虑y轴的影响
            //如果接触地面，就不要考虑重力
            if (GetComponent<CharacterController>().isGrounded)
            {
                yspeed = 0;
            }
            //否则就让yspeed从0开始减，保证踩空的时候会往下掉
            else
            {
                //重力加到速度上也要考虑time.deltatime
                yspeed -= Physics.gravity.magnitude * Time.deltaTime;
            }

            //平面上的向量要被标准化
            vec3_Move = vec3_Move.normalized;
            //转动人物的朝向
            if (vec3_Move.magnitude >= 0.01) transform.forward = vec3_Move;

            //把y的运动加上
            vec3_Move += yspeed * Vector3.up;
            GetComponent<CharacterController>().Move(vec3_Move * moveSpeed * humanControll.animationSynchrolizer * Time.deltaTime);
        }
        //下面所有的函数都在考虑动画
        public override void enter()
        {
            Debug.Log("entered groundedState");

            base.enter();
            var animator = GetComponent<Animator>();
            //状态机是一个任意状态都可以转的机子
            //直接用trigger打开，而且只有进入groundState的时候才会切换一次
            //如果没有animator，直接放弃动画，防止报错
            if (animator)
            {
                animator.SetTrigger("Grounded");
            }
        }
        public override void exit()
        {
            Debug.Log("exited groundedState");
            base.exit();

        }
        public override void animate()
        {
            base.animate();
            var animator = GetComponent<Animator>();
            //如果没有animator，直接放弃动画，防止报错
            if (animator)
            {
                //给动画同步速度的时候不能考虑Y轴的速度
                var _vec3_Move = vec3_Move;
                _vec3_Move.y = 0;
                animator.SetFloat("Velocity",
                    Mathf.Lerp(
                        animator.GetFloat("Velocity"),
                        _vec3_Move.magnitude * humanControll.moveSpeed,
                        Time.deltaTime * humanControll.animationMergeSpeed
                        )
                    );
            }
        }

    }
}
