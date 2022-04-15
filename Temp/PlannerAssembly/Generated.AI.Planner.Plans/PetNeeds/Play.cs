using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.Burst;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetNeeds;
using Generated.Semantic.Traits.Enums;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Generated.AI.Planner.Plans.PetNeeds
{
    [BurstCompile]
    struct Play : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_agentIndex = 0;
        const int k_tableIndex = 1;
        const int k_timeIndex = 2;
        const int k_happinessIndex = 3;
        const int k_MaxArguments = 4;

        public static readonly string[] parameterNames = {
            "agent",
            "table",
            "time",
            "happiness",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> agentFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> agentObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> tableFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> tableObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> timeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> timeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> happinessFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> happinessObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<PlayFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal Play(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            agentFilter = default;
            agentObjectIndices = default;
            tableFilter = default;
            tableObjectIndices = default;
            timeFilter = default;
            timeObjectIndices = default;
            happinessFilter = default;
            happinessObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            agentFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet>(),[1] = ComponentType.ReadWrite<Location>(),  };
            agentObjectIndices = new NativeList<int>(2, Allocator.Temp);
            tableFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Table_Happiness>(),[1] = ComponentType.ReadWrite<Location>(),  };
            tableObjectIndices = new NativeList<int>(2, Allocator.Temp);
            timeFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet_Time>(),  };
            timeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            happinessFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Need>(),  };
            happinessObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<PlayFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "agent", StringComparison.OrdinalIgnoreCase))
                 return k_agentIndex;
            if (string.Equals(parameterName, "table", StringComparison.OrdinalIgnoreCase))
                 return k_tableIndex;
            if (string.Equals(parameterName, "time", StringComparison.OrdinalIgnoreCase))
                 return k_timeIndex;
            if (string.Equals(parameterName, "happiness", StringComparison.OrdinalIgnoreCase))
                 return k_happinessIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            agentObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(agentObjectIndices, agentFilter);
            
            tableObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(tableObjectIndices, tableFilter);
            
            timeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(timeObjectIndices, timeFilter);
            
            happinessObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(happinessObjectIndices, happinessFilter);
            
            var LocationBuffer = stateData.LocationBuffer;
            var NeedBuffer = stateData.NeedBuffer;
            
            

            for (int i0 = 0; i0 < agentObjectIndices.Length; i0++)
            {
                var agentIndex = agentObjectIndices[i0];
                var agentObject = stateData.TraitBasedObjects[agentIndex];
                
                
                
                
                
                
                
            
            

            for (int i1 = 0; i1 < tableObjectIndices.Length; i1++)
            {
                var tableIndex = tableObjectIndices[i1];
                var tableObject = stateData.TraitBasedObjects[tableIndex];
                
                if (!(LocationBuffer[agentObject.LocationIndex].Position != LocationBuffer[tableObject.LocationIndex].Position))
                    continue;
                
                
                
                
                
                
            
            

            for (int i2 = 0; i2 < timeObjectIndices.Length; i2++)
            {
                var timeIndex = timeObjectIndices[i2];
                var timeObject = stateData.TraitBasedObjects[timeIndex];
                
                
                
                
                
                
                
            
            

            for (int i3 = 0; i3 < happinessObjectIndices.Length; i3++)
            {
                var happinessIndex = happinessObjectIndices[i3];
                var happinessObject = stateData.TraitBasedObjects[happinessIndex];
                
                
                if (!(NeedBuffer[happinessObject.NeedIndex].NeedType == NeedType.Happiness))
                    continue;
                
                if (!(NeedBuffer[happinessObject.NeedIndex].Level <= 75))
                    continue;
                
                
                
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_agentIndex] = agentIndex,
                                                       [k_tableIndex] = tableIndex,
                                                       [k_timeIndex] = timeIndex,
                                                       [k_happinessIndex] = happinessIndex,
                                                    };
                argumentPermutations.Add(actionKey);
            
            }
            
            }
            
            }
            
            }
        }

        StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> ApplyEffects(ActionKey action, StateEntityKey originalStateEntityKey)
        {
            var originalState = m_StateDataContext.GetStateData(originalStateEntityKey);
            var originalStateObjectBuffer = originalState.TraitBasedObjects;
            var originalhappinessObject = originalStateObjectBuffer[action[k_happinessIndex]];
            var originaltimeObject = originalStateObjectBuffer[action[k_timeIndex]];
            var originalagentObject = originalStateObjectBuffer[action[k_agentIndex]];
            var originaltableObject = originalStateObjectBuffer[action[k_tableIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newNeedBuffer = newState.NeedBuffer;
            var newPet_TimeBuffer = newState.Pet_TimeBuffer;
            var newLocationBuffer = newState.LocationBuffer;
            {
                    var @Need = newNeedBuffer[originalhappinessObject.NeedIndex];
                    @Need.@Level = 100;
                    newNeedBuffer[originalhappinessObject.NeedIndex] = @Need;
            }
            {
                    var @Pet_Time = newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex];
                    @Pet_Time.@Value += 4;
                    newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex] = @Pet_Time;
            }
            {
                    var @Location = newLocationBuffer[originalagentObject.LocationIndex];
                    @Location.Position = newLocationBuffer[originaltableObject.LocationIndex].Position;
                    newLocationBuffer[originalagentObject.LocationIndex] = @Location;
            }

            

            var reward = Reward(originalState, action, newState);
            var StateTransitionInfo = new StateTransitionInfo { Probability = 1f, TransitionUtilityValue = reward };
            var resultingStateKey = m_StateDataContext.GetStateDataKey(newState);

            return new StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>(originalStateEntityKey, action, resultingStateKey, StateTransitionInfo);
        }

        float Reward(StateData originalState, ActionKey action, StateData newState)
        {
            var reward = -5f;

            return reward;
        }

        public void Execute(int jobIndex)
        {
            if (!LocalContainersInitialized)
                InitializeLocalContainers();

            m_StateDataContext.JobIndex = jobIndex;

            var stateEntityKey = m_StatesToExpand[jobIndex];
            var stateData = m_StateDataContext.GetStateData(stateEntityKey);

            ArgumentPermutations.Clear();
            GenerateArgumentPermutations(stateData, ArgumentPermutations);

            TransitionInfo.Clear();
            TransitionInfo.Capacity = math.max(TransitionInfo.Capacity, ArgumentPermutations.Length);
            for (var i = 0; i < ArgumentPermutations.Length; i++)
            {
                TransitionInfo.Add(new PlayFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<PlayFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetAgentTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_agentIndex]);
        }
        
        public static T GetTableTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_tableIndex]);
        }
        
        public static T GetTimeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_timeIndex]);
        }
        
        public static T GetHappinessTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_happinessIndex]);
        }
        
    }

    public struct PlayFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


