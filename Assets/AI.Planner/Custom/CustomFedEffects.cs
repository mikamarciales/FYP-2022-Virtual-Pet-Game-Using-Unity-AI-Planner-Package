using System;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using UnityEngine;
using Unity.AI.Planner.Traits;

namespace AI.Planner.Custom.PetAgentPlan
{
    public struct CustomFedEffects : ICustomActionEffect<StateData>
    {
        public void ApplyCustomActionEffectsToState(StateData originalState, ActionKey action, StateData newState)
        {
            TraitBasedObjectId moverObjectId = newState.GetTraitBasedObjectId(action[0]);
            TraitBasedObject moverObject = newState.GetTraitBasedObject(moverObjectId);
            Need needs = newState.GetTraitOnObject<Need>(moverObject);
    
            needs.ThirstLevel = Mathf.Max(0, needs.ThirstLevel - 3);
            needs.HappinessLevel = Mathf.Max(0, needs.HappinessLevel - 1);
            needs.EnergyLevel = Mathf.Max(0, needs.EnergyLevel - 2);

            //Debug.Log("[AI] Fed! CURRENT LEVELS - Hunger: " + needs.HungerLevel + " Thirst: " + needs.HungerLevel + " Happiness: " + needs.HappinessLevel + " Energy: " + needs.EnergyLevel);

            newState.SetTraitOnObject(needs, ref moverObject);
        }
    }
}