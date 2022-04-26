// Pet abstract class created to be used in the base game
// Defines Pet and its variables
// Structure based on the Education Ecosystem's 'How To Create A Virtual Pet Game In Unity' tutorial: https://www.youtube.com/watch?v=MAK2Qzu0j40&list=PLQbzkJk10-f6mK42ieiFoU6YGQN7BM_nE&ab_channel=EducationEcosystem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
