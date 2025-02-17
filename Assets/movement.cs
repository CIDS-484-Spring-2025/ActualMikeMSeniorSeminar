using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;
    public string sceneToLoad;
    public float moveSpeed = 5f; // Movement speed
    private bool canMove = true; // Flag to control movement

    void Update()
    {
        if (canMove)
        {
            // Move right along the X-axis
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }

        // Stop movement when left mouse button is clicked
        if (Input.GetMouseButtonDown(0)) // 0 = Left Mouse Button
        {
            canMove = false; // Stops movement
            CheckBelow();


        }
    }
    void CheckBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);

        if (hit.collider != null)
        {
            string hitObject = hit.collider.gameObject.name;
            Debug.Log("Object below: " + hitObject);
            if (hit.collider.CompareTag("End"))
            {
                Debug.Log("Object below: woooooow");
                Attack.ApplyDamage(minDamage);
            } else if (hit.collider.CompareTag("Middle"))
            {
                Debug.Log("Object below: crit");
                Attack.ApplyDamage(maxDamage);
            }
            else { }
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Nothing below!");
        }
    }
    
}

