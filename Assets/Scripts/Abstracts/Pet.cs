using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public System.DateTime lastTimeFed, lastTimeHappy, lastTimeEnergised;
    public int food, happiness, energy;

    public Pet(System.DateTime lastTimeFed, System.DateTime lastTimeHappy, System.DateTime lastTimeEnergised, int food, int happiness, int energy)
    {
        this.lastTimeFed = lastTimeFed;
        this.lastTimeHappy = lastTimeHappy;
        this.lastTimeEnergised = lastTimeEnergised;
        this.food = food;
        this.happiness = happiness;
        this.energy = energy;
    }
}
