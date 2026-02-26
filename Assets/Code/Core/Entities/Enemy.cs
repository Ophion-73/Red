using UnityEngine;

public enum State { Idle, Chasing, Attacking, Dead }
public class Enemy : Entity
{
    [Header("State Machine")]
    [SerializeField] protected State _currentState = State.Idle;
    
    protected Player _playerRef;
    
    [Header("Detection Settings")]
    [SerializeField] protected float _detectionRange = 10f;

    protected override void Awake()
    {
        base.Awake();
        _playerRef = FindFirstObjectByType<Player>(); 
        if (_playerRef == null) Debug.LogWarning($"Enemy " + gameObject.name + " no encontro a player");
    }

    protected virtual void Update()
    {
        if (!IsAlive || _playerRef == null) return;
        
        switch (_currentState)
        {
            case State.Idle:
                HandleIdle();
                break;
            case State.Chasing:
                HandleChasing();
                break;
            case State.Attacking:
                HandleAttacking();
                break;
        }
    }

    protected virtual void HandleIdle()
    {
        
    }

    protected virtual void HandleChasing()
    {
        FlipTowardsPlayer();
    }

    protected virtual void HandleAttacking()
    {
        
    }
    
    public void ChangeState(State newState)
    {
        if (_currentState == newState) return;
        
        _currentState = newState;
    }

    protected void FlipTowardsPlayer()
    {
        float direction = _playerRef.transform.position.x - transform.position.x;
        if (direction > 0.1f)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (direction < -0.1f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
