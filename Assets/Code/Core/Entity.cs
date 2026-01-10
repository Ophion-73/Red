using UnityEngine;
using NaughtyAttributes;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour
{
    #region Variables

    #region Stats
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _currentDamage;
    [SerializeField] private float _maxDamage;
    #endregion

    #region Testing

    [SerializeField] private Vector3 _knockbackDirectionTest;
    [SerializeField] private float _knockbackForceTest;
    [SerializeField] private float _damageTest;

    #endregion

    private Rigidbody _rb;

    [SerializeField] private bool _isAlive = true;

    #endregion

    #region Getters & Setters

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

    public float CurrentDamage { get => _currentDamage; set => _currentDamage = value; }
    public float MaxDamage { get => _maxDamage; set => _maxDamage = value; }

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    #endregion

    #region Methods Testing
    [Button]
    public void TakeDamageAndKnockBackTest()
    {
        TakeDamage(_damageTest);
        TakeKnockback(_knockbackDirectionTest, _knockbackForceTest);
    }

    #endregion


    #region Methods

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Healing(float heal)
    {
        _currentHealth += heal;
        Debug.Log("Ah... I feel better (Healed HP)");
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("Ouch! That hurt (Lost HP)");

        if (_currentHealth <= 0)
            Die();
    }

    public void TakeKnockback( Vector3 knockbackDirection, float knockbackForce)
    {
        Vector3 directionForKnockback = knockbackDirection;
        directionForKnockback.Normalize();

        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, _rb.linearVelocity.z);
        _rb.AddForce(directionForKnockback * knockbackForce, ForceMode.Impulse);

        Debug.Log("Hey! You knocked the air out of me (Got knockback)");
    }

    public void Die()
    {
        if (!_isAlive) return;

        _isAlive = false;
        gameObject.SetActive(false);
        Debug.Log("My story shouldn't end like this (Died)");
    }

    #endregion
}

