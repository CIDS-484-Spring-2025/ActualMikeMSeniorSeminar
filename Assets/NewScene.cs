using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class NewScene : MonoBehaviour
{

    public string newScene;

    private void Awake()
    {
        References.NewScene = this;
    }

    void Start()
    {
        //SceneManager.LoadScene(firstLevelName);

    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Start Game");

    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Start Tutorial");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        LoadScene();
    }
    public void LoadScene()
    {
        // Make sure newScene is set
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
