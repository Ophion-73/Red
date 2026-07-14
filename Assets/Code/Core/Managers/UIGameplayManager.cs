using UnityEngine;

public class UIGameplayManager : UISceneManager
{
    [Header("Gameplay Canvases")]
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas gameOverCanvas;

    public override void UpdateSceneUI(GameState newState)
    {
        DisableAllCanvases(hudCanvas, pauseCanvas, gameOverCanvas);

        switch (newState)
        {
            case GameState.Playing:
                ToggleCanvas(hudCanvas, true);
                break;
            
            case GameState.Paused:
                ToggleCanvas(hudCanvas, true); 
                ToggleCanvas(pauseCanvas, true);
                break;

            case GameState.GameOver:
                ToggleCanvas(gameOverCanvas, true);
                break;
        }
    }
}