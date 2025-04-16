using UnityEngine;

public class inofbuttonscene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject firstMessage;
    public GameObject secondMessage;    
    public GameObject thirdMessage;
    public static int timesVisited = 0;

    private void Awake()
    {

        References.inofbuttonscene = this;
  

    }
    void Start()
    {
        timesVisited++;
    }

    // Update is called once per frame
    void Update()
    {
        if (timesVisited == 1)
        {
            firstMessage.SetActive(true);
        }
        else if (timesVisited == 2)
        {
            firstMessage.SetActive(true);
            secondMessage.SetActive(true);
        }
        else if (timesVisited >= 3)
        {
            firstMessage.SetActive(true);
            secondMessage.SetActive(true);
            thirdMessage.SetActive(true);
        }
    }
 }
