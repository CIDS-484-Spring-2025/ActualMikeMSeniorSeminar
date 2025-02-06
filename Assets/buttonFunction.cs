using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class buttonFunction : MonoBehaviour
{
    public string sceneToLoad; // Assign this in the Inspector

    void Start()
    {
        // Get the Button component and add a listener to it
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set in the Inspector!");
        }
    }
}
