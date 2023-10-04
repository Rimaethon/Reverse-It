namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IHealth
    {
        int Health { get; }
        void Heal(int amount);
        void TakeDamage(int amount);
    }
}