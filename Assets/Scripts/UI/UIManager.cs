using System.Collections.Generic;
using System.Linq;
using Player;
using UI.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace UI
{
    public class UIManager : MonoBehaviour
    {

        [Header("Page Management")]
        [Tooltip("The pages (Panels) managed by the UI Manager")]
        public List<UIPage> pages;
        [Tooltip("The index of the active page in the UI")]
        public int currentPage;
        [Tooltip("The page (by index) switched to when the UI Manager starts up")]
        public int defaultPage;

        [Header("Pause Settings")]
        [Tooltip("The index of the pause page in the pages list")]
        public int pausePageIndex = 1;
        [Tooltip("Whether or not to allow pausing")]
        public bool allowPause = true;
        [Header("Polish Effects")]
        [Tooltip("The effect to create when navigating between UI")]
        public GameObject navigationEffect;
        [Tooltip("The effect to create when clicking on or pressing a UI element")]
        public GameObject clickEffect;
        [Tooltip("The effect to create when the player is backing out of a Menu page")]
        public GameObject backEffect;

        private bool _isPaused;

        private List<UIElement> _uIelements;

        [HideInInspector]
        public EventSystem eventSystem;
        [SerializeField]
        private InputManager inputManager;

  
        public void CreateBackEffect()
        {
            if (backEffect)
            {
                Instantiate(backEffect, transform.position, Quaternion.identity, null);
            }
        }

   
        public void CreateClickEffect()
        {
            if (clickEffect)
            {
                Instantiate(clickEffect, transform.position, Quaternion.identity, null);
            }
        }

     
        public void CreateNavigationEffect()
        {
            if (navigationEffect)
            {
                Instantiate(navigationEffect, transform.position, Quaternion.identity, null);
            }
        }

      
        private void OnEnable()
        {
            SetupGameManagerUIManager();
        }

  
        private void SetupGameManagerUIManager()
        {
            if (GameManager.Instance != null && GameManager.Instance.uiManager == null)
            {
                GameManager.Instance.uiManager = this;
            }     
        }

   
        private void SetUpUIElements()
        {
            _uIelements = FindObjectsOfType<UIElement>().ToList();
        }

     
        private void SetUpEventSystem()
        {
            eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                Debug.LogWarning("There is no event system in the scene but you are trying to use the UIManager. /n" +
                                 "All UI in Unity requires an Event System to run. /n" + 
                                 "You can add one by right clicking in hierarchy then selecting UI->EventSystem.");
            }
        }

      
        private void SetUpInputManager()
        {
            if (inputManager == null)
            {
                inputManager = InputManager.Instance;
            }
            if (inputManager == null)
            {
                Debug.LogWarning("The UIManager is missing a reference to an Input Manager, without a Input Manager the UI can not pause");
            }
        }

    
        public void TogglePause()
        {
            if (!allowPause) return;
            if (_isPaused)
            {
                GoToPage(defaultPage);
                Time.timeScale = 1;
                _isPaused = false;
            }
            else
            {
                GoToPage(pausePageIndex);
                Time.timeScale = 0;
                _isPaused = true;
            }
        }

    
        public void UpdateUI()
        {
            foreach(UIElement uiElement in _uIelements)
            {
                uiElement.UpdateUI();
            }
        }

   
        private void Start()
        {
            SetUpInputManager();
            SetUpEventSystem();
            SetUpUIElements();
            InitializeFirstPage();
            UpdateUI();
        }

    
        private void InitializeFirstPage()
        {
            GoToPage(defaultPage);
        }

        private void Update()
        {
            CheckPauseInput();
        }

   
        private void CheckPauseInput()
        {
            if (inputManager == null) return;
            if (inputManager.pauseButton != 1) return;
            TogglePause();
            inputManager.pauseButton = 0;
        }
       
        public void GoToPage(int pageIndex)
        {
            if (pageIndex < pages.Count && pages[pageIndex] != null)
            {
                SetActiveAllPages(false);
                pages[pageIndex].gameObject.SetActive(true);
                pages[pageIndex].SetSelectedUIToDefault();
            }
        }

        
        public void GoToPageByName(string pageName)
        {
            UIPage page = pages.Find(item => item.name == pageName);
            int pageIndex = pages.IndexOf(page);
            GoToPage(pageIndex);
        }


        private void SetActiveAllPages(bool activated)
        {
            if (pages == null) return;
            foreach (var page in pages.Where(page => page != null))
            {
                page.gameObject.SetActive(activated);
            }
        }
    }
}
