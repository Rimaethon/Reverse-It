using UnityEngine;

namespace Utility
{
  
    public class PlayerPrefsResetter : MonoBehaviour
    {

   
        public void ResetGamePlayerPrefs()
        {
            GameManager.ResetGamePlayerPrefs();
        }
    }
}
