using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [Header("Weapon")]
    public WeaponSO currentWeapon;

    public void Attack(bool isGrounded)
    {
        if (isGrounded)
        {
            GroundedAttacks();
        }
        else
        {
            AirAttacks();
        }
    }

    public void GroundedAttacks()
    {
        AttackSO attack = currentWeapon.attackSos[0];
        int damage = attack.damage;
    }

    public void AirAttacks()
    {

    }

}
