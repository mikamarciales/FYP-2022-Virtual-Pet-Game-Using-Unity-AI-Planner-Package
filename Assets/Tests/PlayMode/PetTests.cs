using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace VirtualPetGame
{
    public class PetTests
    {
        public PetManager petManager;

        [UnityTest]
        public IEnumerator MoveToFood()
        {
            var gameObject = new GameObject();
            var pet = gameObject.AddComponent<PetController>();
            
            pet.Move(petManager.waypoints[11].position);

            yield return new WaitForSeconds(5);

            Assert.AreEqual(new Vector3(-25,3,0), gameObject.transform.position);
        }

        [UnityTest]
        public IEnumerator ChangeFoodValue()
        {
            var gameObject = new GameObject();
            var pet = gameObject.AddComponent<NeedsController>();
            
            pet.ChangeFood(10);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(90, pet.food);
        }

        [UnityTest]
        public IEnumerator ChangeFoodValueOnClick()
        {
            var gameObject = new GameObject();
            var pet = gameObject.AddComponent<NeedsController>();
            
            pet.ChangeFood(50);
            pet.FoodOnClick(10);

            yield return new WaitForSeconds(1);

            Assert.AreEqual(60, pet.food);
            Assert.AreEqual(97, pet.drink);
            Assert.AreEqual(99, pet.happiness);
            Assert.AreEqual(98, pet.energy);
        }
    }
}
