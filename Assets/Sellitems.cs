using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Sellitems : MonoBehaviour
{
 
    public TextMeshProUGUI inventoryText; // Reference to the UI text displaying inventory
    public int maxItemsToShow; // Limit on how many items are displayed at once
    public GameObject hint; // UI element that shows the "Press E to use" message
    //public TextMeshProUGUI confirmText; // Confirmation text

    private string selectedItem = ""; // Stores the currently selected item

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

            inventoryText.text += $"- <color=red><link={item}>{item}</link></color>\n";
            count++;
        }

        if (GameManager.instance.inventory.Count > maxItemsToShow)
        {
            inventoryText.text += $"...and {GameManager.instance.inventory.Count - maxItemsToShow} more.";
        }
    }

    private void Update()
    {
        //cancels the purchase
        if (hint.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Purchase canceled.");
            hint.SetActive(false); // Hide hint
        }
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
        if (!string.IsNullOrEmpty(selectedItem) && Input.GetKeyDown(KeyCode.A))
        {
            SellItem(selectedItem);
        }
    }

    // Shows the "Press E to use" tooltip for the selected item
    void ShowUseTooltip(string item)
    {
        if (hint != null)
        {
            TextMeshProUGUI hintText = hint.GetComponent<TextMeshProUGUI>();

            if (hintText != null) // Ensure TextMeshPro component exists
            {
                // Make this more personalized when you figure out item names
                if (item == "cake")
                {
                    hintText.text = $"Press A to sell {item} for 70 dino dabloons or Q to cancel ";
                }
                else
                {
                    hintText.text = $"Press A to sell {item} or Q to cancel";
                }

                hint.SetActive(true); // Ensure hint is visible
            }
        }
    }



    // Handles item usage (removing it from inventory)
    void SellItem(string item)
    {
        //set selling prices
        int sellPrice;
        if (item == "cake")
        {
            sellPrice = 50;
        }
        else if(item =="item 1")
        {
            sellPrice = 500;
        } else
        {
            sellPrice = 5;
        }
        if (GameManager.instance.inventory.Contains(item))
        {
            Debug.Log($"Sold {item} for {sellPrice} dino dabloons!");
            //GameManager.instance.AddMoney(sellPrice); // Add money when selling
            References.MoneyDisplay.AddMoney(sellPrice);
            References.MoneyDisplay.UpdateMoneyUI();
            GameManager.instance.inventory.Remove(item); // Fix the incorrect inventory reference

            // Hide the tooltip
            if (hint != null)
            {
                hint.SetActive(false);
            }

            selectedItem = ""; // Clear selection
            ShowInventory(); // Refresh UI
        }
        else
        {
            Debug.LogError($"Item {item} not found in inventory!");
        }
    }

   
}

