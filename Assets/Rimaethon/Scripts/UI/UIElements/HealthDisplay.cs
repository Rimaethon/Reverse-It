using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine.UI;

namespace Rimaethon.Scripts.UI.UIElements
{
    public class HealthDisplay : UIElement
    {
        private Text _healthDisplayText;
        
        private void OnEnable()
        {
            EventManager.Instance.AddHandler<int>(GameEvents.OnHealthChange, UpdateHealthDisplay);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnHealthChange, UpdateHealthDisplay);
        }

        private void UpdateHealthDisplay(int healthValue)
        {
            _healthDisplayText.text = $"Health: {healthValue}";
        }
        
    
    }
}