using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToDestroyScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToUnityChanScene()
    {
        SceneManager.LoadScene(1);
    }
}
