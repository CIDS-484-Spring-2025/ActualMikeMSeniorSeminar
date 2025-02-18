using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI inventoryText; // Reference to the UI text displaying inventory
    public int maxItemsToShow; // Limit on how many items are displayed at once
    public GameObject hint; // UI element that shows the "Press E to use" message

    private string selectedItem = ""; // Stores the currently selected item

    private void Start()
    {
        ShowInventory(); // Display the inventory at the start

        // Hide the tooltip initially
        if (hint != null)
            hint.SetActive(false);
    }

    // Displays the inventory items in the UI
    void ShowInventory()
    {
        // If inventory is empty, show a message and return
        if (ObjectPickup.inventory.Count == 0)
        {
            inventoryText.text = "No items collected.";
            return;
        }

        inventoryText.text = "Inventory:\n"; // Start inventory text
        int count = 0;

        // Loop through inventory items and display them
        foreach (string item in ObjectPickup.inventory)
        {
            if (count >= maxItemsToShow) break; // Stop displaying extra items

            // Make the item name yellow and clickable using TextMeshPro rich text
            inventoryText.text += $"- <color=yellow><link={item}>{item}</link></color>\n";
            count++;
        }

        // If there are more items than maxItemsToShow, show a message indicating more items exist
        if (ObjectPickup.inventory.Count > maxItemsToShow)
        {
            inventoryText.text += $"...and {ObjectPickup.inventory.Count - maxItemsToShow} more.";
        }
    }

    private void Update()
    {
        // Detect if the user RIGHT-CLICKS an item in the inventory
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            // Check if the mouse is over a linked item in TextMeshPro
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(inventoryText, Input.mousePosition, null);
            if (linkIndex != -1) // If a link is found
            {
                TMP_LinkInfo linkInfo = inventoryText.textInfo.linkInfo[linkIndex];
                selectedItem = linkInfo.GetLinkID(); // Get the clicked item name
                ShowUseTooltip(selectedItem); // Show tooltip for the selected item
            }
        }

        // If an item is selected, allow pressing "E" to use it
        if (!string.IsNullOrEmpty(selectedItem) && Input.GetKeyDown(KeyCode.E))
        {
            UseItem(selectedItem);
        }
    }

    // Shows the "Press E to use" tooltip for the selected item
    void ShowUseTooltip(string item)
    {
        if (hint != null)
        {
            hint.SetActive(true);
            hint.GetComponent<TextMeshProUGUI>().text = $"Press E to use {item}";
        }
    }

    // Handles item usage (removing it from inventory)
    void UseItem(string item)
    {
        Debug.Log($"Used: {item}");

        //if sepcific item do somethign special
        if (item != "cake")
        {
            Debug.Log("gained 10 hp");
            References.thePlayer.health += 10;
        }
        else
        {
            Debug.Log("gained 20 hp");
            References.thePlayer.health += 20;
        }
      

        // Remove the item from inventory
        ObjectPickup.inventory.Remove(item);

        // Hide the tooltip after using the item
        hint.SetActive(false);
        selectedItem = ""; // Clear the selected item

        // Refresh inventory display to reflect changes
        ShowInventory();
       
        // Update the health display UI (to reflect the health change)
        FindObjectOfType<HealthDisplay>().UpdateHealthDisplay();
    }
}
