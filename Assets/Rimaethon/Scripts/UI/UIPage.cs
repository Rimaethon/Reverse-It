using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.UI
{
    public class UIPage : MonoBehaviour
    {
        [Tooltip("The default UI to have selected when opening this page")]
        public GameObject defaultSelected;


        public void SetSelectedUIToDefault()
        {
            if (GameManager.Instance == null || GameManager.Instance.uiManager == null ||
                defaultSelected == null) return;
            GameManager.Instance.uiManager.eventSystem.SetSelectedGameObject(null);
            GameManager.Instance.uiManager.eventSystem.SetSelectedGameObject(defaultSelected);
        }
    }
}