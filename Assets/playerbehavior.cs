using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour, IDataPersistence
{

    //Never set the value of a public variable here - the inspector will override it without telling you.
    //If you need to, set it in Start() instead

    public float speed; //'float' is short for floating point number, which is basically just a normal number
    private Rigidbody2D ourRigidBody;
    public int health = 10;
  

    private void Awake()
    {

        References.thePlayer = this;
        ourRigidBody = GetComponent<Rigidbody2D>();
        //transform.position = Vector3.zero;


    }

   
    void Start()
    {
        // Load saved health when the scene starts
        /*if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            health = PlayerPrefs.GetInt("PlayerHealth");
        }*/
        if (GameManager.instance != null && GameManager.instance.lastScene == SceneManager.GetActiveScene().name)
        {
            transform.position = GameManager.instance.playerPosition; // Restore saved position
            Debug.Log("Loaded Player Position: " + transform.position);
        }
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage. Health now: {health}");

        // Ensure UI updates when health changes
        References.HealthDisplay.UpdateHealthDisplay();

        // Check if player died
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player has died!");
        // Add death logic (respawn, game over, etc.)
    }
    public void LoadData(GameData data)
    {

        this.transform.position = data.playerPosition;
        

    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

        //WASD to move
        // Get horizontal input and create the input vector
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        // Get the Rigidbody2D component
        //Rigidbody2D ourRigidBody = GetComponent<Rigidbody2D>();

        // Set the velocity (movement) based on input
        ourRigidBody.linearVelocity = inputVector * speed;

        // If the input vector is not zero, rotate the object to face the direction of movement
        if (inputVector != Vector2.zero)
        {
            //calculates the angle (in degrees) based on the inputVector
            //Atan2(y, x) gives the angle in radians, and Rad2Deg converts it to degree
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            //rotates the object to face that direction
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

   

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with a specific object, like an obstacle
        if (collision.gameObject.CompareTag("boxForHeart"))
        {
            // Stop the player's movement by setting the velocity to zero
            ourRigidBody.linearVelocity = Vector2.zero;
        }
    }
}