using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
 
    public static class LevelManager
    {

        public static void LoadScene(string sceneName)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneName);
        }
    }
}
