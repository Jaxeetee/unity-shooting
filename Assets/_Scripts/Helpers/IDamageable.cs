public interface IDamageable
{
    int currentHealth { get; set; }
    void TakeDamage(int damageAmount);
}