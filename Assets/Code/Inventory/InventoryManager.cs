using UnityEngine;
using System.Collections.Generic;
using RED.Utility.Singleton;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("Base de Datos Global")]
    [Tooltip("Arrastra aquí TODOS los ScriptableObjects de ítems que existan en el juego")]
    public List<SOItem> itemDatabase = new List<SOItem>();

    [Header("Recursos de la Run")]
    public int money;
    public int heartPotions;

    [Header("Equipamiento Activo")]
    public SOWeapon equippedWeapon;
    public SOCharm equippedCharm;
    public SOHood equippedHood;
    public List<SOAmulet> equippedAmulets = new List<SOAmulet>();

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Dinero total: " + money);
    }
    
    public void SyncDataToSave()
    {
        RunData data = SaveManager.Instance.CurrentRun;
        
        data.currentMoney = money;
        data.currentHeartPotions = heartPotions;
        
        data.equippedWeaponID = equippedWeapon != null ? equippedWeapon.itemID : "";
        data.equippedCharmID = equippedCharm != null ? equippedCharm.itemID : "";
        data.equippedHoodID = equippedHood != null ? equippedHood.itemID : "";

        data.equippedAmuletIDs.Clear();
        
        foreach (SOAmulet amulet in equippedAmulets)
        {
            if (amulet != null) data.equippedAmuletIDs.Add(amulet.itemID);
        }
    }
    
    public void LoadDataFromSave()
    {
        RunData data = SaveManager.Instance.CurrentRun;

        money = data.currentMoney;
        heartPotions = data.currentHeartPotions;

        equippedWeapon = GetItemFromDatabase<SOWeapon>(data.equippedWeaponID);
        equippedCharm = GetItemFromDatabase<SOCharm>(data.equippedCharmID);
        equippedHood = GetItemFromDatabase<SOHood>(data.equippedHoodID);

        equippedAmulets.Clear();
        
        foreach (string amuletID in data.equippedAmuletIDs)
        {
            SOAmulet loadedAmulet = GetItemFromDatabase<SOAmulet>(amuletID);
            if (loadedAmulet != null) equippedAmulets.Add(loadedAmulet);
        }
    }
    
    private T GetItemFromDatabase<T>(string idToSearch) where T : SOItem
    {
        if (string.IsNullOrEmpty(idToSearch)) return null;

        foreach (SOItem item in itemDatabase)
        {
            if (item.itemID == idToSearch && item is T correctTypeItem) return correctTypeItem;
        }
        
        Debug.LogWarning($"[InventoryManager] No se encontró el ítem con ID: {idToSearch}");
        return null;
    }
}
