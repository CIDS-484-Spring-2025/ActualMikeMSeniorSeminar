using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasBehavior : MonoBehaviour
{
    public SceneAsset firstScene;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject inventoryMenu;
    public GameObject currentMenu;
    public GameObject GoToMainMenu;

    

    // Start is called before the first frame update
    void Awake()
    {
        References.CanvasBehavior = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Opens menu if space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentMenu == mainMenu)
            {
                HideMenu();
            }
            else
            {
                ShowMenu(mainMenu);
            }
        }
    }

    public void ShowMainMenu()
    {
        ShowMenu(mainMenu);
    }

    public void HideMenu()
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
        currentMenu = null;
        Time.timeScale = 1;
    }

    public void ShowMenu(GameObject menuToShow)
    {
        HideMenu();
        currentMenu = menuToShow;
        if (menuToShow != null)
        {
            menuToShow.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartNewGame()
    {
        HideMenu();
        SceneManager.LoadScene(firstScene.name);
    }

    // New method to save data before quitting
    public void SaveAndQuit()
    {
        // Save the game before quitting
        DataPersistenceManager.instance.SaveGame();

        // Quit the application
        Application.Quit();

        // If running in the Unity Editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Quit()
    {
        // Call SaveAndQuit before quitting
        SaveAndQuit();
    }

    public void showInventory()
    {
        // To a different menu to show inventory
        References.InventoryDisplay.ShowInventory();
    }

   
}
