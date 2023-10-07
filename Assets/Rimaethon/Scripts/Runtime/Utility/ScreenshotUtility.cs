using System.IO;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.Utility
{
    public class ScreenshotUtility : MonoBehaviour
    {
        #region Constants

        private const string ImageCountKey = "IMAGE_CNT";

        #endregion

        #region Private Variables
        private int _imageCount;
        [SerializeField] private bool runOnlyInEditor = true;
        [SerializeField] private int scaleFactor = 1;
        [SerializeField] private bool includeImageSizeInFilename = true;
        #endregion

        private void Awake()
        {
            if (runOnlyInEditor && !Application.isEditor)
            {
                Destroy(gameObject);
            }
            else
            {
                _imageCount = PlayerPrefs.GetInt(ImageCountKey);
                if (!Directory.Exists("Screenshots")) Directory.CreateDirectory("Screenshots");
            }
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnScreenshot, TakeScreenshot);
        }

        private void OnDestroy()
        {
            if (EventManager.Instance == null) return;

            EventManager.Instance.RemoveHandler(GameEvents.OnScreenshot, TakeScreenshot);
        }


        [ContextMenu("Reset Counter")]
        public void ResetCounter()
        {
            _imageCount = 0;
            PlayerPrefs.SetInt(ImageCountKey, _imageCount);
        }


        private void TakeScreenshot()
        {
            PlayerPrefs.SetInt(ImageCountKey, ++_imageCount);

            var width = Screen.width * scaleFactor;
            var height = Screen.height * scaleFactor;

            var pathname = "Screenshots/Screenshot_";
            if (includeImageSizeInFilename) pathname += width + "x" + height + "_";
            pathname += _imageCount + ".png";

            ScreenCapture.CaptureScreenshot(pathname, scaleFactor);
            Debug.Log("Screenshot captured at " + pathname);
        }

   
    }
}