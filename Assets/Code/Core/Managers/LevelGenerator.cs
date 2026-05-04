using UnityEngine;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour
{
    public ScenarioData config;
    private List<GameObject> mapaFinal = new List<GameObject>();

    void Start()
    {
        ConstruirRuta();
        InstanciarMapa();
    }

    void ConstruirRuta()
    {
        mapaFinal.Clear();

        foreach (LevelSlot slot in config.secuenciaDeNiveles)
        {
            if (slot.esObligatorio && slot.prefabFijo != null)
            {
                mapaFinal.Add(slot.prefabFijo);
            }
            else
            {
                mapaFinal.Add(SeleccionarDePool(slot.tipo));
            }
        }
    }

    GameObject SeleccionarDePool(RoomType tipo)
    {
        switch (tipo)
        {
            case RoomType.Combat:
                return config.combatRoomsPool[Random.Range(0, config.combatRoomsPool.Count)];
            case RoomType.Shop:
                return config.shopRoomsPool[Random.Range(0, config.shopRoomsPool.Count)];
            default:
                return null;
        }
    }

    void InstanciarMapa()
    {
        Vector3 proximaPosicion = Vector3.zero;
        Transform ultimaSalida = null;

        foreach (GameObject prefab in mapaFinal)
        {
            GameObject room = Instantiate(prefab, proximaPosicion, Quaternion.identity);
            
            Transform entrada = room.transform.Find("Entrada");
            if (ultimaSalida != null && entrada != null)
            {
                Vector3 offset = entrada.position - room.transform.position;
                room.transform.position = ultimaSalida.position - offset;
            }

            ultimaSalida = room.transform.Find("Salida");
        }
    }
}
