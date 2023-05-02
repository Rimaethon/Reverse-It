using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Utility
{
   
    public class ScreenshotUtility : MonoBehaviour
    {
        private static ScreenshotUtility _screenShotUtility;

        #region Public Variables
        [Header("Settings")]
        [Tooltip("Should the screenshot utility run only in the editor.")]
        public bool runOnlyInEditor = true;
        [FormerlySerializedAs("m_ScreenshotKey")] [Tooltip("What key is mapped to take the screenshot.")]
        public string mScreenshotKey = "c";
        [FormerlySerializedAs("m_ScaleFactor")] [Tooltip("What is the scale factor for the screenshot. Standard is 1, 2x size is 2, etc..")]
        public int mScaleFactor = 1;
        [Tooltip("Include image size in filename.")]
        public bool includeImageSizeInFilename = true;
        #endregion

        [FormerlySerializedAs("m_ImageCount")]
        [Header("Private Variables")]
        #region Private Variables
        // The number of screenshots taken
        [Tooltip("Use the Reset Counter contextual menu item to reset this.")]
        [SerializeField]
        private int mImageCount;
        #endregion

        #region Constants
        private const string ImageCntKey = "IMAGE_CNT";
        #endregion


        private void Awake()
        {
            if (_screenShotUtility != null)
            { 
                Destroy(gameObject);
            }
            else if (runOnlyInEditor && !Application.isEditor)
            { 
                Destroy(gameObject);
            }
            else
            {
                _screenShotUtility = this.GetComponent<ScreenshotUtility>();

                DontDestroyOnLoad(gameObject);

                mImageCount = PlayerPrefs.GetInt(ImageCntKey);

                if (!Directory.Exists("Screenshots"))
                {
                    Directory.CreateDirectory("Screenshots");
                }
            }
        }


        private void Update()
        {

            if (Keyboard.current.FindKeyOnCurrentKeyboardLayout(mScreenshotKey).wasPressedThisFrame)
            {
                TakeScreenshot();
            }
        }

     
        [ContextMenu("Reset Counter")]
        public void ResetCounter()
        {
            mImageCount = 0;
            PlayerPrefs.SetInt(ImageCntKey, mImageCount);
        }


        private void TakeScreenshot()
        {
            PlayerPrefs.SetInt(ImageCntKey, ++mImageCount);

            int width = Screen.width * mScaleFactor;
            int height = Screen.height * mScaleFactor;

            string pathname = "Screenshots/Screenshot_";
            if (includeImageSizeInFilename)
            {
                pathname += width + "x" + height + "_";
            }
            pathname += mImageCount + ".png";

            ScreenCapture.CaptureScreenshot(pathname, mScaleFactor);
            Debug.Log("Screenshot captured at " + pathname);
        }
    }
}
