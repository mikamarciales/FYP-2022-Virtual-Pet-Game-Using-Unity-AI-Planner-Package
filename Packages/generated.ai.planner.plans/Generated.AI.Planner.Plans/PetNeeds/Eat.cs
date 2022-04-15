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
    struct Eat : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_agentIndex = 0;
        const int k_treeIndex = 1;
        const int k_timeIndex = 2;
        const int k_hungerIndex = 3;
        const int k_MaxArguments = 4;

        public static readonly string[] parameterNames = {
            "agent",
            "tree",
            "time",
            "hunger",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> agentFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> agentObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> treeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> treeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> timeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> timeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> hungerFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> hungerObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<EatFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal Eat(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            agentFilter = default;
            agentObjectIndices = default;
            treeFilter = default;
            treeObjectIndices = default;
            timeFilter = default;
            timeObjectIndices = default;
            hungerFilter = default;
            hungerObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            agentFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet>(),[1] = ComponentType.ReadWrite<Location>(),  };
            agentObjectIndices = new NativeList<int>(2, Allocator.Temp);
            treeFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Location>(),[1] = ComponentType.ReadWrite<Tree_Food>(),  };
            treeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            timeFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet_Time>(),  };
            timeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            hungerFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Need>(),  };
            hungerObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<EatFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "agent", StringComparison.OrdinalIgnoreCase))
                 return k_agentIndex;
            if (string.Equals(parameterName, "tree", StringComparison.OrdinalIgnoreCase))
                 return k_treeIndex;
            if (string.Equals(parameterName, "time", StringComparison.OrdinalIgnoreCase))
                 return k_timeIndex;
            if (string.Equals(parameterName, "hunger", StringComparison.OrdinalIgnoreCase))
                 return k_hungerIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            agentObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(agentObjectIndices, agentFilter);
            
            treeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(treeObjectIndices, treeFilter);
            
            timeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(timeObjectIndices, timeFilter);
            
            hungerObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(hungerObjectIndices, hungerFilter);
            
            var LocationBuffer = stateData.LocationBuffer;
            var NeedBuffer = stateData.NeedBuffer;
            
            

            for (int i0 = 0; i0 < agentObjectIndices.Length; i0++)
            {
                var agentIndex = agentObjectIndices[i0];
                var agentObject = stateData.TraitBasedObjects[agentIndex];
                
                
                
                
                
                
                
            
            

            for (int i1 = 0; i1 < treeObjectIndices.Length; i1++)
            {
                var treeIndex = treeObjectIndices[i1];
                var treeObject = stateData.TraitBasedObjects[treeIndex];
                
                if (!(LocationBuffer[agentObject.LocationIndex].Position != LocationBuffer[treeObject.LocationIndex].Position))
                    continue;
                
                
                
                
                
                
            
            

            for (int i2 = 0; i2 < timeObjectIndices.Length; i2++)
            {
                var timeIndex = timeObjectIndices[i2];
                var timeObject = stateData.TraitBasedObjects[timeIndex];
                
                
                
                
                
                
                
            
            

            for (int i3 = 0; i3 < hungerObjectIndices.Length; i3++)
            {
                var hungerIndex = hungerObjectIndices[i3];
                var hungerObject = stateData.TraitBasedObjects[hungerIndex];
                
                
                if (!(NeedBuffer[hungerObject.NeedIndex].NeedType == NeedType.Hunger))
                    continue;
                
                if (!(NeedBuffer[hungerObject.NeedIndex].Level <= 90))
                    continue;
                
                
                
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_agentIndex] = agentIndex,
                                                       [k_treeIndex] = treeIndex,
                                                       [k_timeIndex] = timeIndex,
                                                       [k_hungerIndex] = hungerIndex,
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
            var originalhungerObject = originalStateObjectBuffer[action[k_hungerIndex]];
            var originaltimeObject = originalStateObjectBuffer[action[k_timeIndex]];
            var originalagentObject = originalStateObjectBuffer[action[k_agentIndex]];
            var originaltreeObject = originalStateObjectBuffer[action[k_treeIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newNeedBuffer = newState.NeedBuffer;
            var newPet_TimeBuffer = newState.Pet_TimeBuffer;
            var newLocationBuffer = newState.LocationBuffer;
            {
                    var @Need = newNeedBuffer[originalhungerObject.NeedIndex];
                    @Need.@Level += 10;
                    newNeedBuffer[originalhungerObject.NeedIndex] = @Need;
            }
            {
                    var @Pet_Time = newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex];
                    @Pet_Time.@Value += 1;
                    newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex] = @Pet_Time;
            }
            {
                    var @Location = newLocationBuffer[originalagentObject.LocationIndex];
                    @Location.Position = newLocationBuffer[originaltreeObject.LocationIndex].Position;
                    newLocationBuffer[originalagentObject.LocationIndex] = @Location;
            }

            

            var reward = Reward(originalState, action, newState);
            var StateTransitionInfo = new StateTransitionInfo { Probability = 1f, TransitionUtilityValue = reward };
            var resultingStateKey = m_StateDataContext.GetStateDataKey(newState);

            return new StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>(originalStateEntityKey, action, resultingStateKey, StateTransitionInfo);
        }

        float Reward(StateData originalState, ActionKey action, StateData newState)
        {
            var reward = -1f;

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
                TransitionInfo.Add(new EatFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<EatFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetAgentTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_agentIndex]);
        }
        
        public static T GetTreeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_treeIndex]);
        }
        
        public static T GetTimeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_timeIndex]);
        }
        
        public static T GetHungerTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_hungerIndex]);
        }
        
    }

    public struct EatFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


