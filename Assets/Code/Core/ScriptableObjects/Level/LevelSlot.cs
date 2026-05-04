using UnityEngine;
public enum RoomType { Start, Combat, Shop, MiniBoss, Boss }

[System.Serializable]
public class LevelSlot
{
    public RoomType tipo;
    public bool esObligatorio;
    public GameObject prefabFijo;
}
