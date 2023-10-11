using UnityEngine;

namespace Rimaethon.Scripts.Environment
{
    public class PlatformChildObjectHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.position.y > transform.position.y)
            {
                AddChildObject(collision);
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            RemoveChildObject(collision);
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            AddChildObject(collision);
        }


        private static void RemoveChildObject(Collider2D collision)
        {
            if (collision.CompareTag("Player")) collision.gameObject.transform.SetParent(null);
        }


        private void AddChildObject(Collider2D collision)
        {
            if (collision.CompareTag("Player")) collision.gameObject.transform.SetParent(transform);
        }
    }
}