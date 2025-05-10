using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI moneyText;
    public int money;

    private void Awake()
    {
        // Ensure only one MoneyDisplay instance exists across scenes
        if (References.MoneyDisplay == null)
        {
            References.MoneyDisplay = this;
            DontDestroyOnLoad(gameObject); // Keep it across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate UI objects
        }
    }

    // Add money to the current amount and update the UI
    public void AddMoney(int amount)
    {
        money += amount; // Increase the money by the specified amount
        DataPersistenceManager.instance.SaveGame(); // Save the updated data
        UpdateMoneyUI(); // Update the UI to reflect the new amount
        Debug.Log($"Dino Dabloons added: {amount}. Total: {money}");
    }

    // Attempt to spend money, if sufficient funds exist
    public bool SpendMoney(int cost)
    {
        // Check if the player has enough money to spend
        Debug.Log($"Attempting to spend {cost} dino dabloons. Current money: {money}");

        if (money >= cost)
        {
            money -= cost; // Subtract the cost from the total
            DataPersistenceManager.instance.SaveGame(); // Save the updated data
            UpdateMoneyUI(); // Update the UI after spending
            Debug.Log($"Spent {cost} dino dabloons. Remaining: {money}");

            return true; // Return true if the transaction was successful
        }

        // If not enough money, log a failure
        //add message for this in buy shop
        Debug.Log("Not enough money to spend!");
        return false; // Return false indicating failure
    }

    // Update the UI text to reflect the current amount of money
    public void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Dino Dabloons: {money}";
        }
        else
        {
            Debug.LogError("Money text UI is missing!");
        }
    }

    // This method is used when loading game data
    public void LoadData(GameData data)
    {
        this.money = data.dinoDabloons; // Load the saved money from data
        UpdateMoneyUI(); // Update the UI with the loaded money
    }

    // This method is used when saving game data
    public void SaveData(GameData data)
    {
        Debug.Log("Saving dabloons: " + money);
        data.dinoDabloons = this.money; // Save the current money to game data
    }
}


