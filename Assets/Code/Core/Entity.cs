using UnityEngine;
using NaughtyAttributes;

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

    #region Knockback
    private Rigidbody _rb;

    [SerializeField] private Vector3 _knockbackDirection;
    [SerializeField] private float _knockbackForce;
    #endregion

    [SerializeField] private bool _isAlive = true;

    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    #region Getters & Setters

    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

    public float CurrentDamage { get => _currentDamage; set => _currentDamage = value; }
    public float MaxDamage { get => _maxDamage; set => _maxDamage = value; }

    public bool IsAlive { get => _isAlive; set => _isAlive = value; }

    #endregion

    #region Methods

    [Button]
    public void Heal()
    {
        _currentHealth += 10f;
        Debug.Log("Ah... I feel better (Healed 10 HP)");
    }

    [Button]
    public void TakeDamage()
    {
        _currentHealth -= 10f;
        Debug.Log("Ouch! That hurt (Lost 10 HP)");

        if (_currentHealth <= 0)
            Die();
    }

    [Button]
    public void TakeDamageWithKnockback()
    {
        TakeDamage();

        Vector3 directionForKnockback = _knockbackDirection;
        directionForKnockback.Normalize();

        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, _rb.linearVelocity.z);

        _rb.AddForce(directionForKnockback * _knockbackForce, ForceMode.Impulse);

        Debug.Log("Hey! You knocked the air out of me (Lost 10 HP and got knockback)");
    }

    [Button]
    public void Die()
    {
        _currentHealth = 0f;
        _isAlive = false;
        gameObject.SetActive(false);

        Debug.Log("My story shouldn't end like this (Died)");
    }

    #endregion
}

