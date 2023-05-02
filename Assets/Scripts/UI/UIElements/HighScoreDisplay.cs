using System.Collections;
using System.Collections.Generic;
using UI.UIElements;
using UnityEngine;
using UnityEngine.UI;
using Utility;

/// <summary>
/// Handles updating the high score display
/// </summary>
public class HighScoreDisplay : UIElement
{
    [Header("References")]
    [Tooltip("The text that displays the high score")]
    public Text displayText = null;

    /// <summary>
    /// Description:
    /// Updates the display text with the higch score value
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    public void DisplayHighScore()
    {
        if (displayText != null)
        {
            displayText.text = "High: " + GameManager.Instance.highScore.ToString();
        }
    }

    /// <summary>
    /// Description:
    /// Updates the UI element according to this class
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    public override void UpdateUI()
    {
        // This calls the base update UI function from the UIelement class
        base.UpdateUI();

        // The remaining code is only called for this sub-class of UIelement and not others
        DisplayHighScore();
    }
}
