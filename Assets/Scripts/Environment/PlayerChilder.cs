using UnityEngine;

namespace Environment
{
    public class PlayerChilder : MonoBehaviour
    {
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            MakeAChildOfAttachedTransform(collision);
        }

       
        private void OnTriggerStay2D(Collider2D collision)
        {
            MakeAChildOfAttachedTransform(collision);
        }

       
        private void OnTriggerExit2D(Collider2D collision)
        {
            DeChild(collision);
        }

       
        private static void DeChild(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(null);
            }
        }


      
        private void MakeAChildOfAttachedTransform(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.gameObject.transform.SetParent(transform);
            } 
        }
    }
}
