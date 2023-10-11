namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IGiveHeal
    {
        public int HealAmount { get; }
        void GiveHeal(IHealAble target);
    }
}