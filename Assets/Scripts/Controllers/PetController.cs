using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    public Animator petAnimator;
    private Vector3 destination;
    public float moveSpeed;

    private void Awake()
    {

    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, destination) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed*Time.deltaTime);
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

    public void Sad()
    {
        petAnimator.SetTrigger("Sad");
    }

    public void Hungry()
    {
        petAnimator.SetTrigger("Hungry");
    }

    public void Eat()
    {
        petAnimator.SetTrigger("Eat");
    }

    public void Thirsty()
    {
        petAnimator.SetTrigger("Thirsty");
    }

    public void Drink()
    {
        petAnimator.SetTrigger("Drink");
    }

    public void Tired()
    {
        petAnimator.SetTrigger("Tired");
    }
}
