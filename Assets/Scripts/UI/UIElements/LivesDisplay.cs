using Health_Damage;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.UIElements
{
    public class LivesDisplay : UIElement
    {
        public GameObject livesDisplayImage;

        [Tooltip("The prefab to use to display the number")]
        public GameObject numberDisplay;
        public int maximumNumberToDisplay = 3;


        public override void UpdateUI()
        {
            if (GameManager.Instance != null && GameManager.Instance.player != null)
            {
                var playerHealth = GameManager.Instance.player.GetComponent<Health>();
                if (playerHealth != null) SetChildImageNumber(playerHealth.currentLives - 1);
            }
        }


        private void SetChildImageNumber(int number)
        {
            for (var i = transform.childCount - 1; i >= 0; i--) Destroy(transform.GetChild(i).gameObject);

            if (livesDisplayImage != null)
            {
                if (maximumNumberToDisplay >= number)
                {
                    for (var i = 0; i < number; i++) Instantiate(livesDisplayImage, transform);
                }
                else
                {
                    Instantiate(livesDisplayImage, transform);
                    var createdNumberDisp = Instantiate(numberDisplay, transform);
                    createdNumberDisp.GetComponent<Text>().text = number.ToString();
                }
            }
        }
    }
}