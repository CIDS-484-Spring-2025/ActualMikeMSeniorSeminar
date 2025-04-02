using UnityEngine;

public class Walls : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            References.thePlayer.speed = 0;
        }

    }
}
