using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEditor.Progress;
using static UnityEditorInternal.ReorderableList;
using System.Xml.Linq;

public class BuyItem : MonoBehaviour
{
    public GameObject buyconfirm; // Confirmation panel
    public TextMeshProUGUI confirmText; // Confirmation text
    private bool isBuying = false;
    private string currentItemName = ""; // Name of the current item

    private static BuyItem activeBuyItem = null; // Track active UI

    public RectTransform cursor; // Cursor reference
    public List<Button> buttons; // List of buttons to navigate

    void Update()
    {
        CleanupDestroyedButtons(); // Remove null buttons from the list

        Button selectedButton = GetClosestButtonToCursor();

        if (selectedButton != null)
        {
            CheckIfBuying(selectedButton);
        }
    }
    

    public void CheckIfBuying(Button selectedButton)
    {
        if (activeBuyItem != null && activeBuyItem != this)
        {
            return; // Prevent multiple UIs
        }

        if (!isBuying && Input.GetKeyDown(KeyCode.A))

        {

            
                currentItemName = selectedButton.gameObject.name;
                Debug.Log($"Buying started for: {currentItemName}");

                buyconfirm.SetActive(true);

                //when maing actual items chnage these if statements
                if (currentItemName == "item 1")
                {
                    confirmText.text = $"Are you sure you want to buy {currentItemName} for 5 dino dabloons or press E to cancel?";
                }
                else if (currentItemName == "item 2")
                {
                    confirmText.text = $"Are you sure you want to buy {currentItemName} for 10 dino dabloons or press E to cancel?";
                }
                else if (currentItemName == "item 3")
                {
                    confirmText.text = $"Are you sure you want to buy {currentItemName} for 15 dino dabloons or press E to cancel?";
                }

                isBuying = true;
                activeBuyItem = this;
        }
        else if (isBuying && Input.GetKeyDown(KeyCode.A))
        {
            int cost = GetItemCost(currentItemName); // Get the cost of the selected item
            if (References.MoneyDisplay.SpendMoney(cost))
            //GameManager.instance.SpendMoney(cost)) // Check if player has enough money old version
            {
                Debug.Log($"Confirmed purchase: {currentItemName}");
                GameManager.instance.AddPickedUpObject(currentItemName);
                References.MoneyDisplay.UpdateMoneyUI();
            }
        


            buyconfirm.SetActive(false);
            isBuying = false;
            activeBuyItem = null;

            // Remove the button from the list and destroy it
            RemoveButton(selectedButton);
        }

        if (isBuying && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Purchase canceled.");
            buyconfirm.SetActive(false);
            isBuying = false;
            activeBuyItem = null;
        }
    }

    private int GetItemCost(string itemName)
    {
        if (itemName == "item 1")
        {
            return 5;
        }
        else if (itemName == "item 2")
        {
            return 10;
        }
        else if (itemName == "item 3")
        {
            return 15;
        }
        else
        {
            return 0;
        }//Default cost if item name doesn't match
     }
    // Find the closest button to the cursor, skipping destroyed buttons
    private Button GetClosestButtonToCursor()
    {
        Button closestButton = null;
        float minDistance = float.MaxValue;

        foreach (Button button in buttons)
        {
            if (button == null) continue; // Skip destroyed buttons

            float distance = Vector2.Distance(cursor.position, button.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestButton = button;
            }
        }

        return closestButton;
    }

    // Remove the button from the list if it's destroyed
    private void RemoveButton(Button button)
    {
        if (buttons.Contains(button))
        {
            buttons.Remove(button);
            Destroy(button.gameObject);
        }
    }

    // Clean up null buttons from the list
    private void CleanupDestroyedButtons()
    {
        buttons.RemoveAll(button => button == null);
    }
}
