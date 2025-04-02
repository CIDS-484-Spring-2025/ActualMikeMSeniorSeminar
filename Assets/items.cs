using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI inventoryText; // Reference to the UI text displaying inventory
    public int maxItemsToShow; // Limit on how many items are displayed at once
    public GameObject hint; // UI element that shows the "Press E to use" message

    private string selectedItem = ""; // Stores the currently selected item
    void Awake()
    {
        References.InventoryDisplay = this;


    }
    private void Start()
    {
        ShowInventory(); // Display the inventory at the start

        // Hide the tooltip initially
        if (hint != null)
            hint.SetActive(false);
    }

    // Displays the inventory items in the UI
    public void ShowInventory()
    {
        if (GameManager.instance.inventory.Count == 0)
        {
            inventoryText.text = "No items collected.";
            return;
        }

        inventoryText.text = "Inventory:\n";
        int count = 0;

        foreach (string item in GameManager.instance.inventory)
        {
            if (count >= maxItemsToShow) break;

            inventoryText.text += $"- <color=yellow><link={item}>{item}</link></color>\n";
            count++;
        }

        if (GameManager.instance.inventory.Count > maxItemsToShow)
        {
            inventoryText.text += $"...and {GameManager.instance.inventory.Count - maxItemsToShow} more.";
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

    void UseItem(string item)
    {
        Debug.Log($"Used: {item}");

        // Ensure the player reference is valid
        if (References.thePlayer == null)
        {
            Debug.LogError("Player reference is missing!");
            return;
        }

        // Increase health based on item type
        if (item != "cake")
        {
            Debug.Log("Gained 10 HP");
            References.thePlayer.health += 10;
        }
        else
        {
            Debug.Log("Gained 20 HP");
            References.thePlayer.health += 20;
        }

        // Update the health UI
        References.HealthDisplay.UpdateHealthDisplay();

        // Remove item from inventory
        if (GameManager.instance.inventory.Contains(item))
        {
            GameManager.instance.inventory.Remove(item);
            Debug.Log($"Item {item} removed from inventory. Remaining items: {string.Join(", ", GameManager.instance.inventory)}");
        }
        else
        {
            Debug.LogError($"Item {item} not found in inventory!");
        }

        // Hide tooltip
        if (hint != null)
        {
            hint.SetActive(false);
        }

        selectedItem = ""; // Clear selection

        // Refresh inventory UI
        ShowInventory();
    }
}
