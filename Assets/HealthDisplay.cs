using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI healthText; // Reference to TextMeshPro UI element for health display
    public PlayerBehaviour player; // Reference to PlayerBehaviour script to get health

    
    private void Awake()
    {
        References.HealthDisplay = this;
       

    }
    public void UpdateHealthDisplay()
    {
        if (player != null && healthText != null)
        {
            healthText.text = " Your Health: " + player.health.ToString();
        }
        else
        {
            Debug.LogError("Health UI or Player reference missing!");
        }
    }
   

    // Load health data when game loads
    public void LoadData(GameData data)
    {
        if (player != null)
        {
            player.health = data.playerHealth; // Set player's health
            UpdateHealthDisplay(); // Update UI
        }
    }

    // Save health data when game saves
    public void SaveData(GameData data)
    {
        if (player != null)
        {
            data.playerHealth = player.health;
        }
    }
}

