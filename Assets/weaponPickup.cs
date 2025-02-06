using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class weaponPickup : MonoBehaviour
{
    public GameObject primaryWeapon; // The currently equipped weapon
    public static List<GameObject> weaponInventory = new List<GameObject>(); // Store weapons
    public TextMeshProUGUI pickupNotification; // Assign in Inspector
    public TextMeshProUGUI switchNotification; // Assign a separate TextMeshPro for switch notifications

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            string weaponName = collision.gameObject.name;

            // Check if already picked up (prevents duplicates)
            if (!GameManager.instance.HasPickedUp(weaponName))
            {
                GameManager.instance.AddPickedUpObject(weaponName); // Save in GameManager
                weaponInventory.Add(collision.gameObject); // Add to local inventory
                collision.gameObject.SetActive(false); // Disable in the scene

                Debug.Log("Picked up: " + weaponName);
                ShowPickupMessage(weaponName);

                // Equip as primary weapon if none is equipped
                if (primaryWeapon == null)
                {
                    EquipWeapon(weaponInventory.Count - 1); // Equip last picked-up weapon
                }
            }
        }
    }


    void ShowPickupMessage(string weaponName)
    {
        if (pickupNotification != null)
        {
            pickupNotification.text = "Picked up: " + weaponName;
            StartCoroutine(FadeText(pickupNotification)); // Show pickup message
        }
    }

    void ShowSwitchMessage(string weaponName)
    {
        if (switchNotification != null)
        {
            switchNotification.text = "Switched to: " + weaponName;
            StartCoroutine(FadeText(switchNotification)); // Show switch message
        }
    }

    IEnumerator FadeText(TextMeshProUGUI notificationText)
    {
        notificationText.alpha = 1;
        yield return new WaitForSeconds(2f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            notificationText.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        notificationText.alpha = 0;
    }

    void Update()
    {
        // Right-click to switch weapons
        if (weaponInventory.Count > 1)
        {
            if (Input.GetMouseButtonDown(1)) // Right mouse click (Button 1)
            {
                CycleWeapon(1); // Switch to next weapon
            }
        }
    }

    void EquipWeapon(int index)
    {
        primaryWeapon = weaponInventory[index];
        Debug.Log("Equipped: " + primaryWeapon.name);
    }

    void CycleWeapon(int direction)
    {
        int currentIndex = weaponInventory.IndexOf(primaryWeapon);
        int newIndex = (currentIndex + direction + weaponInventory.Count) % weaponInventory.Count;
        EquipWeapon(newIndex);

        // Show switch notification
        ShowSwitchMessage(weaponInventory[newIndex].name);
    }
}