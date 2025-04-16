using System.Collections.Generic;
using UnityEngine;

public class WeaponRespawn : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    public bool collected = false;

    // Store all instances of WeaponRespawn
    public static List<WeaponRespawn> AllWeapons = new List<WeaponRespawn>();

    [ContextMenu("Generate GUID for ID")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        if (string.IsNullOrEmpty(id))
        {
            GenerateGuid(); // Ensure every item has a unique ID
        }

        if (!AllWeapons.Contains(this))
        {
            AllWeapons.Add(this);
        }
    }

    private void Start()
    {
        if (GameManager.instance.HasPickedUp(id)) // Check if item was already collected
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogWarning("Item ID is missing! Skipping data load.");
            return;
        }

        if (data.itemsCollected.TryGetValue(id, out collected) && collected)
        {
            gameObject.SetActive(false); // Hide if already collected
        }

        data.itemsCollected[id] = collected;
        

    }

    public void SaveData(GameData data)
    {
        /*if (string.IsNullOrEmpty(id))
        {
            Debug.LogError("Item ID is null or empty. Cannot save data.");
            
        }

        data.itemsCollected[id] = collected; // Save collection status*/
    }

    // Collect the item but only call Save when necessary (e.g., after selling)
    public void Collect()
    {
        collected = true;
        gameObject.SetActive(false); // Hide the object
        SaveCollectedStatus();

        // Do not save immediately after collection; save later when appropriate
        // (e.g., after player interaction with shop or upon game exit)
    }

    // This can be called when you're ready to save the data
    public void SaveCollectedStatus()
    {
        DataPersistenceManager.instance.SaveGame(); // Save only when necessary
    }

    public static void CollectItemByName(string itemName)
    {
        // Remove invalid (destroyed) references from the AllWeapons list
        AllWeapons.RemoveAll(w => w == null || w.gameObject == null);

        // Find the weapon by name
        WeaponRespawn weapon = AllWeapons.Find(w => w.gameObject.name == itemName);
        if (weapon != null)
        {
            weapon.Collect();
            Debug.Log($"Collected item: {itemName}");
        }
        else
        {
            Debug.LogError($"No WeaponRespawn found with name: {itemName}");
        }
    }

}


