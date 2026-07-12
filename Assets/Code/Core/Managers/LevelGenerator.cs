using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public ScenarioData config;
    private List<GameObject> finalMap = new List<GameObject>();

    /*void Start()
    {
        BuildRoute();
        InstantiateMap();
    }*/

    private void OnEnable()
    {
        GameEvents.OnRequestLevelGeneration += GenerateFullLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnRequestLevelGeneration -= GenerateFullLevel;
    }

    private void GenerateFullLevel()
    {
        BuildRoute();
        InstantiateMap();
        GameEvents.OnLevelGenerated?.Invoke();
    }

    void BuildRoute()
    {
        finalMap.Clear();

        foreach (LevelSlot slot in config.levelSequence)
        {
            if (slot.isRequired && slot.fixedPrefab != null)
            {
                finalMap.Add(slot.fixedPrefab);
            }
            else
            {
                finalMap.Add(SelectFromPool(slot.type));
            }
        }

        // --- NUEVA SECCIÓN DE LOGS ---
        Debug.Log($"<color=green><b>[LevelGenerator]</b> Ruta construida. Total de habitaciones guardadas: {finalMap.Count}</color>");
        for (int i = 0; i < finalMap.Count; i++)
        {
            if (finalMap[i] != null)
            {
                Debug.Log($"Habitación [{i}]: {finalMap[i].name}");
            }
            else
            {
                Debug.LogWarning($"Habitación [{i}]: ¡Alerta! El objeto es nulo (revisa las pools o prefabs fijos).");
            }
        }
        // ------------------------------
    }

    GameObject SelectFromPool(RoomType type)
    {
        switch (type)
        {
            case RoomType.Combat:
                return config.combatRoomsPool[Random.Range(0, config.combatRoomsPool.Count)];
            case RoomType.Event:
                return config.eventRoomsPool[Random.Range(0, config.eventRoomsPool.Count)];
            default:
                return null;
        }
    }

    void InstantiateMap()
    {
        Vector3 nextPosition = Vector3.zero;
        Transform lastRoom = null; // Nota: En tu código anterior era lastExit, se mantiene como lo pusiste.

        foreach (GameObject roomPrefab in finalMap)
        {
            GameObject roomInstance = Instantiate(roomPrefab, nextPosition, Quaternion.identity);
            
            Transform entrance = roomInstance.transform.Find("Entrance");
            if (lastRoom != null && entrance != null)
            {
                Vector3 offset = entrance.position - roomInstance.transform.position;
                roomInstance.transform.position = lastRoom.position - offset;
            }

            lastRoom = roomInstance.transform.Find("Exit");
        }
    }
}