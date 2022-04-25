using System.Collections;
using System.Collections.Generic;
using System;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using UnityEngine;
using Unity.AI.Planner.Traits;

namespace AI.Planner.Custom.PetAgentPlan
{
    public struct TargetInSightPrecondition : ICustomActionPrecondition<StateData> {
        /*private static PhysicsScene physics = Physics.defaultPhysicsScene;
        public bool CheckCustomPrecondition(StateData state, ActionKey action)
        {
            Location agentLocation = state.GetTraitOnObject<Location>(state.GetTraitBasedObject(state.GetTraitBasedObjectId(action[0])));
            Location targetLocation = state.GetTraitOnObject<Location>(state.GetTraitBasedObject(state.GetTraitBasedObjectId(action[1])));
    
            return ProximityUtility.IsNear(moverLocation, targetLocation);
            Vector3 agentPos = agentLocation.Position;
            Vector3 targetPos = targetLocation.Position;
            var targetDir = (agentPos - targetPos).normalize;
            return TargetIsInSight(agentPos, targetDir);
        }
        public static bool TargetIsInSight(float3 position, float3 dir)
        {
            return physics.Raycast(position, dir);
        }*/
        public bool CheckCustomPrecondition(StateData state, ActionKey action)
        {
            return true;
        }
    }
}
