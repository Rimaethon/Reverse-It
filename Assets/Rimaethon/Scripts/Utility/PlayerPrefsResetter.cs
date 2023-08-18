using UnityEngine;

namespace Rimaethon.Scripts.Utility
{
    public class PlayerPrefsResetter : MonoBehaviour
    {
        public void ResetGamePlayerPrefs()
        {
            GameManager.ResetGamePlayerPrefs();
        }
    }
}