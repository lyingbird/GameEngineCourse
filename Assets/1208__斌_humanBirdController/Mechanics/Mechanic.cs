using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanic : MonoBehaviour
{
    //TODO 这是个父类，被其他机制继承。

    private bool _istriggered=false;
    private bool isTriggered
    {
        get { return _istriggered; }
        set
        {
            if (_istriggered == false && value == true)
            {
                _istriggered = value;
                act = true;
            }
        }
    }
    bool act = false;
    public virtual void triggeredAction()
    {
        Debug.Log("do some triggered Action:" + name);
    }
    public void trigger() {
        isTriggered = true;
    }

    private void Update()
    {
        if (act)
        {
            triggeredAction();
            act = false;
        }
    }
}
