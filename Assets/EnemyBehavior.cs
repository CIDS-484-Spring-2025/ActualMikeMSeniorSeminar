using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public int enemyHealth;
    public int maxHealth;
    public string previousScene;
    public EnemyHealthDisplay enemyHealthDisplay; // Reference to the health display script

    private void Awake()
    {
        References.EnemyBehavior = this;

        // Load saved health if it exists
        if (PlayerPrefs.HasKey("EnemyHealth"))
        {
            enemyHealth = PlayerPrefs.GetInt("EnemyHealth");
        }
    }

    private void Start()
    {
        maxHealth = enemyHealth;
        // Ensure the health display updates when the scene loads
        if (enemyHealthDisplay == null)
        {
            enemyHealthDisplay = FindObjectOfType<EnemyHealthDisplay>(); // Auto-assign if not set
        }

        if (enemyHealthDisplay != null)
        {
            enemyHealthDisplay.UpdateHealthDisplay();
        }
        else
        {
            Debug.LogError("EnemyHealthDisplay not found! Make sure it's in the scene.");
        }
    }

    public void TakeDamage(int playerAttack)
    {
        enemyHealth -= playerAttack;
        Debug.Log("Enemy health: " + enemyHealth);

        // Save the current enemy health before reloading
        PlayerPrefs.SetInt("EnemyHealth", enemyHealth);
        PlayerPrefs.Save();

        // Update health display after taking damage
        if (enemyHealthDisplay != null)
        {
            enemyHealthDisplay.UpdateHealthDisplay();
        }

        if (enemyHealth <= 0)
        {
            Debug.Log("Enemy is dead.");
            PlayerPrefs.DeleteKey("EnemyHealth"); // Reset saved health on death
            SceneManager.LoadScene(previousScene);
        }
    }
}