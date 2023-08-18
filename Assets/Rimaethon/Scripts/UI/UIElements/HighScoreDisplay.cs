using Rimaethon.Scripts.Utility;
using UnityEngine.UI;

namespace Rimaethon.Scripts.UI.UIElements
{

    public class HighScoreDisplay : UIElement
    {
        public Text displayText;

 
        public void DisplayHighScore()
        {
            if (displayText != null) displayText.text = "High: " + GameManager.Instance.highScore;
        }

     
        public override void UpdateUI()
        {
            base.UpdateUI();

            DisplayHighScore();
        }
    }
}