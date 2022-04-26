// TimingManager class created to be used in the base game
// Defines the timers to be used internally within the base game
// Structure based on the Education Ecosystem's 'How To Create A Virtual Pet Game In Unity' tutorial: https://www.youtube.com/watch?v=MAK2Qzu0j40&list=PLQbzkJk10-f6mK42ieiFoU6YGQN7BM_nE&ab_channel=EducationEcosystem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualPetGame
{
    public class TimingManager : MonoBehaviour
    {
        public static float gameHourTimer, gameTickTimer;
        public float hourLength, tickLength;

        private void Update()
        {
            if (gameHourTimer <= 0 && gameTickTimer <= 0)
            {
                gameHourTimer = hourLength;
                gameTickTimer = tickLength;
            }
            else
            {
                gameHourTimer -= Time.deltaTime;
                gameTickTimer -= Time.deltaTime;
            }
        }

    }
}
