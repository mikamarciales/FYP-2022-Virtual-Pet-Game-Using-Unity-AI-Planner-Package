// PetController class created to be used in the base game and AI Planner
// Defines how the pet moves and animation triggers for pet's moods and actions
// Structure based on the Education Ecosystem's 'How To Create A Virtual Pet Game In Unity' tutorial: https://www.youtube.com/watch?v=MAK2Qzu0j40&list=PLQbzkJk10-f6mK42ieiFoU6YGQN7BM_nE&ab_channel=EducationEcosystem

using System.Collections;
using System.Collections.Generic;
using Unity.AI.Planner;
using UnityEngine;

namespace VirtualPetGame
{
    public class PetController : MonoBehaviour
    {
        public Animator petAnimator;
        private Vector3 destination;
        public float moveSpeed;

        private void Update()
        {
            if (Vector3.Distance(transform.position, destination) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            }
        }

        public void Move(Vector3 destination)
        {
            this.destination = destination;
        }

        public void Happy()
        {
            petAnimator.SetTrigger("Happy");
        }

        public void Eat()
        {
            petAnimator.SetTrigger("Eat");
        }

        public void Drink()
        {
            petAnimator.SetTrigger("Drink");
        }

        public void Tired()
        {
            petAnimator.SetTrigger("Tired");
        }

        public void Sleep()
        {
            petAnimator.SetTrigger("Sleep");
        }
    }
}
