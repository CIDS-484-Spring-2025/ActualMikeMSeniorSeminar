using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public Vector3 playerPosition; // Store player position
    public string lastScene; // Store the last scene name
    public List<string> pickedUpObjects = new List<string>(); // Store picked-up objects
    public List<string> inventory = new List<string>(); // Store collected items
    public int money = 100; // Default starting money
    


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    // Save player position
    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
        lastScene = SceneManager.GetActiveScene().name;
    }

    // Add picked-up objects to inventory
  public void AddPickedUpObject(string itemName)
    {
 
        if (itemName == "nextEnemy")
        {
            Debug.Log($"Tried to add enemy to inventory");
            Destroy(gameObject);
            return;
        }
       
        if (!inventory.Contains(itemName)) // Avoid duplicates
        {
            
            inventory.Add(itemName);
            Debug.Log($"Added {itemName} to inventory!");
            //References.WeaponRespawn.Collect();
            // Refresh the inventory display immediately
            if (FindObjectOfType<InventoryDisplay>() != null)
            {
                FindObjectOfType<InventoryDisplay>().ShowInventory();
            }
        }

    }

    public bool HasPickedUp(string objectName)
    {
        return pickedUpObjects.Contains(objectName);
    }
    
}
