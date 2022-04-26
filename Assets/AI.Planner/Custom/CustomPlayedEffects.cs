// CustomPlayedEffects struct created to be used in the AI Planner as a custom action effect
// Defines method ApplyCustomActionEffectsToState which finds the other Need traits and decreases them by a certain amount after the Pet Agent plays
// Structure based on the Custom Planner Extension section in the Unity AI Planner 0.3.0-preview.3 package documentation: https://docs.unity3d.com/Packages/com.unity.ai.planner@0.3/manual/CustomPlannerExtensions.html
// Uses AI Planner package library

using System;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using UnityEngine;
using Unity.AI.Planner.Traits;

namespace AI.Planner.Custom.PetAgentPlan
{
    public struct CustomPlayedEffects : ICustomActionEffect<StateData>
    {
        public void ApplyCustomActionEffectsToState(StateData originalState, ActionKey action, StateData newState)
        {
            TraitBasedObjectId moverObjectId = newState.GetTraitBasedObjectId(action[0]);
            TraitBasedObject moverObject = newState.GetTraitBasedObject(moverObjectId);
            Need needs = newState.GetTraitOnObject<Need>(moverObject);

            needs.HungerLevel = Mathf.Max(0, needs.HungerLevel - 5);
            needs.ThirstLevel = Mathf.Max(0, needs.ThirstLevel - 3);
            needs.EnergyLevel = Mathf.Max(0, needs.EnergyLevel - 2);

            //Debug.Log("[AI] Played! CURRENT LEVELS - Hunger: " + needs.HungerLevel + " Thirst: " + needs.HungerLevel + " Happiness: " + needs.HappinessLevel + " Energy: " + needs.EnergyLevel);

            newState.SetTraitOnObject(needs, ref moverObject);
        }
    }
}