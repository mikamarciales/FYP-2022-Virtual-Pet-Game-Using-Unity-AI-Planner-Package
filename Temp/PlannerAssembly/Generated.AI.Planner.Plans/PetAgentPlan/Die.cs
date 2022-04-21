using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.Plans.PetAgentPlan
{
    public struct Die
    {
        public bool IsTerminal(StateData stateData)
        {
            var needFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Need>(),  };
            var needObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(needObjectIndices, needFilter);
            var NeedBuffer = stateData.NeedBuffer;
            for (int i0 = 0; i0 < needObjectIndices.Length; i0++)
            {
                var needIndex = needObjectIndices[i0];
                var needObject = stateData.TraitBasedObjects[needIndex];
            
                
                if (!(NeedBuffer[needObject.NeedIndex].Level <= 0))
                    continue;
                needObjectIndices.Dispose();
                needFilter.Dispose();
                return true;
            }
            needObjectIndices.Dispose();
            needFilter.Dispose();

            return false;
        }

        public float TerminalReward(StateData stateData)
        {
            var reward = 0f;

            return reward;
        }
    }
}
