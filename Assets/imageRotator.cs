using UnityEngine;

public class imageRotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // degrees per second
    int time = 0;
    public GameObject firstPic;
    public GameObject scaryPic;
    public GameObject scaryPic1;
    public GameObject scaryPic2;
    public GameObject scaryPic3;
    void Update()
    {
        
        transform.Rotate(rotationSpeed * Time.deltaTime);
        
        if(time > 4000)
        {
            firstPic.SetActive(false);
            scaryPic.SetActive(true);
            scaryPic1.SetActive(true);
            scaryPic2.SetActive(true);
            scaryPic3.SetActive(true);
        }
        //Debug.Log(time);
        time++;
    }
}
