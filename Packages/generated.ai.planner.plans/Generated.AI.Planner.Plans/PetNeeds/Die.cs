using Unity.AI.Planner;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetNeeds;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.Plans.PetNeeds
{
    public struct Die
    {
        public bool IsTerminal(StateData stateData)
        {
            var parameter1Filter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Need>(),  };
            var parameter1ObjectIndices = new NativeList<int>(2, Allocator.Temp);
            stateData.GetTraitBasedObjectIndices(parameter1ObjectIndices, parameter1Filter);
            var NeedBuffer = stateData.NeedBuffer;
            for (int i0 = 0; i0 < parameter1ObjectIndices.Length; i0++)
            {
                var parameter1Index = parameter1ObjectIndices[i0];
                var parameter1Object = stateData.TraitBasedObjects[parameter1Index];
            
                
                if (!(NeedBuffer[parameter1Object.NeedIndex].Urgency >= 20))
                    continue;
                parameter1ObjectIndices.Dispose();
                parameter1Filter.Dispose();
                return true;
            }
            parameter1ObjectIndices.Dispose();
            parameter1Filter.Dispose();

            return false;
        }

        public float TerminalReward(StateData stateData)
        {
            var reward = 0f;

            return reward;
        }
    }
}
