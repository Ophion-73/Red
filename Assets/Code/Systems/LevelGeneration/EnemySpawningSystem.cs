using UnityEngine;
using System.Collections.Generic;

public class EnemySpawningSystem
{
    private EnemyBundleSO _enemies; 
    //private List<Transform> _spawners;
    //private int _budget;
    private LevelManager _levelManager;

    //private List<Enemy> _enemyPool = new List<Enemy>();
    //private List<Transform> _unUsedSpawnPoints = new List<Transform>();
    
    public EnemySpawningSystem(EnemyBundleSO enemy, LevelManager levelManager)
    {
        _enemies = enemy;
        _levelManager = levelManager;
    }
    
    public void SpawnEnemies(int budget, List<Transform> availableSpawnPoints)
    {
        if (_enemies == null || _enemies.enemy.Length == 0 || availableSpawnPoints.Count == 0) return;

        int remainingBudget = budget;
        
        // Clonamos la lista de puntos para ir tachando los usados y no encimar enemigos
        List<Transform> unusedSpawns = new List<Transform>(availableSpawnPoints);

        // PASO CLAVE: Encontrar el costo mínimo para evitar un bucle infinito
        int minCost = int.MaxValue;
        foreach (GameObject prefab in _enemies.enemy)
        {
            Enemy enemyScript = prefab.GetComponent<Enemy>();
            if (enemyScript != null && enemyScript.cost > 0 && enemyScript.cost < minCost)
            {
                minCost = enemyScript.cost;
            }
        }

        // El bucle se detiene si se acaba la plata, o si ya no hay dónde ponerlos
        while (remainingBudget >= minCost && unusedSpawns.Count > 0)
        {
            // Corregido: El Random.Range de un Array DEBE empezar en 0
            int randomIndex = Random.Range(0, _enemies.enemy.Length);
            GameObject selectedEnemyPrefab = _enemies.enemy[randomIndex];
            Enemy enemyScript = selectedEnemyPrefab.GetComponent<Enemy>();

            if (enemyScript.cost <= remainingBudget)
            {
                // Elegir un punto al azar de los que sobran
                int randomSpawnIndex = Random.Range(0, unusedSpawns.Count);
                Transform spawnPoint = unusedSpawns[randomSpawnIndex];

                // Le pedimos al LevelManager que saque al enemigo del Almacén (Pool)
                _levelManager.SpawnEnemyFromPool(selectedEnemyPrefab, spawnPoint);

                remainingBudget -= enemyScript.cost;
                unusedSpawns.RemoveAt(randomSpawnIndex); // Lo quitamos para no encimar al siguiente
            }
        }
    }
    
    /*public EnemySpawningSystem(EnemyBundleSO enemies, List<Transform> spawners, int budget, LevelManager levelManager)
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
    }*/
    
}
