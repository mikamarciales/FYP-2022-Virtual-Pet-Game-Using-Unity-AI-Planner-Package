// PetUIController class created to be used in the base game
// Defines method to update need level bar images as needs are fulfilled/deteriorate
// Structure based on the Education Ecosystem's 'How To Create A Virtual Pet Game In Unity' tutorial: https://www.youtube.com/watch?v=MAK2Qzu0j40&list=PLQbzkJk10-f6mK42ieiFoU6YGQN7BM_nE&ab_channel=EducationEcosystem

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VirtualPetGame
{
    public class PetUIController : MonoBehaviour
    {
        public Image foodBar, drinkBar, happinessBar, energyBar;

        public static PetUIController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else Debug.LogWarning("More than one PetUIController in the Scene");
        }
        public void UpdateImages(int food, int drink, int happiness, int energy)
        {
            foodBar.fillAmount = (float)food / 100;
            drinkBar.fillAmount = (float)drink / 100;
            happinessBar.fillAmount = (float)happiness / 100;
            energyBar.fillAmount = (float)energy / 100;
        }
    }
}
