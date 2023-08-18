using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rimaethon.Scripts.UI
{
    public class LevelLoadButton : MonoBehaviour
    {
        public void LoadLevelByName(string levelToLoadName)
        {
            SceneManager.LoadScene(levelToLoadName);
        }
    }
}