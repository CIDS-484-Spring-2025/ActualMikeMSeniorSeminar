/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followplayer : MonoBehaviour
{

    Vector3 normalPosition;
    Vector3 desiredPosition;

    public Vector3 joltVector;
    public float shakeAmount;

    public float joltDecayFactor;
    public float shakeDecayFactor;

    public float maxMoveSpeed;

    public Vector3 cameraOffset;


    private void Awake()
    {
        References.Followplayer = this;
    }

    void Start()
    {
        normalPosition = transform.position;
        //Store our position relative to the player
        cameraOffset = transform.position - References.thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (References.thePlayer != null)
        {
            //Set our position by looking at the player's position, and adding our offset
            normalPosition = References.thePlayer.transform.position + cameraOffset;
        }

        Vector3 shakeVector = new Vector3(GetRandomShakeAmount(), GetRandomShakeAmount(), GetRandomShakeAmount());
        desiredPosition = normalPosition + joltVector + shakeVector;

        //Set our position to the jolted position
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, maxMoveSpeed * Time.deltaTime);

        //Jolt vector decreases
        joltVector *= joltDecayFactor;
        shakeAmount *= shakeDecayFactor;

    }


    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }

}*/
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // The player object
    private Vector3 offset; // The distance between the camera and player
    public float smoothSpeed = 0.9f; // Smoothness of camera movement
    public Vector3 cameraOffset; // Custom offset for positioning the camera
    private Quaternion fixedRotation; // Keeps the fixed rotation of the camera


   

    private void Start()
    {
        if (References.thePlayer != null)
        {
            player = References.thePlayer.transform; // Set the player reference if it's not set
            cameraOffset = new Vector3(0, 0, -10); // Adjust the default offset
            offset = transform.position - player.position; // Initial offset based on current positions
            fixedRotation = transform.rotation; // Store the initial camera rotation
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Keep the camera in the desired position relative to the player
            Vector3 desiredPosition = player.position + cameraOffset;

            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Update the camera position
            transform.position = smoothedPosition;

            // Keep the camera’s rotation fixed (no tilt or rotation)
            transform.rotation = fixedRotation;
        }
    }
}
