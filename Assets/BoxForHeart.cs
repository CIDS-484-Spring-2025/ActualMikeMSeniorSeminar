using System.Security.Cryptography;
using UnityEngine;

public class BoxForHeart : MonoBehaviour
{
    //private Collider BoxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //BoxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            References.thePlayer.speed = 0;
        }

    }
}
