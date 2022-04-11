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
        if(petMoveTimer > 0 && FindObjectOfType<GameManager>().isGameOver == false)
        {
            petMoveTimer -= Time.deltaTime;
        }
        else if (petMoveTimer <= 0 && pet.petMoving == false && FindObjectOfType<GameManager>().isGameOver == false)
        {
            MovePetToRandomWaypoint();
            petMoveTimer = originalpetMoveTimer;
            Debug.Log("Food -10 Drink -7 Happiness -2 Energy -5");
        }
    }

    private void MovePetToRandomWaypoint()
    {
        int randomWaypoint = Random.Range(0, waypoints.Length);
        if(pet.petMoving == false
            && (waypoints[randomWaypoint].position != waypoints[11].position
            || waypoints[randomWaypoint].position != waypoints[12].position
            || waypoints[randomWaypoint].position != waypoints[13].position
            || waypoints[randomWaypoint].position != waypoints[14].position))
        {
            pet.Move(waypoints[randomWaypoint].position);
            pet.petMoving = false;
        }
    }

    public void MovePetToFood()
    {
        if(pet.petMoving == false)
        {
            pet.Move(waypoints[11].position);
            Debug.Log("Food +10");
            pet.petMoving = false;
            petMoveTimer = originalpetMoveTimer;
            pet.Eat();
        }
    }

    public void MovePetToDrink()
    {
        if(pet.petMoving == false)
        {
            pet.Move(waypoints[12].position);
            Debug.Log("Drink +7");
            pet.petMoving = false;
            petMoveTimer = originalpetMoveTimer;
            pet.Drink();
        }
    }

    public void MovePetToHappiness()
    {
        if(pet.petMoving == false)
        {
            pet.Move(waypoints[14].position);
            Debug.Log("Happiness +2");
            pet.petMoving = false;
            petMoveTimer = originalpetMoveTimer;
            pet.Happy();
        }
    }

        public void MovePetToEnergy()
    {
        if(pet.petMoving == false)
        {
            pet.Move(waypoints[13].position);
            Debug.Log("Energy +5");
            pet.petMoving = false;
            petMoveTimer = originalpetMoveTimer;
            pet.Tired();
        }
    }

    public void Die()
    {
        petMoveTimer = 0;
        pet.Sleep();
        FindObjectOfType<GameManager>().GameOver();
    }
}
