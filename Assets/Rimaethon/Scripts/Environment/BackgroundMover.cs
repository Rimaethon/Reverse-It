using System;
using System.Collections;
using UnityEngine;

namespace Rimaethon.Scripts.Environment
{
    public class BackgroundMover : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private Material _background;
        Vector2 _offset = new Vector2(0, 0);
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _background = _meshRenderer.material;
            _offset = _background.mainTextureOffset;
            StartCoroutine(MoveBackground());
        }
        
        private IEnumerator MoveBackground()
        {
            while (true)
            {
                _offset.x += 0.0005f;
                _background.mainTextureOffset = _offset;
                yield return new WaitForSeconds(0.02f);
            }
           
        }
    }
}
