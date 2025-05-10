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


    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(newScene))
        {
            CheckScene();
            SceneManager.LoadScene(newScene);
        }

        else
        {
            Debug.LogError("Scene name is not set.");
        }
    }

    public void CheckScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log(currentScene);
        if (SceneManager.GetActiveScene().name == "grasslands")
        {
            Debug.Log("grasslands");
            References.GameManager.level++;
            //DataPersistenceManager.instance.SaveGame();
        }
        if (SceneManager.GetActiveScene().name == "jungle")
        {
            Debug.Log("jungle");
            References.GameManager.level++;
            
        }
    }
    
}

