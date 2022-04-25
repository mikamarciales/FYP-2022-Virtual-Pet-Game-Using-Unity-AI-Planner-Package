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
        //public bool petMoving = false;

        private void Update()
        {
            if(Vector3.Distance(transform.position, destination) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed*Time.deltaTime);
            }
        }

        public void Move(Vector3 destination)
        {
            //petMoving = true;
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