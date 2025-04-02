using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using static UnityEditor.Progress;

public class ObjectPickup : MonoBehaviour
{
    public static List<string> inventory = new List<string>(); // Store inventory
    public TextMeshProUGUI pickupNotification; // Drag UI Text here in Inspector

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            string itemName = collision.gameObject.name;

            // Prevent duplicate pickups
            if (!GameManager.instance.HasPickedUp(itemName))
            {
                
                GameManager.instance.AddPickedUpObject(itemName); // Save item in GameManager
                inventory.Add(itemName); // Add to inventory
                if (itemName != "item1" || itemName != "item2" || itemName != "item3")
                {
                    WeaponRespawn.CollectItemByName(itemName);
                    collision.gameObject.SetActive(false); // Hide object in scene

                    Debug.Log("Picked up: " + itemName);
                    ShowPickupMessage(itemName); // Show notification
                }
            }
        }
    }

    void ShowPickupMessage(string itemName)
    {
        if (pickupNotification != null)
        {
            pickupNotification.text = "Picked up: " + itemName;
            StartCoroutine(FadeText());
        }
    }

    IEnumerator FadeText()
    {
        //same thing that i used weapons
        pickupNotification.alpha = 1; // Make visible
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Fade out effect
        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            pickupNotification.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pickupNotification.alpha = 0; // Fully invisible
    }
}