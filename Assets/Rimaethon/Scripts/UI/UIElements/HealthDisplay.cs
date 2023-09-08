using System;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Health_Damage;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon.Scripts.UI.UIElements
{
    public class HealthDisplay : UIElement
    {
        private Text _healthDisplayText;

        private void OnEnable()
        {
            EventManager.Instance.AddHandlerö(GameEvents.OnPlayingAudio, UpdateHealthDisplay);
        }

        private void UpdateHealthDisplay(int healthValue)
        {
            _healthDisplayText.text = $"Health: {HealthManager.Instance.health}";
        }
    
    }
}