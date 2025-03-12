using System;
using UnityEngine;

public class EnemyAttackBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed; // Speed of the bullet
    private Rigidbody2D AttackRigidBody;
    
       void Start()
    {
        //AttackCollider = GetComponent<Collider>();
        AttackRigidBody = GetComponent<Rigidbody2D>();
        AttackRigidBody.linearVelocity = Vector2.down * speed; // Moves the bullet straight down
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // Example: Ignore collision between the bullet and the player
        if (other.gameObject.CompareTag("Player")) // Change "Player" to your desired tag
        {
            References.thePlayer.health -= 1;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("destroyer"))
        {
            Destroy(gameObject); // Destroy this object when the player enters the trigger zone
            References.EnemyBehavior.isDestroyed = true;
        }
        
    }

}
    
