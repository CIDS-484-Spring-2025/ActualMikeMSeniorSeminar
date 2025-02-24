using TMPro;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI EnemyHealthText; // Assign in Inspector
    public EnemyBehavior enemy; // Assign in Inspector

    private void Start()
    {
        if (enemy == null)
        {
            enemy = FindObjectOfType<EnemyBehavior>(); // Auto-assign if not set
        }

        UpdateHealthDisplay(); // Display health when scene loads
    }

    public void UpdateHealthDisplay()
    {
        if (EnemyHealthText == null)
        {
            Debug.LogError("EnemyHealthText is NULL! Assign the UI Text in Inspector.");
            return;
        }

        if (enemy == null)
        {
            Debug.LogError("EnemyBehavior is NULL! Make sure the enemy exists in the scene.");
            return;
        }

        EnemyHealthText.text = "Enemy Health: " + enemy.enemyHealth.ToString();
    }
}