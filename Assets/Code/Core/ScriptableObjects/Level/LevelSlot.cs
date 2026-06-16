using UnityEngine;
public enum RoomType { Start, Combat, Shop, MiniBoss, Boss, Event }

[System.Serializable]
public class LevelSlot
{
    public RoomType type;
    public bool isRequired;
    public GameObject fixedPrefab;
}
