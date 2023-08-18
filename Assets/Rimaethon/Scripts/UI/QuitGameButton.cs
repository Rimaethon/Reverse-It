using UnityEditor;
using UnityEngine;

namespace Rimaethon.Scripts.UI
{
    public class QuitGameButton : MonoBehaviour
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}