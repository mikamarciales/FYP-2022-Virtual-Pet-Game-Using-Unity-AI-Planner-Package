using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    public int food, drink, happiness, energy;
    public int foodTickRate, drinkTickRate, happinessTickRate, energyTickRate;
    public System.DateTime lastTimeFed, lastTimeDrank, lastTimeHappy, lastTimeEnergised;

    public void Awake()
    {
        Initialise(100, 100, 100, 100, 10, 10, 10, 10);
    }

    public void Initialise(int food, int drink, int happiness, int energy,
                            int foodTickRate, int drinkTickRate, int happinessTickRate, int energyTickRate)
    {
        lastTimeFed = System.DateTime.Now;
        lastTimeDrank = System.DateTime.Now;
        lastTimeHappy = System.DateTime.Now;
        lastTimeEnergised = System.DateTime.Now;
        this.food = food;
        this.drink = drink;
        this.happiness = happiness;
        this.energy = energy;
        this.foodTickRate = foodTickRate;
        this.drinkTickRate = drinkTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
    }

    public void Initialise(int food, int drink, int happiness, int energy,
                            int foodTickRate, int drinkTickRate, int happinessTickRate, int energyTickRate,
                            System.DateTime lastTimeFed, System.DateTime lastTimeDrank, System.DateTime lastTimeHappy, System.DateTime lastTimeEnergised)
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeDrank = lastTimeDrank;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergised = lastTimeEnergised;
        this.food = food;
        this.drink = drink;
        this.happiness = happiness;
        this.energy = energy;
        this.foodTickRate = foodTickRate;
        this.drinkTickRate = drinkTickRate;
        this.happinessTickRate = happinessTickRate;
        this.energyTickRate = energyTickRate;
        //TODO: FINISH TIME PASSING
    }

    private void Update()
    {
        if(TimingManager.gameHourTimer < 0)
        {
            ChangeFood(-foodTickRate);
            ChangeDrink(-drinkTickRate);
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

    public void ChangeDrink(int amount)
    {
        drink += amount;
        if(amount > 0)
        {
            lastTimeDrank = System.DateTime.Now;
        }
        if(drink < 0)
        {
            PetManager.instance.Die();
        }
        else if(drink > 100) drink = 100;
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
