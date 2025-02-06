using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{

    public static PlayerBehaviour thePlayer;
    public static NewScene NewScene;

    public static Followplayer Followplayer;
    
    public static float maxDistanceInALevel = 1000;


    public static LayerMask wallsLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");

}
