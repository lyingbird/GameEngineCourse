using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public int SceneNumber;


        private AsyncOperation asyncOperation;

        public void LoadTargetScene() 
        {
            Debug.Log("clicked button!");

            SceneManager.LoadScene(SceneNumber);
        }

        public void Update()
        {
        }
    }
}
