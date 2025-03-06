using UnityEngine;

public class EnemygameManager : MonoBehaviour
{
    private int time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 1)
        {
            References.EnemyBehavior.Attack();
            time++;
        }
        else { 
            
        }
    }
}
