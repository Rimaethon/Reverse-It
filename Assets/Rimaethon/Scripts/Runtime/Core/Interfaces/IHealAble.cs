namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IHealAble
    {
        void ReceiveHealing(IGiveHeal healingSource);
    }
}