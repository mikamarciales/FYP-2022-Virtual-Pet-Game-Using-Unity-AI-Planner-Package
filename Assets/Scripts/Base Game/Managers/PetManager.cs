// PetManager class created to be used in the base game
// Updates timer and facilitates random movement; moving pet methods defined in this class
// Structure based on the Education Ecosystem's 'How To Create A Virtual Pet Game In Unity' tutorial: https://www.youtube.com/watch?v=MAK2Qzu0j40&list=PLQbzkJk10-f6mK42ieiFoU6YGQN7BM_nE&ab_channel=EducationEcosystem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualPetGame
{
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
            if (instance == null)
            {
                instance = this;
            }
            else Debug.LogWarning("More than one PetManager in the Scene");
        }

        private void Update()
        {
            if (petMoveTimer > 0 && FindObjectOfType<GameManager>().isGameOver == false)
            {
                petMoveTimer -= Time.deltaTime;
            }
            else if (petMoveTimer <= 0 && FindObjectOfType<GameManager>().isGameOver == false)
            {
                //MovePetToRandomWaypoint();
                petMoveTimer = originalpetMoveTimer;
                Debug.Log("10s passed! Needs deteriorated! (Food -10 Drink -7 Happiness -2 Energy -5)");
            }
        }

        private void MovePetToRandomWaypoint()
        {
            int randomWaypoint = Random.Range(0, waypoints.Length);
            if (waypoints[randomWaypoint].position != waypoints[11].position
                || waypoints[randomWaypoint].position != waypoints[12].position
                || waypoints[randomWaypoint].position != waypoints[13].position
                || waypoints[randomWaypoint].position != waypoints[14].position)
            {
                pet.Move(waypoints[randomWaypoint].position);
            }
        }

        public void MovePetToFood()
        {
            pet.Move(waypoints[11].position);
            petMoveTimer = originalpetMoveTimer;
            pet.Eat();
        }

        public void MovePetToDrink()
        {
            pet.Move(waypoints[12].position);
            petMoveTimer = originalpetMoveTimer;
            pet.Drink();
        }

        public void MovePetToHappiness()
        {
            pet.Move(waypoints[14].position);
            petMoveTimer = originalpetMoveTimer;
            pet.Happy();
        }

        public void MovePetToEnergy()
        {
            pet.Move(waypoints[13].position);
            petMoveTimer = originalpetMoveTimer;
            pet.Tired();
        }

        public void Die()
        {
            petMoveTimer = 0;
            pet.Sleep();
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}