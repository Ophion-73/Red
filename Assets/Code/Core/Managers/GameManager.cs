using UnityEngine;
using RED.Utility.Singleton;
using UnityEngine.XR;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        ChangeState(GameState.Menu);
    }
    
    private void OnEnable()
    {
        GameEvents.OnPlayerDied += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDied -= HandlePlayerDeath;
    }
    
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        GameEvents.OnGameStateChanged?.Invoke(newState);
    }

    private void HandlePlayerDeath()
    {
        ChangeState(GameState.GameOver);
    }
}
