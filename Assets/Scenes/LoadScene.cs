using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes{
    public class LoadScene : MonoBehaviour
    {
        public int sceneIndex;

        public void Load(){
            SceneManager.LoadScene(sceneIndex);
        }
    }
}