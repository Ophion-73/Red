using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{ 
    [SerializeField] private Transform startingPoint;
    
    public void BuildLevel(List<GameObject> roomPrefabs)
    {
        Transform lastExit = startingPoint;

        foreach (GameObject prefab in roomPrefabs)
        {
            if (prefab == null)
            {
                continue;
            }
            GameObject roomInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            RoomConnector connector = roomInstance.GetComponent<RoomConnector>();
            if (connector == null)
            {
                Debug.LogError($" {prefab.name} no tiene el componente RoomConnector.");
                Destroy(roomInstance);
                continue;
            }
            
            if (lastExit != null)
            {
                Vector3 entranceOffset = connector.Entrance.position - roomInstance.transform.position;
                roomInstance.transform.position = lastExit.position - entranceOffset;
            }
            else
            {
                Vector3 entranceOffset = connector.Entrance.position - roomInstance.transform.position;
                roomInstance.transform.position = Vector3.zero - entranceOffset;
            }
            lastExit = connector.Exit;
        }
    }
}
