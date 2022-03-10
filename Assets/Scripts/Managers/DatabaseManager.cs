/**using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    private Database database;
    public NeedsController needsController;

    private void Awake()
    {
        database = new Database();
        if(instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one DatabaseManager in the Scene");
    }

    private void Update()
    {
        if(TimingManager.gameHourTimer < 0)
        {
            Pet pet = new Pet(needsController.lastTimeFed.ToString(), needsController.lastTimeDrank.ToString(), needsController.lastTimeHappy.ToString(), needsController.lastTimeEnergised.ToString(), needsController.food, needsController.drink, needsController.happiness, needsController.energy);
            //Pet pet = gameObject.AddComponent<Pet>(needsController.lastTimeFed.ToString(), needsController.lastTimeDrank.ToString(), needsController.lastTimeHappy.ToString(), needsController.lastTimeEnergised.ToString(), needsController.food, needsController.drink, needsController.happiness, needsController.energy);
            SavePet(pet);
        }
    }

    private void Start()
    {
        Pet pet = LoadPet();
        if(pet != null)
        {
            needsController.Initialise(pet.food, pet.drink, pet.happiness, pet.energy, 10, 0, 0, 0, System.DateTime.Parse(pet.lastTimeFed), System.DateTime.Parse(pet.lastTimeDrank), System.DateTime.Parse(pet.lastTimeHappy), System.DateTime.Parse(pet.lastTimeEnergised));
        }
    }

    public void SavePet(Pet pet)
    {
        database.SaveData<Pet>("pet", pet);
    }

    public Pet 
    LoadPet()
    {
        Pet returnValue = null;
        database.LoadData<Pet>("pet", (pet) =>
        {
            returnValue = pet;
        });
        return returnValue;
    }
}
**/