using System;

public enum GameState { Boot, Menu, Generating, Playing, GameOver }

public static class GameEvents
{
    public static Action<GameState> OnGameStateChanged;
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;
    public static Action OnRequestLevelGeneration;
    public static Action OnLevelGenerated;
    public static Action OnPlayerSpawn;
}
