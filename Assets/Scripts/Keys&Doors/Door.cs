using UnityEngine;
using UnityEngine.Events;

namespace Keys_Doors
{
    
    [RequireComponent(typeof(Collider2D))]
    public class Door : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The ID of the door used to determine which keys unlock it.\n" +
                 "A door ID of 0 is unlocked by default.")]
        public int doorID;
        [Tooltip("Whether this door is open right now.")]
        public bool isOpen;
        [Tooltip("Events to call when opening the door.")]
        public UnityEvent openEvent = new UnityEvent();
        [Tooltip("Events to call when closing the door.")]
        public UnityEvent closeEvent = new UnityEvent();
        [Tooltip("The effect to play when the door opens or closes")]
        public GameObject doorOpenAndCloseEffect;
        [Tooltip("The effect to play when the door is attempted to open but can not")]
        public GameObject doorLockedEffect;

      
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                AttemptToOpen();
            }
        }


       
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AttemptToOpen();
            }
        }


        private void AttemptToOpen()
        {
            if (CheckPlayerHasKey() && !isOpen)
            {
                Open();
            }
            else if (doorLockedEffect && !isOpen)
            {
                Instantiate(doorLockedEffect, transform.position, Quaternion.identity, null);
            }
        }


        private bool CheckPlayerHasKey()
        {
            return KeyRing.HasKey(this);
        }

 
        protected virtual void Open()
        {
            isOpen = true;
            openEvent.Invoke();
            if (doorOpenAndCloseEffect)
            {
                Instantiate(doorOpenAndCloseEffect, transform.position, Quaternion.identity, null);
            }
        }

        protected virtual void Close()
        {
            isOpen = false;
            closeEvent.Invoke();
            if (doorOpenAndCloseEffect)
            {
                Instantiate(doorOpenAndCloseEffect, transform.position, Quaternion.identity, null);
            }
        }
    }
}
