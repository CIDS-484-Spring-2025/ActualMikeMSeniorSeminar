using System.Collections;
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

}