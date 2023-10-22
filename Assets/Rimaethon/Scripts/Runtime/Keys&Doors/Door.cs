using UnityEngine;

namespace Rimaethon.Runtime.Keys_Doors
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private int doorID;
        private readonly int _closeHash = Animator.StringToHash("close");
        private readonly int _openHash = Animator.StringToHash("open");
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private bool _isOpen;


        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }


        private void OnDisable()
        {
            PlayerKeyHolder.ClearKeyRing();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player")) AttemptToOpen();
        }

        private void AttemptToOpen()
        {
            if (!CheckPlayerHasKey() || _isOpen) return;
            Open();
            //AudioManager.Instance.PlaySFX(3);
        }


        private bool CheckPlayerHasKey()
        {
            return PlayerKeyHolder.HasKey(doorID);
        }


        private void Open()
        {
            _isOpen = true;
            _animator.SetTrigger(_openHash);
            //AudioManager.Instance.PlaySFX(2);
        }

        public void DisableCollider()
        {
            _boxCollider.enabled = false;
        }

        private void Close()
        {
            _isOpen = false;
            _animator.SetTrigger(_closeHash);
            _boxCollider.enabled = true;
            // AudioManager.Instance.PlaySFX(2);
        }
    }
}