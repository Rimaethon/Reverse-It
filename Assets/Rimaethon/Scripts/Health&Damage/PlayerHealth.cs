using Rimaethon.Scripts.Core.Interfaces;
using UnityEngine;

namespace Rimaethon.Scripts.Health_Damage
{
    public class PlayerHealth : BaseHealth,IHealAble
    {
        
        

        public  void ReceiveHealing(IGiveHeal healingSource)
        {
            Health += healingSource.HealAmount;
        }
      
        


      
    }
}
