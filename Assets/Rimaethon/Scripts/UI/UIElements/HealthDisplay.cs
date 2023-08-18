using Rimaethon.Scripts.Health_Damage;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon.Scripts.UI.UIElements
{
    public class HealthDisplay : UIElement
    {
        public GameObject healthDisplayImage;

        public GameObject numberDisplay;

        public int maximumNumberToDisplay = 3;


        public override void UpdateUI()
        {
            if (GameManager.Instance != null && GameManager.Instance.player != null)
            {
                var playerHealth = GameManager.Instance.player.GetComponent<BaseHealth>();
            }
        }

 
        private void SetChildImageNumber(int number)
        {
            for (var i = transform.childCount - 1; i >= 0; i--) Destroy(transform.GetChild(i).gameObject);

            if (healthDisplayImage != null)
            {
                if (maximumNumberToDisplay >= number)
                {
                    for (var i = 0; i < number; i++) Instantiate(healthDisplayImage, transform);
                }
                else
                {
                    Instantiate(healthDisplayImage, transform);
                    var createdNumberDisp = Instantiate(numberDisplay, transform);
                    createdNumberDisp.GetComponent<Text>().text = number.ToString();
                }
            }
        }
    }
}