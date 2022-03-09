using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    public int food, happiness, energy;
    public int foodTickRate, happinessTickRate, energyTickRate;
    public System.DateTime lastTimeFed, lastTimeHappy, lastTimeEnergised;

    public void Awake()
    {
        Initialise(100, 100, 100, 10, 10, 10);
    }

    public void Initialise(int food, int happiness, int energy, int foodTickRate, int happinessTickRate, int energyTickRate)
    {
        lastTimeFed = System.DateTime.Now;
        lastTimeHappy = System.DateTime.Now;
        lastTimeEnergised = System.DateTime.Now;
        this.food = food;
        this.happiness = happiness;
        this.energy = energy;
        this.foodTickRate = foodTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
    }

    public void Initialise(int food, int happiness, int energy, int foodTickRate, int happinessTickRate, int energyTickRate, System.DateTime lastTimeFed, System.DateTime lastTimeHappy, System.DateTime lastTimeEnergised)
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergised = lastTimeEnergised;
        this.food = food;
        this.happiness = happiness;
        this.energy = energy;
        this.foodTickRate = foodTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
        //TODO: FINISH TIME PASSING
    }

    private void Update()
    {
        if(TimingManager.gameHourTimer < 0)
        {
            ChangeFood(-foodTickRate);
            ChangeHappiness(-happinessTickRate);
            ChangeEnergy(-energyTickRate);
        }
    }

    public void ChangeFood(int amount)
    {
        food += amount;
        if(amount > 0)
        {
            lastTimeFed = System.DateTime.Now;
        }
        if(food < 0)
        {
            PetManager.instance.Die();
        }
        else if(food > 100) food = 100;
    }

    public void ChangeHappiness(int amount)
    {
        happiness += amount;
        if(amount > 0)
        {
            lastTimeHappy = System.DateTime.Now;
        }
        if(happiness < 0)
        {
            PetManager.instance.Die();
        }
        else if(happiness > 100) happiness = 100;
    }

    public void ChangeEnergy(int amount)
    {
        energy += amount;
        if(amount > 0)
        {
            lastTimeEnergised = System.DateTime.Now;
        }
        if(energy < 0)
        {
            PetManager.instance.Die();
        }
        else if(energy > 100) energy = 100;
    }
}
