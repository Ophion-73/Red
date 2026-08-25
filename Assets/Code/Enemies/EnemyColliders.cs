using UnityEngine;
using System.Collections.Generic;

public class EnemyColliders : MonoBehaviour
{
    public static EnemyColliders instance;

    [SerializeField] private int _maxCollisionEnemies;

    private List<Enemy> combatEnemies = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void EnterCombat(Enemy enemy)
    {
        if (combatEnemies.Contains(enemy))
            return;

        combatEnemies.Add(enemy);

        Debug.Log($"{enemy.name} entro en combate");

        UpdateCollisionSlots();

    }

    public void ExitCombat(Enemy enemy)
    {
        if (!combatEnemies.Contains (enemy)) 
            return;

        combatEnemies.Remove(enemy);

        Debug.Log($"{enemy.name} salio del combate");

        UpdateCollisionSlots();

    }

    private void UpdateCollisionSlots()
    {
        for (int i = 0; i < combatEnemies.Count; i++)
        {
            if (i < _maxCollisionEnemies)
            {
                combatEnemies[i].SetCollisionLayer(true);
            }
            else
            {
                combatEnemies[i].SetCollisionLayer(false);
            }
        }
    }
}
