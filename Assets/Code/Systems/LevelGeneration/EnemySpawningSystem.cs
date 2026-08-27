using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemySpawningSystem
{
    private EnemyBundleSO _enemies; 
    private List<Transform> _spawners;
    private int _budget;
    private LevelManager _levelManager;

    private List<Enemy> _enemyPool = new List<Enemy>();
    private List<Transform> _unUsedSpawnPoints = new List<Transform>();

    public EnemySpawningSystem(EnemyBundleSO enemies, List<Transform> spawners, int budget, LevelManager levelManager)
    {
        _enemies = enemies;
        _spawners = spawners;
        _budget = budget;
        _levelManager = levelManager;
        
    }

    public void SpawnEnemies()
    {
        CreateEnemiesList();

        int remainingBudget = _budget;
        while (remainingBudget > 0) 
        {
            int randomIndex = UnityEngine.Random.Range(1, _enemies.enemy.Length);
            if (_enemyPool[randomIndex].cost == 0)
            {
                Debug.Log("Ningun enemigo puede tener costo 0, eso rompe el juego porfavor no hagan eso jaja saludos");
                break;
            }
            if (_enemyPool[randomIndex].cost > remainingBudget)
            {
                _enemyPool.Remove(_enemyPool[randomIndex]);
                continue;
            }
            else
            {
                if (_unUsedSpawnPoints.Count == 0)
                    CreateSpawnPointList();  
                int randomSpawner = UnityEngine.Random.Range(0, _unUsedSpawnPoints.Count);
                GameObject enemy = Array.Find(_enemies.enemy, enemyToSpawn => enemyToSpawn.GetComponent<Enemy>().enemyId == _enemyPool[randomIndex].enemyId);
                _levelManager.SpawnEnemy(enemy, _unUsedSpawnPoints[randomSpawner]);
                _unUsedSpawnPoints.Remove(_unUsedSpawnPoints[randomSpawner]);
                remainingBudget -= _enemyPool[randomIndex].cost;
            }
        }
    }

    public void CreateEnemiesList()
    {
        for (int i = 0; i < _enemies.enemy.Length; i++)
        {
            if (_enemies.enemy[i].GetComponent<Enemy>() != null)
                _enemyPool.Add(_enemies.enemy[i].GetComponent<Enemy>());
            else
            {
                Debug.Log("los game objects no tienen el script enemy");
                return;
            }
        }
        
    }
    public void CreateSpawnPointList()
    {
        for (int i = 0; i < _spawners.Count; i++)
        {
            _unUsedSpawnPoints.Add(_spawners[i]);
        }
    }
    
}
