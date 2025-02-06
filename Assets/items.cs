using UnityEngine;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI inventoryText;
    public int maxItemsToShow; // Limit items shown

    void Start()
    {
        ShowInventory();
    }

    void ShowInventory()
    {
        if (ObjectPickup.inventory.Count == 0)
        {
            inventoryText.text = "No items collected.";
            return;
        }

        inventoryText.text = "Inventory:\n";
        int count = 0;

        foreach (string item in ObjectPickup.inventory)
        {
            if (count >= maxItemsToShow) break; // Stop displaying extra items
            inventoryText.text += "- " + item + "\n";
            count++;
        }

        if (ObjectPickup.inventory.Count > maxItemsToShow)
        {
            inventoryText.text += "...and " + (ObjectPickup.inventory.Count - maxItemsToShow) + " more.";
        }
    }
}
