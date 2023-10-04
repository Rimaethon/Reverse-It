namespace Rimaethon.Scripts.Core.Interfaces
{
    public interface IDamageAble
    {
        int TeamId { get; }

        void TakeDamage(int damageAmount, float contactNormal = 0)
        {
        }
    }
}