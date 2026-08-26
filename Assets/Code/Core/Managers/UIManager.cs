using RED.Utility.Singleton;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Global UI")]
    [SerializeField] private Canvas loadingCanvas;

    private UISceneManager currentSceneManager;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameEvents.OnGameStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStateChanged -= HandleGameStateChange;
    }

    public void RegisterSceneManager(UISceneManager sceneManager)
    {
        currentSceneManager = sceneManager;
        
        if (GameManager.Instance != null)
        {
            currentSceneManager.UpdateSceneUI(GameManager.Instance.CurrentState);
        }
    }

    private void HandleGameStateChange(GameState newState)
    {
        if (loadingCanvas != null)
        {
            loadingCanvas.enabled = (newState == GameState.Boot || newState == GameState.Generating);
        }
        
        if (currentSceneManager != null)
        {
            currentSceneManager.UpdateSceneUI(newState);
        }
    }
}
