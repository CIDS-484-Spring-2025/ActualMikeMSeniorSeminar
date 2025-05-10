using System;
using System.Threading.Tasks;
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
    public GameObject secondAttack;
    public GameObject thirdAttack;
    public GameObject bigAttack;
    public Boolean isDestroyed = false;
    private int randomNumber;
    private bool isAttacking = false; // Prevents overlapping attacks
    private bool attackisDestroyed = false; // Tracks if the object is destroyed

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
        randomNumber = UnityEngine.Random.Range(0, 3);
        Debug.Log(randomNumber);
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
    void Update()
    {
        /*if (References.EnemyBehavior == null)
        {
            return;
        }*/
      
        if (References.thePlayer.health <= 0)
        {
            return;
        }
    }
    
    public async void Attack()
    {
        if (isAttacking || attackisDestroyed) return; // Block if already attacking or destroyed

        isAttacking = true; // Set attacking flag to true

        switch (randomNumber)
        {
            case 0:
                Debug.Log("Using attack pattern 1");
                await AttackPattern1();
                break;
            case 1:
                Debug.Log("Using attack pattern 2");
                await AttackPattern2();
                break;
            default:
                Debug.Log("Using attack pattern 3");
                await AttackPattern3();
                break;
        }

        isAttacking = false; // Reset attacking flag after the attack is done
    }

    private async Task AttackPattern1()
    {
        Vector3[] offsets = {
            new Vector3(-1.4f, 0f, -1.4f),
            new Vector3(-1.0f, 0f, -1.4f),
            new Vector3(0f, 0f, -1.4f),
            new Vector3(1.4f, 0f, -1.4f)
        };

        foreach (var offset in offsets)
        {

            if (References.thePlayer.health <= 0)
            {
                return;
            } // Stop if destroyed

            Quaternion rotation = Quaternion.LookRotation(offset);
            Instantiate(attackPrefab, transform.position + offset, rotation);
            await Task.Delay(200); // Delay between attacks
        }
    }

    private async Task AttackPattern2()
    {
        Vector3[] offsets = {
            new Vector3(2f, 0f, 0f),
            new Vector3(-2f, 0f, 0f)
        };

        foreach (var offset in offsets)
        {

            if (References.thePlayer.health <= 0)
            {
                return;
            } // Stop if destroyed

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(secondAttack, transform.position + offset, rotation);
            await Task.Delay(300); // Delay between attacks
        }
    }

    private async Task AttackPattern3()
    {
        Vector3[] thirdOffsets = {
            new Vector3(-1.6f, 0f, 2f),
            new Vector3(1.6f, 0f, -2f),
            new Vector3(-.5f, 0f, 2f),
            new Vector3(.5f, 0f, 2f)
        };

        foreach (var offset in thirdOffsets)
        {

            if (References.thePlayer.health <= 0)
            {
                return;
            } // Stop if destroyed

            Quaternion rotation = Quaternion.LookRotation(offset);
            Instantiate(thirdAttack, transform.position + offset, rotation);
            await Task.Delay(300); // Delay between attacks
        }

        await Task.Delay(400); // Delay before big attacks


        if (References.thePlayer.health <= 0)
        {
            return;
        } // Check again if destroyed before proceeding

        Vector3[] bigOffsets = {
            new Vector3(-1.8f, 0f, 2f),
            new Vector3(1.8f, 0f, 2f)
        };

        foreach (var offset in bigOffsets)
        {

            if (References.thePlayer.health <= 0)
            {
                return;
            } // Stop if destroyed

            Quaternion rotation = Quaternion.LookRotation(offset);
            Instantiate(bigAttack, transform.position + offset, rotation);
        }
    }

    // Call this function to set the object as destroyed
    public void DestroyObject()
    {
        attackisDestroyed = true; // Mark as destroyed
        isAttacking = false; // Ensure no more attacks can happen
    }

    
}