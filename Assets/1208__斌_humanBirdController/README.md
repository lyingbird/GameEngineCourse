# 12月8日人鸟控制器更新版本

根目录下有一个Player的prefab，直接拖到场景里就可以了
1. 要让Player能够跟随某个平台移动，必须把平台的物体的layer设置成“Ground”。也可以叫别的名字，那就要去Player的HumanControll物体下，找到GroundedState组件修改目标Layer名称。
2. `ProjectSettings->Physics->AutoSyncTransform`一定要打开，才能支持人物跟随平台移动
3. 人物使用WASD操控，按空格可以跳跃，跳跃的键位可以修改，在Player的HumanControll物体下，找到JumpState即可。鸟也使用WASD操控，从人切换到鸟按B键，反之按H键。
4. 人物操控和鸟的操控的相机视角估计还会改，大部分的玩家同学都觉得应该按照相机前进的方向来操控。
5. 鸟后续会加上子弹时间冲刺效果。