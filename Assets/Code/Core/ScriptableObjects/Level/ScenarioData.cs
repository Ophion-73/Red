using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EscenarioConfig", menuName = "Juego/ScenarioData")]
public class ScenarioData : ScriptableObject
{
    [Header("Estructura de la Partida")]
    public List<LevelSlot> secuenciaDeNiveles;

    [Header("Pools de Variedad")]
    public List<GameObject> combatRoomsPool;
    public List<GameObject> shopRoomsPool;
}
