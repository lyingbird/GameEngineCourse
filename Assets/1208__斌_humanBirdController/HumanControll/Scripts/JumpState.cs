using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    [RequireComponent(typeof(SpeedCurveGenerator))]
    [RequireComponent(typeof(GroundedState))]
    public class JumpState : StateBase
    {
        /// <summary>
        /// 跳跃顶点高度，人物会按照这个设定的高度跳跃
        /// </summary>
        public float jumpHeight = 1;
        /// <summary>
        /// 跳跃开始后经过的时间
        /// </summary>
        float jumpTimeline = 0;
        /// <summary>
        /// y方向上的速度，用来施加重力
        /// </summary>
        float yspeed = 0;
        /// <summary>
        /// 跳跃状态下落的时候的重力倍数
        /// </summary>
        [Range(0.1f,10f)]
        [Tooltip("跳跃状态下落的时候的重力倍数")]
        public float gravityFactor = 1;
        bool isGrounded = false;
        bool hitGround = false;
        bool closeGround = false;
        public Vector3 hitpointMinusTransform;
        /// <summary>
        /// 获取一下用来生成曲线的函数
        /// </summary>
        SpeedCurveGenerator scurve;
        //单例模式应用
        private void Awake()
        {
            if (jumpState == null)
            {
                jumpState = this;
            }
            else
            {
                Destroy(this);
            }
        }
        public override void handleInput()
        {
            base.handleInput();

            RaycastHit hit;
            hitGround = Physics.Raycast(
                new Ray(transform.position + transform.up, -transform.up),//从人物的中间开始往下打，避免从脚底开始打会出现的一开始射线就在地面以下的问题
                out hit,
                LayerMask.NameToLayer("Ground")
                );
            hitpointMinusTransform = transform.position- hit.point;
            closeGround = hitpointMinusTransform.y < 0.2f;
            isGrounded = hitGround && closeGround;
                //起跳之后过了一段时间，如果发现落地了，那就转到地面状态
                if (isGrounded && jumpTimeline>=scurve.t2)
            {
                turnToState(groundedState);
            }

            //用humancontroll的输入xy来转换到实际世界坐标下的前进方向
            //方向与相机挂钩，且在Unity中yz互换了，因此这里要注意
            var x = humanControll.xInput * camTransform.right;
            var z = humanControll.yInput * camTransform.forward;

            //跳起状态时，更新时间，获取当前冲刺速度
            if (jumpTimeline <= scurve.t2)
            {
                jumpTimeline += Time.deltaTime;
                yspeed = scurve.caculateVelocity(jumpTimeline);
            }
            else//下落时应用重力
            {
                yspeed -= Physics.gravity.magnitude* gravityFactor * Time.deltaTime;
            }
            var y = yspeed * Vector3.up;

            //平面上的向量要被标准化
            Vector3 vec3_Move = x + z;
            vec3_Move.y = 0;

            //转动人物的朝向
            if (vec3_Move.magnitude >= 0.01) transform.forward = vec3_Move.normalized;
            vec3_Move = vec3_Move.normalized * humanControll.moveSpeed;

            //把y的运动加上
            vec3_Move += y;
            GetComponent<CharacterController>().Move(vec3_Move * Time.deltaTime);

        }
        public override void enter()
        {
            base.enter();
            //把曲线的积分大小改成跳跃高度
            scurve = GetComponent<SpeedCurveGenerator>();
            scurve.D = jumpHeight;
            //初始化时间线
            jumpTimeline = 0;

            //初始化状态机
            var animator = GetComponent<Animator>();
            if (animator)
            {
                animator.SetTrigger("Jump");
            }
        }
        public override void exit()
        {
            base.exit();
        }
        public override void animate()
        {
            base.animate();

            //直接同步动画速度
            var animator = GetComponent<Animator>();
            if (animator)
            {
                animator.SetFloat("JumpVelocity", yspeed);
            }
        }
    }
}
