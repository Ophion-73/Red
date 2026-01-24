using System.Collections;
using Unity.VisualScripting;
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

    #region Switch
    public void Attack(bool isGrounded, AttackDirection dir)
    {
        if (!isAttacking)
        {
            switch (dir)
            {
                case AttackDirection.Up:
                    if (isGrounded)
                    {
                        GroundedAttacksUp();
                    }
                    else
                    {
                        AirAttacksUp();
                    }
                    break;
                case AttackDirection.Down:
                    if (isGrounded)
                    {
                        GroundedAttacksDown();
                    }
                    else
                    {
                        AirAttacksDown();
                    }
                    break;
                case AttackDirection.Right:
                    if (isGrounded)
                    {
                        GroundedAttacksRight();
                    }
                    else
                    {
                        AirAttacksRight();
                    }
                    break;
                case AttackDirection.Left:
                    if (isGrounded)
                    {
                        GroundedAttacksLeft();
                    }
                    else
                    {
                        AirAttacksLeft();
                    }
                    break;
                case AttackDirection.Neutral:
                    if (isGrounded)
                    {
                        GroundedAttacksNeutral();
                    }
                    else
                    {
                        AirAttacksNeutral();
                    }
                    break;
            }   

        }


    }
    #endregion

    #region GroundedAttacks
    public void GroundedAttacksNeutral()
    {
        AttackSO attackSO = currentWeapon.GroundedNeutralAttackSos[0];
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));

    }
    public void GroundedAttacksUp()
    {
        AttackSO attackSO = currentWeapon.GroundedUpAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));

    }
    public void GroundedAttacksLeft()
    {
        AttackSO attackSO = currentWeapon.GroundedLeftAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));

    }
    public void GroundedAttacksRight()
    {
        AttackSO attackSO = currentWeapon.GroundedRightAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));

    }
    public void GroundedAttacksDown()
    {
        AttackSO attackSO = currentWeapon.GroundedDownAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));

    }
    #endregion

    #region AirAttacks
    public void AirAttacksUp()
    {
        AttackSO attackSO = currentWeapon.AirUpAttackSOs;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));
    }
    public void AirAttacksDown()
    {
        AttackSO attackSO = currentWeapon.AirDownAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));
    }
    public void AirAttacksRight()
    {
        AttackSO attackSO = currentWeapon.AirRightAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));
    }
    public void AirAttacksLeft()
    {
        AttackSO attackSO = currentWeapon.AirLeftAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));
    }

    public void AirAttacksNeutral()
    {
        AttackSO attackSO = currentWeapon.AirNeutralAttackSos;
        isAttacking = true;
        StartCoroutine(AttackCoroutine(attackSO));
    }
    #endregion

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
