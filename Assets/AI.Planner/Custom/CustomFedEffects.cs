//#if PLANNER_DOMAIN_GENERATED
using System;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using UnityEngine;
using Unity.AI.Planner.Traits;
//using AI.Planner.Domains;
//using Unity.AI.Planner.DomainLanguage.TraitBased;
//using Unity.Entities;

namespace AI.Planner.Custom.PetAgentPlan
{
    public struct CustomFedEffects : ICustomActionEffect<StateData>
    {
        //public NeedsController needsController;
        //static ComponentType[] needFilter = { typeof(Need) };

        /*public void ApplyCustomActionEffectsToState(StateData originalState, ActionKey action, StateData newState)
        {
            // Should only be one time object
            var previousTime = originalState.TimeBuffer[0];
            var newStateTime = newState.TimeBuffer[0];
            var deltaTime = newStateTime.Value - previousTime.Value;

            var needBuffer = newState.NeedBuffer;
            var objectBuffer = newState.DomainObjects;
            for (var obj = 0; obj < objectBuffer.Length; obj++)
            {
                var domainObject = objectBuffer[obj];
                if (domainObject.MatchesTraitFilter(needFilter))
                {
                    var need = needBuffer[domainObject.NeedIndex];
                    need.Urgency += need.ChangePerSecond * deltaTime;
                    needBuffer[domainObject.NeedIndex] = need;
                }
            }
        }*/

        public void ApplyCustomActionEffectsToState(StateData originalState, ActionKey action, StateData newState)
        {
            Debug.Log("[AI] Fed");
        }
    }
}
//#endif