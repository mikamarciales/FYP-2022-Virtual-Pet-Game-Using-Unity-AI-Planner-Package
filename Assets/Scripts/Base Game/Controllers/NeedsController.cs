using System.Collections;
using System.Collections.Generic;
using Unity.AI.Planner;
using UnityEngine;

namespace VirtualPetGame
{
    public class NeedsController : MonoBehaviour
    {
        public int food, drink, happiness, energy;
        public int foodTickRate, drinkTickRate, happinessTickRate, energyTickRate;
        public System.DateTime lastTimeFed, lastTimeDrank, lastTimeHappy, lastTimeEnergised;

        public void Awake()
        {
            Initialise(100, 100, 100, 100, 10, 7, 2, 5);
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
            PetUIController.instance.UpdateImages(food, drink, happiness, energy);
        }

        private void Update()
        {
            if(TimingManager.gameHourTimer < 0 && FindObjectOfType<GameManager>().isGameOver == false)
            {
                ChangeFood(-foodTickRate);
                ChangeDrink(-drinkTickRate);
                ChangeHappiness(-happinessTickRate);
                ChangeEnergy(-energyTickRate);
            }
            if (TimingManager.gameTickTimer < 0 && FindObjectOfType<GameManager>().isGameOver == false)
            {
                PetUIController.instance.UpdateImages(food, drink, happiness, energy);
            }
        }

        public void ChangeFood(int amount)
        {
            food += amount;
            if(amount > 0)
            {
                lastTimeFed = System.DateTime.Now;
            }
            if(food <= 0)
            {
                PetManager.instance.Die();
            }
            else if(food > 100) food = 100;
        }

        public void FoodOnClick(int amount)
        {
            food += amount;
            Debug.Log("Food clicked! (Food +" + amount + " Drink -3 Happiness -1 Energy -2)");
            drink -= 3;
            happiness -= 1;
            energy -= 2;
            if(amount > 0)
            {
                lastTimeFed = System.DateTime.Now;
            }
            if(food <= 0)
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
            if(drink <= 0)
            {
                PetManager.instance.Die();
            }
            else if(drink > 100) drink = 100;
        }

        public void DrinkOnClick(int amount)
        {
            drink += amount;
            Debug.Log("Drink clicked! (Food -5 Drink +" + amount + " Happiness -1 Energy -2)");
            food -= 5;
            happiness -= 1;
            energy -= 2;
            if(amount > 0)
            {
                lastTimeDrank = System.DateTime.Now;
            }
            if(drink <= 0)
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
            if(happiness <= 0)
            {
                PetManager.instance.Die();
            }
            else if(happiness > 100) happiness = 100;
        }

        public void HappinessOnClick(int amount)
        {
            happiness += amount;
            Debug.Log("Happiness clicked! (Food -5 Drink -3 Happiness +" + amount + " Energy -2)");
            drink -= 3;
            food -= 5;
            energy -= 2;
            if(amount > 0)
            {
                lastTimeHappy = System.DateTime.Now;
            }
            if(happiness <= 0)
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
            if(energy <= 0)
            {
                PetManager.instance.Die();
            }
            else if(energy > 100) energy = 100;
        }

        public void EnergyOnClick(int amount)
        {
            energy += amount;
            Debug.Log("Energy clicked! (Food -5 Drink -3 Happiness -1 Energy +" + amount + ")");
            drink -= 3;
            happiness -= 1;
            food -= 5;
            if(amount > 0)
            {
                lastTimeEnergised = System.DateTime.Now;
            }
            if(energy <= 0)
            {
                PetManager.instance.Die();
            }
            else if(energy > 100) energy = 100;
        }
    }
}
