using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void gotoOriginalScene()
    {
        SceneManager.LoadScene(0);
    }

    public void gotoChangedScene()
    {
        SceneManager.LoadScene(1);
    }
}
