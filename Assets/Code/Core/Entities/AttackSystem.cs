using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [Header("Weapon")]
    public WeaponSO currentWeapon;
    [Header("HitBox")]
    public BoxCollider2D hitBoxWeapon;
    [Header("Variables")]
    public bool isAttacking;
    public int damage;


    private void Awake()
    {
        if (hitBoxWeapon == null)
        {
            hitBoxWeapon = GetComponent<BoxCollider2D>();
        }
    }
    public void Attack(bool isGrounded)
    {
        if (isGrounded && !isAttacking)
        {
            GroundedAttacks();
        }
        else if (!isGrounded && !isAttacking)
        {
            AirAttacks();
        }
    }

    public void GroundedAttacks()
    {
        AttackSO attackSO = currentWeapon.GroundedAttackSos[0];
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));

    }

    public void AirAttacks()
    {
        AttackSO attackSO = currentWeapon.AirAttackSos[0];
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));
    }


    IEnumerator AttackCoroutine(AttackSO attackSO)
    {
        yield return new WaitForSeconds(attackSO.startup);
        hitBoxWeapon.size = attackSO.rangeAttack;
        hitBoxWeapon.gameObject.SetActive(true);
        damage = attackSO.damage;
        yield return new WaitForSeconds(attackSO.durationAttack);
        hitBoxWeapon.gameObject.SetActive(false);
        yield return new WaitForSeconds(attackSO.endlag);
        isAttacking = false;
    }

}
