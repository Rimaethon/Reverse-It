namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IDamageSource
    {
        int DamageAmount { get; }
        void ApplyDamage(IDamageable target);
    }
}