using UnityEngine;

namespace Utility
{

    public class LevelSwitcher : MonoBehaviour
    {
   
        public void LoadScene(string sceneName)
        {
            LevelManager.LoadScene(sceneName);
        }
    }
}
