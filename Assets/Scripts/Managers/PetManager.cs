using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public PetController pet;
    public float petMoveTimer, originalpetMoveTimer;
    public Transform[] waypoints;

    private void Awake()
    {
        originalpetMoveTimer = petMoveTimer;
    }

    private void Update()
    {
        if(petMoveTimer > 0)
        {
            petMoveTimer -= Time.deltaTime;
        }
        else{
            MovePetToRandomWaypoint();
            petMoveTimer = originalpetMoveTimer;
        }
    }

    private void MovePetToRandomWaypoint()
    {
        int randomWaypoint = Random.Range(0,waypoints.Length);
        //Debug.Log("randomWaypoint = " + randomWaypoint);
        //Debug.Log("waypoints[randomWaypoint].position = " + waypoints[randomWaypoint].position);
        if(randomWaypoint >= 0 && randomWaypoint <= waypoints.Length)
        {
            pet.Move(waypoints[randomWaypoint].position);
        }
        else
        {
            pet.Move(waypoints[0].position);
        }
    }

    public static void Die()
    {

    }
}
