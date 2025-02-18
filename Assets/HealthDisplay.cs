using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText; // Reference to TextMeshPro UI element for health display
    public PlayerBehaviour player; // Reference to PlayerBehaviour script to get health

    // Call this method to update health display in the UI
    public void UpdateHealthDisplay()
    {
        healthText.text = "Health: " + References.thePlayer.health.ToString();
    }
}
