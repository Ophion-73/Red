using UnityEngine;

public class UIMenuManager : UISceneManager
{
    [Header("Menu Canvases")]
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas settingsCanvas;
    // [SerializeField] private Canvas creditsCanvas;

    public override void UpdateSceneUI(GameState newState)
    {
        DisableAllCanvases(menuCanvas, settingsCanvas);
        
        if (newState == GameState.Menu)
        {
            ToggleCanvas(menuCanvas, true);
        }
    }
    
    public void OpenSettings()
    {
        ToggleCanvas(menuCanvas, false);
        ToggleCanvas(settingsCanvas, true);
    }

    public void BackToMenu()
    {
        ToggleCanvas(settingsCanvas, false);
        ToggleCanvas(menuCanvas, true);
    }
}