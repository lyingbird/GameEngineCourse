# GameEngineCourse

游戏在IMDT/Build目录中，

包括布料模拟与物理碰撞两个场景，通过按键切换。

布料模拟分别采用了Spring Bone与Cloth, 完成了两套UnityChan头发的模拟。总体来看Spring Bone的模拟效果更好，因为Cloth系统中需要为头发上的诸多顶点指定各项参数，很难调整。而Spring Bone只需要通过指定Collider的size并添加到Manager中即可有不错的效果。

物理碰撞采用老师给的插件，实现了一个开车沿途搞破坏的小游戏。场景中多个物体可破坏，有的采用BSP划分，有的采用Voronoi 划分。以一定速度撞上物体即可破坏。

