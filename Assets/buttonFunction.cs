using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    public string sceneToLoad; // Assign this in the Inspector

    void Start()
    {
        // Add button click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => ChangeScene(sceneToLoad));
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is not set in the Inspector!");
            return;
        }
        
        SceneManager.LoadScene(sceneName);
    }
}

