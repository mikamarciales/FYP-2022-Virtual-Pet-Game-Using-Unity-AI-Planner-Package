// PetAgent class - contains methods for 

using System.Collections;
using System.Collections.Generic;
using Unity.AI.Planner;
using UnityEngine;
using Generated.Semantic.Traits;
using Unity.AI.Planner.Controller;
#if PLANNER_STATEREPRESENTATION_GENERATED
using Generated.AI.Planner.StateRepresentation;
#endif

namespace VirtualPetGame
{
    enum ControllerType
    {
        Random,
        Planner
    }

    public class PetAgent : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]

        ControllerType m_ControlledBy;
        DecisionController m_Controller;

        [SerializeField]
        float m_SensorDelay = 1;
#pragma warning restore 0649

        GameObject m_Target;
        Coroutine m_Move;
        float m_TimeOfLastWorldQuery;

        public NeedsController needsController;
        public PetController petController;
        public float moveSpeed = 10;

        bool AtTarget()
        {
            return Vector3.Distance(transform.position, m_Target.transform.position) < 0.3f;
        }

        public IEnumerator Move(GameObject target)
        {
            m_Target = target;

            while (m_Target != null && !AtTarget())
            {
                transform.position = Vector3.MoveTowards(transform.position, m_Target.transform.position, moveSpeed*Time.deltaTime);
                //Debug.Log("[AI] Pet moving towards " + target + "!");
                yield return null;
            }

            if (m_Target != null)
            {
                transform.position = m_Target.transform.position;
            }

            m_Move = null;
        }

        public IEnumerator Eat(GameObject target, int amount)
        {
            Move(target);
            petController.Eat();
            needsController.food += amount;
            needsController.drink -= 3;
            needsController.happiness -= 1;
            needsController.energy -= 2;
            Debug.Log("[AI] Ate! (Food +" + amount + " Drink -3 Happiness -1 Energy -2)");

            if(needsController.food <= 0)
            {
                PetManager.instance.Die();
            }

            if(needsController.food > 100) needsController.food = 100;

            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator Drink(GameObject target, int amount)
        {
            Move(target);
            petController.Drink();
            needsController.drink += amount;
            needsController.food -= 5;
            needsController.happiness -= 1;
            needsController.energy -= 2;
            Debug.Log("[AI] Drank! (Drink +" + amount + " Food -5 Happiness -1 Energy -2)");

            if(needsController.drink <= 0)
            {
                PetManager.instance.Die();
            }

            if(needsController.drink > 100) needsController.drink = 100;

            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator Play(GameObject target, int amount)
        {
            Move(target);
            petController.Happy();
            needsController.happiness += amount;
            needsController.food -= 5;
            needsController.drink -= 3;
            needsController.energy -= 2;
            Debug.Log("[AI] Played! (Happiness +" + amount + " Food -5 Drink -3 Energy -2)");

            if(needsController.happiness <= 0)
            {
                PetManager.instance.Die();
            }

            if(needsController.happiness > 100) needsController.happiness = 100;

            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator Sleep(GameObject target, int amount)
        {
            Move(target);
            petController.Tired();
            needsController.energy += amount;
            needsController.food -= 5;
            needsController.drink -= 3;
            needsController.happiness -= 1;
            Debug.Log("[AI] Slept! (Energy +" + amount + " Food -5 Drink -3 Happiness -1)");

            if(needsController.energy <= 0)
            {
                PetManager.instance.Die();
            }

            if(needsController.energy > 100) needsController.energy = 100;

            yield return new WaitForSeconds(0.5f);
        }

        void Start()
        {
            m_Controller = GetComponent<DecisionController>();
        }

        void Update()
        {
            UpdateControl();
        }

        void UpdateControl()
        {
            if (m_ControlledBy == ControllerType.Random)
            {
                if (m_Move == default)
                {
                    GameObject needItem = GameObject.FindGameObjectWithTag("NeedItem");

                    if (needItem != null)
                    {
                        m_Target = needItem;

                        if (AtTarget())
                            StartCoroutine(Eat(needItem, 10));
                        else
                            m_Move = StartCoroutine(Move(needItem));
                    }
                }
            }
            else
            {
                m_Controller.AutoUpdate = true;
                if (m_Controller.IsIdle && Time.realtimeSinceStartup > m_TimeOfLastWorldQuery + m_SensorDelay)
                {
                    m_Controller.UpdateStateWithWorldQuery();
                    m_TimeOfLastWorldQuery = Time.realtimeSinceStartup;
                }
            }
        }
    }
}