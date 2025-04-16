using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemygameManager : MonoBehaviour
{
    private int time;
    public GameObject attack;
    public GameObject information;
    public GameObject spare;
    public GameObject items;
    public GameObject attackPrefab;
    public GameObject secondAttack;
    public GameObject thirdAttack;
    public GameObject bigAttack;
    private float attackCooldown = 1f;
    private float nextAttackTime = 1.5f;
    private int numofattacks = 5;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        time = 0;
        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawnObject = GameObject.Find("boxaroundheart"); // change to your object's name

        if (player != null && spawnObject != null)
        {
            player.transform.position = spawnObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (time < numofattacks && Time.time >= nextAttackTime)

        {
           
            if (References.EnemyBehavior != null)
            {
                References.EnemyBehavior.Attack(); // or whatever you're calling
                time++;
                nextAttackTime = Time.time + attackCooldown;
            } 
           
            //time++;
            //nextAttackTime = Time.time + attackCooldown;
        }
        else if (GameObject.FindGameObjectsWithTag("attack").Length == 0 && References.EnemyBehavior.isDestroyed == true)
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
        //spare.SetActive(true);
        items.SetActive(true);
        if (inofbuttonscene.timesVisited >= 3)
        {
            spare.SetActive(true);
        }
    } 
  }

    
    
  

