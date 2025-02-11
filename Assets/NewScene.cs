using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public string newScene; // Set in the Inspector
    private bool hasTriggered = false; // Track if triggered

    private void Awake()
    {
        // Check if this object was already triggered and disable it
        if (GameManager.instance != null && GameManager.instance.HasPickedUp(gameObject.name))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasTriggered && collision.gameObject.CompareTag("Player"))
        {
            hasTriggered = true; // Mark as triggered
            SavePlayerPosition(collision.gameObject);
            GameManager.instance.AddPickedUpObject(gameObject.name); // Save object state
            LoadScene();
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(newScene))
        {
            SceneManager.LoadScene(newScene);
        }
        else
        {
            Debug.LogError("Scene name is not set.");
        }
    }

    private void SavePlayerPosition(GameObject player)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SavePlayerPosition(player.transform.position);
        }
    }
}
