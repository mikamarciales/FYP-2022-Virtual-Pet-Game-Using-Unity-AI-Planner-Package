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
            if(instance == null)
            {
                instance = this;
            }
            else Debug.LogWarning("More than one PetUIController in the Scene");
        }
        public void UpdateImages(int food, int drink, int happiness, int energy)
        {
            foodBar.fillAmount = (float) food/100;
            drinkBar.fillAmount = (float) drink/100;
            happinessBar.fillAmount = (float) happiness/100;
            energyBar.fillAmount = (float) energy/100;
        }
    }
}
