using UnityEngine;

public abstract class UISceneManager : MonoBehaviour
{
    protected virtual void Start()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.RegisterSceneManager(this);
        }
    }

    public abstract void UpdateSceneUI(GameState newState);

    protected void ToggleCanvas(Canvas canvas, bool state)
    {
        if (canvas != null) 
        {
            canvas.enabled = state;
        }
    }

    protected void DisableAllCanvases(params Canvas[] canvases)
    {
        foreach (var canvas in canvases)
        {
            ToggleCanvas(canvas, false);
        }
    }
}
