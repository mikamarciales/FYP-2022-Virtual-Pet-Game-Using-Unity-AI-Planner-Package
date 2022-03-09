using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class Pet : MonoBehaviour
{
    public string lastTimeFed, lastTimeHappy, lastTimeEnergised;
    public int food, happiness, energy;

    public Pet(string lastTimeFed, string lastTimeHappy, string lastTimeEnergised, int food, int happiness, int energy)
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergised = lastTimeEnergised;
        this.food = food;
        this.happiness = happiness;
        this.energy = energy;
    }
}
