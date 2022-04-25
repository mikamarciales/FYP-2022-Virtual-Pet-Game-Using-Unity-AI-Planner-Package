using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
namespace VirtualPetGame
{
    public abstract class Pet : MonoBehaviour
    {
        public string lastTimeFed, lastTimeDrank, lastTimeHappy, lastTimeEnergised;
        public int food, drink, happiness, energy;

        public Pet(string lastTimeFed, string lastTimeDrank, string lastTimeHappy, string lastTimeEnergised,
                    int food, int drink, int happiness, int energy)
        {
            this.lastTimeFed = lastTimeFed;
            this.lastTimeDrank = lastTimeDrank;
            this.lastTimeHappy = lastTimeHappy;
            this.lastTimeEnergised = lastTimeEnergised;
            this.food = food;
            this.drink = drink;
            this.happiness = happiness;
            this.energy = energy;
        }
    }
}
