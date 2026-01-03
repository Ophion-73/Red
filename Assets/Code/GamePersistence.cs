using UnityEngine;

public class GamePersistence : MonoBehaviour
{
    public SaveManager saveManager;

    [Header("Player Stats")]
    public float health; 
    public int damage;

    // guardado in run
    public void SaveCheckpoint()
    {
        RunData data = new RunData();
        data.currentHealth = health;
        data.currentDamage = damage;
        data.currentRoom = 5; // ejemplo

        saveManager.SaveRun(data);
    }
    
    public void OnPlayerDeath()
    {
        MetaData meta = saveManager.LoadMeta();
        saveManager.SaveMeta(meta);
        
        saveManager.DeleteRun();
    }
    
    public void StartGame()
    {
        RunData savedRun = saveManager.LoadRun();

        if (savedRun != null)
        {
            health = savedRun.currentHealth;
            damage = savedRun.currentDamage;
            Debug.Log("Continuing existing run...");
        }
        else
        {
            MetaData meta = saveManager.LoadMeta();
            damage = 10 + meta.permanentDamageLevel;
            health = 100;
            Debug.Log("Starting fresh run with Meta Upgrades.");
        }
    }
}
