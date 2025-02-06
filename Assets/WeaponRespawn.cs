using UnityEngine;

public class WeaponRespawn : MonoBehaviour
{
    void Start()
    {
        if (GameManager.instance.HasPickedUp(gameObject.name))
        {
            gameObject.SetActive(false); // Hide if already picked up
        }
    }
}