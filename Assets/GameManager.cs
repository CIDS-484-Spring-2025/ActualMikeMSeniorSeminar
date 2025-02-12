using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public Vector2 playerPosition; // Store player position
    public string lastScene; // Store the last scene name
    public List<string> pickedUpObjects = new List<string>(); // Store picked-up objects

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerPosition(Vector2 position)
    {
        playerPosition = position;
        lastScene = SceneManager.GetActiveScene().name;
    }

    public void AddPickedUpObject(string objectName)
    {
        if (!pickedUpObjects.Contains(objectName)) // Avoid duplicates
        {
            pickedUpObjects.Add(objectName);
        }
    }

    public bool HasPickedUp(string objectName)
    {
        return pickedUpObjects.Contains(objectName);
    }
}
