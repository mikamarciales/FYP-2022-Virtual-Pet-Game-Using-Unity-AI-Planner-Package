using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public PetController pet;
    public NeedsController needsController;
    public float petMoveTimer, originalpetMoveTimer;
    public Transform[] waypoints;
    public static PetManager instance;

    private void Awake()
    {
        originalpetMoveTimer = petMoveTimer;
        if(instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one PetManager in the Scene");
    }

    private void Update()
    {
        if(petMoveTimer > 0)
        {
            petMoveTimer -= Time.deltaTime;
        }
        else
        {
            MovePetToRandomWaypoint();
            petMoveTimer = originalpetMoveTimer;
        }
    }

    private void MovePetToRandomWaypoint()
    {
        int randomWaypoint = Random.Range(0, waypoints.Length);
        pet.Move(waypoints[randomWaypoint].position);
    }

    public void MovePetToFood()
    {
        pet.Move(waypoints[11].position);
    }

    public void MovePetToDrink()
    {
        pet.Move(waypoints[12].position);
    }

    public void MovePetToHappiness()
    {
        pet.Move(waypoints[14].position);
    }

        public void MovePetToEnergy()
    {
        pet.Move(waypoints[13].position);
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}
