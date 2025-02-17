using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Different damage values for different parts of the object

   



    private void Awake()
    {
        References.Attack = this;

    }

    // Detect collision with other objects (e.g., player)

    void OnTriggerEnter2D(Collider2D other)

    {

        // Check if the object that collided is a player or enemy

        if (other.CompareTag("Player"))

        {

            // Apply damage based on which collider the player hit

            if (gameObject.CompareTag("End"))

            {

                //ApplyDamage(minDamage);

            }

            else if (gameObject.CompareTag("middle"))

            {

                //ApplyDamage(maxDamage);

            }
            else
            {
                ApplyDamage(0);
            }
            //after attack got back to base screen
            //NewScene.LoadScene(enemy);

        }

    }


    public static void ApplyDamage(int damageAmount)

    {

        // Assuming the player has a method to take damage

        Debug.Log("Applying " + damageAmount + " damage.");

        // Example: playerHealth.TakeDamage(damageAmount);

    }

}