using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YB
{
    public class DynamicParentingState :StateBase
    {
        /// <summary>
        /// 初始状态时的父物体
        /// </summary>
        private Transform originalParent;
        /// <summary>
        /// 上一个父物体
        /// </summary>
        private Transform lastParent;
        /// <summary>
        /// 中间要放一个空物体来处理父子关系问题
        /// </summary>
        private GameObject subParent;
        /// <summary>
        /// 顾名思义，地面layer的名字
        /// </summary>
        [Tooltip("角色只会认定被标记为名为这个字符串的Layer的物体")]
        public string groundLayerName = "Ground";
        private void Start()
        {
            //初始化，用一开始的父物体给两个变量赋值
            originalParent = transform.parent;
            lastParent = originalParent;
        }
        public override void handleInput()
        {
            base.handleInput();

            //开始射线检测
            RaycastHit hit;
            if (Physics.Raycast(
                new Ray(transform.position + transform.up, -transform.up),//从人物的中间开始往下打，避免从脚底开始打会出现的一开始射线就在地面以下的问题
                out hit,1.2f,
                LayerMask.GetMask(groundLayerName)//我日，这里一定要用getmask
                ))
            {
                Debug.Log(hit.transform.gameObject);
                //如果碰到的物体在射线出发后1.5米内，且碰撞物体和目前的父物体不一样
                //这说明有一个新的物体出现在我们脚下
                //那就要更换父物体
                if (hit.transform != lastParent/* && (hit.point - transform.position + transform.up).magnitude < 1.2f*/)
                {
                    //刷新lastParent的记录，不过这样的代码好像不是很符合异常安全，不过anyway，下面的代码也不会有异常了
                    lastParent = hit.transform;
                    Debug.Log("*************** Parent Switching *****************");
                    //新建一个空物体，空物体的位置和旋转没有变换，但是缩放做一个反变换

                    transform.parent = originalParent;//先解绑再把父物体删掉啊
                    Destroy(subParent);
                    //一个新的物体
                    subParent = new GameObject();
                    subParent.transform.parent = hit.transform;
                    //空物体已经绑到父物体上了，这时候重新置换他的位置和旋转
                    subParent.transform.localPosition = Vector3.zero;
                    subParent.transform.localRotation = Quaternion.identity;
                    //仅对父物体的scale做反变换
                    Vector3 parentLossyScale = hit.transform.lossyScale;
                    subParent.transform.localScale = new Vector3(1 / parentLossyScale.x, 1 / parentLossyScale.y, 1 / parentLossyScale.z);
                    //现在就可以放心把人物挂到这个空物体下
                    transform.parent = subParent.transform;
                }
            }
            else
            {
                //Debug.Log("+++++++++++++++++++++++++++++++++++++++++");
                //在该状态下，脚底没人，那就还原父物体和lastParent变量的状态
                transform.parent = originalParent;
                lastParent = originalParent;
            }
        }
        public override void enter()
        {
            base.enter();
           
        }
        public override void exit()
        {
            base.exit();
            //离开该状态时还原父物体和lastParent变量的状态
            transform.parent = originalParent;
            lastParent = originalParent;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position + transform.up, transform.position - transform.up*0.2f);
        }
        private void OnDestroy()
        {
        }
    }

}