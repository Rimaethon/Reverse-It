using UnityEngine;

namespace Rimaethon.Runtime.Environment
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;

        private Renderer _renderer;
        private Vector2 _savedOffset;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }


        private void FixedUpdate()
        {
            var x = Mathf.Repeat(Time.time * scrollSpeed, 1);
            _savedOffset = new Vector2(x, 0);
            _renderer.sharedMaterial.SetTextureOffset("_MainTex", _savedOffset);
        }
    }
}