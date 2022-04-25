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
            if (gameHourTimer <= 0 && gameTickTimer <=0)
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
