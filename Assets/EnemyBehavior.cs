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
    private void Update()
    {
        if (References.EnemyBehavior == null)
        {
            return;
        }
    }
    public async void Attack()
    {
        if (attackPrefab == null || secondAttack == null || thirdAttack == null || bigAttack == null || References.EnemyBehavior == null)
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
            //int randomNumber = UnityEngine.Random.Range(0, 3);
            if (randomNumber == 0) {
                Vector3 attack1Offset = new Vector3(-1.4f, 0f, -1.4f); // Diagonal direction
                Quaternion attack1newRotation = Quaternion.LookRotation(attack1Offset); // Rotate to face that direction
                Vector3 attack1Offset1 = new Vector3(1.4f, 0f, -1.4f); // Diagonal direction
                Quaternion attack1newRotation1 = Quaternion.LookRotation(attack1Offset); // Rotate to face that direction
                Vector3 attack1Offset2 = new Vector3(-1.0f, 0f, -1.4f); // Diagonal direction
                Quaternion attack1newRotation2 = Quaternion.LookRotation(attack1Offset2); // Rotate to face that direction
                Vector3 attack1Offset3 = new Vector3(0f, 0f, -1.4f); // Diagonal direction
                Quaternion attack1newRotation3 = Quaternion.LookRotation(attack1Offset3); // Rotate to face that direction
                GameObject attack1newAttack = Instantiate(attackPrefab, transform.position + attack1Offset, attack1newRotation);
                await Task.Delay(500);
                GameObject attack1newAttack2 = Instantiate(attackPrefab, transform.position + attack1Offset2, attack1newRotation2);
                await Task.Delay(500);
                GameObject attack1newAttack3 = Instantiate(attackPrefab, transform.position + attack1Offset3, attack1newRotation3);
                await Task.Delay(500);
                GameObject attack1newAttack1 = Instantiate(attackPrefab, transform.position + attack1Offset1, attack1newRotation1);
            }
            else if (randomNumber == 1) {
            //attack 3
            Vector3 attack2Offset1 = new Vector3(2f, 0f, 0f); // Or whatever direction you want
            Vector3 attack2Offset2 = new Vector3(-2f, 0f, 0f);
            // Calculate angle based on x/y direction
            float attack2angle = Mathf.Atan2(attack2Offset1.y, attack2Offset1.x) * Mathf.Rad2Deg;
            float attack2angle1 = Mathf.Atan2(attack2Offset2.y, attack2Offset2.x) * Mathf.Rad2Deg;
            // Set rotation around Z (for 2D)
            Quaternion attack2newRotation = Quaternion.Euler(0f, 0f, attack2angle);
            Quaternion attack2newRotation1 = Quaternion.Euler(0f, 0f, attack2angle1);

            GameObject newAttack = Instantiate(secondAttack, transform.position + attack2Offset1, attack2newRotation);
            await Task.Delay(500); // Wait 3 seconds
            GameObject newAttack1 = Instantiate(secondAttack, transform.position + attack2Offset2, attack2newRotation1);
        }
        else
        {
            //attack 3
            Vector3 attack3Offset = new Vector3(-1.6f, 0f, 2f); // Diagonal direction
            Quaternion attack3newRotation = Quaternion.LookRotation(attack3Offset); // Rotate to face that direction
            Vector3 attack3Offset1 = new Vector3(1.6f, 0f, -2f); // Diagonal direction
            Quaternion attack3newRotation1 = Quaternion.LookRotation(attack3Offset1); // Rotate to face that direction
            Vector3 attack3Offset2 = new Vector3(-.5f, 0f, 2f); // Diagonal direction
            Quaternion attack3newRotation2 = Quaternion.LookRotation(attack3Offset2); // Rotate to face that direction
            Vector3 attack3Offset3 = new Vector3(.5f, 0f, 2f); // Diagonal direction
            Quaternion attack3newRotation3 = Quaternion.LookRotation(attack3Offset3);
            Vector3 bigattackOffset = new Vector3(-1.8f, 0f, 2f);
            Quaternion bigattacknewRotation = Quaternion.LookRotation(bigattackOffset);
            Vector3 bigattackOffset1 = new Vector3(1.8f, 0f, 2f);
            Quaternion bigattacknewRotation1 = Quaternion.LookRotation(bigattackOffset1);
            GameObject attack3newAttack = Instantiate(thirdAttack, transform.position + attack3Offset, attack3newRotation);
            await Task.Delay(500);
            GameObject attack3newAttack1 = Instantiate(thirdAttack, transform.position + attack3Offset1, attack3newRotation1);
            await Task.Delay(500);
            GameObject attack3newAttack2 = Instantiate(thirdAttack, transform.position + attack3Offset2, attack3newRotation2);
            await Task.Delay(500);
            GameObject attack3newAttack3 = Instantiate(thirdAttack, transform.position + attack3Offset3, attack3newRotation3);
            await Task.Delay(4500);
            GameObject bigattacknewAttack = Instantiate(bigAttack, transform.position + bigattackOffset, bigattacknewRotation);
            GameObject bigattacknewAttack1 = Instantiate(bigAttack, transform.position + bigattackOffset1, bigattacknewRotation1);


        }
       }

    }
}