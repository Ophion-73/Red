using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using NaughtyAttributes;

public class AttackSystem : MonoBehaviour
{
    //Esto esta documentado medio a lo wey si alguien ve esto y no le entiende pregunteme soy barrera
    [Header("Weapon")]
    public WeaponSO currentWeapon;
    [Header("HitBox")]
    public BoxCollider2D hitBoxWeapon;
    [Header("Variables")]
    public bool isAttacking;
    public int damage;
    public bool canChainAttack;
    public int comboIndex;
    private Coroutine _comboWindowCoroutine;
    private Coroutine _attackCoroutine;


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
        else if ( isAttacking)
        {
            return;
        }


    }
    #endregion

    #region GroundedAttacks
    [Button("Ataque de player")]
    public void GroundedAttacksNeutral()
    {
        //paso 1
        //si es true mandamos a llamar trychain attack
        if (canChainAttack)
        {
            TryCahinAttack();
            return;
        }
        else if (_attackCoroutine != null)
        {
            return ;
        }

        StartAttack();
 

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
    #region AttacksChecks
    public void TryCahinAttack()
    {
        //paso 6
        //si esta desactivado no hacemos nada pero si si activamos startattack
        if (!canChainAttack)
        {
            return;
        }
        comboIndex++;

        if (comboIndex >= currentWeapon.GroundedNeutralAttackSos.Length)
        {
            comboIndex = 0;
        }
        //si si es verdadero trychainattack llamamos el metodo startattack y pues ya se repite pero ahora con 1 mas en el index del array
        canChainAttack = false;
        StartAttack();
        
    }
    public void StartAttack()
    {
        //paso 2
        if (_attackCoroutine != null)
        {
            StopCoroutine( _attackCoroutine);
        }
        AttackSO attackSO = currentWeapon.GroundedNeutralAttackSos[comboIndex];
        _attackCoroutine = StartCoroutine(AttackCoroutine(attackSO));
    }

    public void OpenComboWindow(AttackSO attackSO)
    {
        //paso 4
        if (_comboWindowCoroutine != null)
        {
            StopCoroutine(_comboWindowCoroutine);
        }
        _comboWindowCoroutine = StartCoroutine(ComboWindowCoroutine(attackSO));
    }
    #endregion
    IEnumerator AttackCoroutine(AttackSO attackSO)
    {
        //paso 3
        //Corrutina para activar ataque
        isAttacking = true;
        yield return new WaitForSeconds(attackSO.startup);
        Debug.Log("Activate Hitbox");
        hitBoxWeapon.size = attackSO.rangeAttack;
        Debug.Log("Tamaño de hitbox" +  hitBoxWeapon.size);
        hitBoxWeapon.gameObject.SetActive(true);
        damage = attackSO.damage;
        yield return new WaitForSeconds(attackSO.durationAttack);
        hitBoxWeapon.gameObject.SetActive(false);
        OpenComboWindow(attackSO);
        yield return new WaitForSeconds(attackSO.endlag);
        comboIndex = 0;
        isAttacking = false;
        _attackCoroutine = null;
        Debug.Log("Finish Attack");
        
    }
    IEnumerator ComboWindowCoroutine(AttackSO attackSO)
    {
        //paso 5
        //Corrutina para ventana de combos con arrays
        canChainAttack = true;
        Debug.Log("Combo Open");

        yield return new WaitForSeconds(attackSO.comboWindow);

        canChainAttack = false;
        
        Debug.Log("Combo Close");
    }


}
