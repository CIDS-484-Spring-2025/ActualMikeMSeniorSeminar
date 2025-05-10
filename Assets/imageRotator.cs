using UnityEngine;

public class imageRotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0); // degrees per second
    public GameObject firstPic;
    public GameObject scaryPic;
    public GameObject scaryPic1;
    public GameObject scaryPic2;
    public GameObject scaryPic3;

    private float timer = 0f;
    public float showScaryImagesAfter = 10f; // time in seconds

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer > showScaryImagesAfter)
        {
            firstPic.SetActive(false);
            scaryPic.SetActive(true);
            scaryPic1.SetActive(true);
            scaryPic2.SetActive(true);
            scaryPic3.SetActive(true);
        }
    }
}

