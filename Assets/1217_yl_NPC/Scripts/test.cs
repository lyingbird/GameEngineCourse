using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    

    private void Start()
    {
        this.transform.DOScale(0, 2).From().SetEase(Ease.OutBack);

    }
}
