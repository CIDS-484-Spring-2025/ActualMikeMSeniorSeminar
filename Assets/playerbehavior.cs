using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{

    //Never set the value of a public variable here - the inspector will override it without telling you.
    //If you need to, set it in Start() instead

    public float speed; //'float' is short for floating point number, which is basically just a normal number
    private Rigidbody2D ourRigidBody;

    private void Awake()
    {
        References.thePlayer = this;
        ourRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        if (GameManager.instance != null && GameManager.instance.lastScene == SceneManager.GetActiveScene().name)
        {
            transform.position = GameManager.instance.playerPosition; // Restore saved position
        }
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
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        //Click to fire
        //If clicked, create a bullet at our current position
        //if (Input.GetButton("Fire1"))
        //{
        //Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
        //}

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