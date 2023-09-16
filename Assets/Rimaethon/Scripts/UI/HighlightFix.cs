using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rimaethon.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class HighlightFix : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static HighlightFix lastHovered;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (lastHovered != null && lastHovered != this)
            {
                lastHovered.ResetButtonState();
            }
            lastHovered = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ResetButtonState();
        }

        public void ResetButtonState()
        {
            if(_button != null)
            {
                // Here you can reset your button state, perhaps by manually invoking the sprite swap, if necessary
            }
        }

        public static void ResetLastHovered()
        {
            if (lastHovered != null)
            {
                lastHovered.ResetButtonState();
                lastHovered = null;
            }
        }
    }
}