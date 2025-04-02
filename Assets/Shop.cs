using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public GameObject shopText;  // UI Text that says "Press Enter to Shop"
    public string shopScene;     // Name of the shop scene
    private bool isPlayerInShop = false;

    void Start()
    {
        // Ensure the text is hidden when the game starts
        if (shopText != null)
            shopText.SetActive(false);
    }

    void Update()
    {
        // Only allow entering the shop when the player is inside the trigger
        if (isPlayerInShop && Input.GetKeyDown(KeyCode.Return))  // Use Return instead of KeypadEnter
        {
            SceneManager.LoadScene(shopScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered shop trigger!"); // Check if this appears in Console
            if (shopText != null)
                shopText.SetActive(true);
            isPlayerInShop = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (shopText != null)
                shopText.SetActive(false); // Hide text when the player leaves
            isPlayerInShop = false;
        }
    }
}
