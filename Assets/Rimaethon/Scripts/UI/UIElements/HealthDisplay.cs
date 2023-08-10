using Health_Damage;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.UIElements
{
    public class HealthDisplay : UIElement
    {
        [Header("Settings")] [Tooltip("The image which represents one unit of health")]
        public GameObject healthDisplayImage;

        [Tooltip("The prefab to use to display the number")]
        public GameObject numberDisplay;

        [Tooltip("The maximum number of images to display before switching to just a number")]
        public int maximumNumberToDisplay = 3;


        public override void UpdateUI()
        {
            if (GameManager.Instance != null && GameManager.Instance.player != null)
            {
                var playerHealth = GameManager.Instance.player.GetComponent<Health>();
                if (playerHealth != null) SetChildImageNumber(playerHealth.currentHealth);
            }
        }

        /// <summary>
        ///     Description:
        ///     Deletes and spawns images until this gameobject has as many children as the player has health
        ///     Input:
        ///     int
        ///     Return:
        ///     void (no return)
        /// </summary>
        /// <param name="number">The number of images that this object should have as children</param>
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