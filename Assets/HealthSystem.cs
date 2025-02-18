using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void Awake()
    {
        References.HealthSystem = this;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //increasehealth function
    public void addPlayerHealth()
    {
        // figure out max health stuff
        //if(References.thePlayer.health = maxHealth)
        References.thePlayer.health += 10;
    }
    public static void LowerPlayerHealth()
    {
        if (References.thePlayer.health < 0)
        {
            References.thePlayer.health = 0;
            //load to last save point
        }
        else 
        { 
        References.thePlayer.health -= 10;
        }
    }
    void LowerEnemyHealth()
    {
        //if health lower than zero go to the scene it was previous at
        // lower enemy health by like 50 or something 
        //References.thePlayer.health -= 10;
    }
}
