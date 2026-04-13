using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Canvas")]
    [Tooltip("Arrastra los componentes Canvas, no los GameObjects")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas gameOverCanvas;

    private void OnEnable()
    {
        GameEvents.OnGameStateChanged += UpdateUIState;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStateChanged -= UpdateUIState;
    }

    private void UpdateUIState(GameState newState)
    {
        DisableAllCanvases();
        
        switch (newState)
        {
            case GameState.Menu:
                if (menuCanvas != null) menuCanvas.enabled = true;
                break;

            case GameState.Playing:
                if (hudCanvas != null) hudCanvas.enabled = true;
                break;

            case GameState.GameOver:
                if (gameOverCanvas != null) gameOverCanvas.enabled = true;
                break;
        }
    }

    private void DisableAllCanvases()
    {
        if (menuCanvas != null) menuCanvas.enabled = false;
        
        if (hudCanvas != null) hudCanvas.enabled = false;
        
        if (gameOverCanvas != null) gameOverCanvas.enabled = false;
    }
}
