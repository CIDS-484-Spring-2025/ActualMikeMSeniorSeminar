using UnityEngine;

public class EnemygameManager : MonoBehaviour
{
    private int time;
    public GameObject attack;
    public GameObject information;
    public GameObject spare;
    public GameObject items;
    public GameObject attackPrefab;
    private float attackCooldown = 2f;
    private float nextAttackTime = 3;
    private int numofattacks = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (time < 3)
        {
            References.EnemyBehavior.Attack();
            time++;
        }
        else
        {
            if (References.EnemyBehavior.isDestroyed == true)
            {
                ActivateUIElements();
            }
        }*/

        if (time < numofattacks && Time.time >= nextAttackTime)
        {
            References.EnemyBehavior.Attack();
            time++;
            nextAttackTime = Time.time + attackCooldown;
        }
        else if(GameObject.FindGameObjectsWithTag("attack").Length == 0 &&  References.EnemyBehavior.isDestroyed == true)
        {
            ActivateUIElements();
        }
        else
        {

        }

    }

    void ActivateUIElements()
    {
        attack.SetActive(true);
        information.SetActive(true);
        spare.SetActive(true);
        items.SetActive(true);
    }
}
    
    
  

