namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IDamageable
    {
        public int TeamId { get; }
        public int Health { get; }
        void TakeDamage(IDamageSource source);
    }
}