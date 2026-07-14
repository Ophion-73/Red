using System;
using System.Collections.Generic;

public enum GameState { Boot, Menu, Generating, Playing, Paused, GameOver }

public static class GameEvents
{
    public static Action<GameState> OnGameStateChanged;
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;
    public static Action OnPlayerSpawn;
    
    // LEVELS
    public static Action OnRequestLevelGeneration;
    public static Action OnLevelGenerated;
}
