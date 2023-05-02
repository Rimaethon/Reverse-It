using UnityEngine;

namespace UI
{
    public class CursorChanger : MonoBehaviour
    {
        [Header("Settings:")]
        [Tooltip("The cursor to change to")]
        public Texture2D newCursorSprite;


        public void Start()
        {
            ChangeCursor();
        }


        private void ChangeCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;

            Vector2 hotSpot = new Vector2();
            if (newCursorSprite == null) return;
            hotSpot.x = newCursorSprite.width / 2;
            hotSpot.y = newCursorSprite.height / 2;

            Cursor.SetCursor(newCursorSprite, hotSpot, CursorMode.Auto);
        }
    }
}
