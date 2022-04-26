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