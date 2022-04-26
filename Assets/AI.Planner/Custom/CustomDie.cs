// CustomDie struct created with the intention of being used in the AI Planner as a custom termination precondition
// NOT COMPLETE! Cannot access need levels from the main thread in the AI Planner
// Structure based on the Custom Planner Extension section in the Unity AI Planner 0.3.0-preview.3 package documentation: https://docs.unity3d.com/Packages/com.unity.ai.planner@0.3/manual/CustomPlannerExtensions.html
// Uses AI Planner package library

using System;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using UnityEngine;
using Unity.AI.Planner.Traits;

namespace AI.Planner.Custom.PetAgentPlan
{
    public struct CustomDie : ICustomTerminationPrecondition<StateData>
    {
        public bool CheckCustomPrecondition(StateData state)
        {
            /*TraitBasedObjectId moverObjectId = state.GetTraitBasedObjectId(action[0]);
            TraitBasedObject moverObject = state.GetTraitBasedObject(moverObjectId);
            Need needs = state.GetTraitOnObject<Need>(moverObject);
    
            if(needs.HungerLevel <= 0 || needs.ThirstLevel <= 0 || needs.HappinessLevel <= 0 || needs.EnergyLevel <= 0) {true;}
            else false;*/
            return true;
        }
    }
}