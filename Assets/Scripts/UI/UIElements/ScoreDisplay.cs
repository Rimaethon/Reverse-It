using UnityEngine.UI;
using Utility;

namespace UI.UIElements
{
    public class ScoreDisplay : UIElement
    {
        public Text displayText;


        private void DisplayScore()
        {
            if (displayText != null) displayText.text = "Score: " + GameManager.Score;
        }


        public override void UpdateUI()
        {
            base.UpdateUI();

            DisplayScore();
        }
    }
}