using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    
    public int enemyHealth;
    public int maxHealth;
    public string previousScene;
    public EnemyHealthDisplay enemyHealthDisplay; // Reference to the health display script
    public GameObject attackPrefab;
    public Boolean isDestroyed = false;

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
    public void Attack()
    {
        if (attackPrefab == null)
        {
            Debug.LogError("attackPrefab is not assigned in the Inspector!");
            return;
        }

        //Vector3 attackOffset = new Vector3(1f, 0f, 0f);
        //GameObject newAttack = Instantiate(attackPrefab, transform.position + attackOffset, transform.rotation);
        if(isDestroyed = true)
        {
            isDestroyed = false;
        }
        if (isDestroyed == false)
        {
            //attack type 1
            //Vector3 attackOffset = new Vector3(1f, 0f, 0f);
            //GameObject newAttack = Instantiate(attackPrefab, transform.position + attackOffset, transform.rotation);
            Vector3 attackOffset = new Vector3(0f, 0f, 2f);
            Quaternion newRotation = Quaternion.Euler(0f, 0f, 90f); // Rotates 90 degrees on the Z-axis
            GameObject newAttack = Instantiate(attackPrefab, transform.position + attackOffset, newRotation);

        }
    }
            
}