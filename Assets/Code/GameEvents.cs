using System;

public enum GameState { Menu, Playing, GameOver }

public static class GameEvents
{
    public static Action<GameState> OnGameStateChanged;
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;
}
