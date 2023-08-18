using UnityEngine;

namespace Rimaethon.Scripts.Utility
{
    public class LevelSwitcher : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            LevelManager.LoadScene(sceneName);
        }
    }
}