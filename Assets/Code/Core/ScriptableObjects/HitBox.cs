using UnityEngine;

public class HitBox : MonoBehaviour
{
    private AttackSystem attackSystem;
    public GameObject Owner;

    private void Awake()
    {
        
        attackSystem = GetComponent<AttackSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (collision == Owner)
        {
            return;
        }
        damageable.TakeDamage(attackSystem.damage);
        damageable.TakeKnockback(attackSystem.dirKnocBack, attackSystem.forceKnockback);
    }
}
