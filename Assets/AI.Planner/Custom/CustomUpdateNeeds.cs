#if PLANNER_DOMAIN_GENERATED
using System;
using AI.Planner.Domains;
//using AI.Planner.Domains.dll;
using Unity.AI.Planner.DomainLanguage.TraitBased;
using Unity.Entities;
//using AI.Planner.Custom;
using Generated.AI.Planner.StateRepresentation.PetAgent;
using Generated.Semantic.Traits;
using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace AI.Planner.Actions.PetAgent
{
    public struct UpdateNeeds : ICustomActionEffect<StateData>
    {
        static ComponentType[] needFilter = { typeof(Need) };

        public void ApplyCustomActionEffectsToState(StateData originalState, ActionKey action, StateData newState)
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
                    need.Level -= need.ChangePerSecond * deltaTime;
                    needBuffer[domainObject.NeedIndex] = need;
                }
            }
        }
    }
}
#endif
