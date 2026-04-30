using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Resources")]
    public int money;
    public int heartPotions;

    [Header("Equipment")]
    public SOWeapon equippedWeapon;
    public SOCharm equippedCharm;
    public SOHood equippedHood;
    
    public List<SOAmulet> equippedAmulets = new List<SOAmulet>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Dinero total: " + money);
    }
}
