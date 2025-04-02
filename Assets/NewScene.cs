using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public string newScene; // Set this in the Inspector
    
    private void Awake()
    {
        // If this object was already interacted with, disable it
        if (GameManager.instance != null && GameManager.instance.HasPickedUp(gameObject.name))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                // Save player's position once
                GameManager.instance.SavePlayerPosition(collision.transform.position);
                Debug.Log("Player position saved at: " + collision.transform.position);

                // Prevent object from triggering again
                GameManager.instance.AddPickedUpObject(gameObject.name);
               
            }


            LoadScene();
            
        }
    }


    private void LoadScene()
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
}

