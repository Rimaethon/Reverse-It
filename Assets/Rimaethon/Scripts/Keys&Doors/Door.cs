using UnityEngine;
using UnityEngine.Events;

namespace Keys_Doors
{
    [RequireComponent(typeof(Collider2D))]
    public class Door : MonoBehaviour
    {
     
        public int doorID;

        public bool isOpen;

        public UnityEvent openEvent = new();

        public UnityEvent closeEvent = new();

        public GameObject doorOpenAndCloseEffect;

        public GameObject doorLockedEffect;


        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player")) AttemptToOpen();
        }


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) AttemptToOpen();
        }


        private void AttemptToOpen()
        {
            if (CheckPlayerHasKey() && !isOpen)
                Open();
            else if (doorLockedEffect && !isOpen)
                Instantiate(doorLockedEffect, transform.position, Quaternion.identity, null);
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
                Instantiate(doorOpenAndCloseEffect, transform.position, Quaternion.identity, null);
        }

        protected virtual void Close()
        {
            isOpen = false;
            closeEvent.Invoke();
            if (doorOpenAndCloseEffect)
                Instantiate(doorOpenAndCloseEffect, transform.position, Quaternion.identity, null);
        }
    }
}