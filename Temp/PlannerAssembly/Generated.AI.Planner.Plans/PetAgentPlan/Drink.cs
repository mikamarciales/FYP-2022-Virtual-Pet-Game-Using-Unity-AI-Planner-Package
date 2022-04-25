using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.Burst;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;
using Generated.Semantic.Traits.Enums;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Generated.AI.Planner.Plans.PetAgentPlan
{
    [BurstCompile]
    struct Drink : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_agentIndex = 0;
        const int k_lakeIndex = 1;
        const int k_timeIndex = 2;
        const int k_thirstIndex = 3;
        const int k_MaxArguments = 4;

        public static readonly string[] parameterNames = {
            "agent",
            "lake",
            "time",
            "thirst",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> agentFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> agentObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> lakeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> lakeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> timeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> timeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> thirstFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> thirstObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<DrinkFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal Drink(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            agentFilter = default;
            agentObjectIndices = default;
            lakeFilter = default;
            lakeObjectIndices = default;
            timeFilter = default;
            timeObjectIndices = default;
            thirstFilter = default;
            thirstObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            agentFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet>(),[1] = ComponentType.ReadWrite<Location>(),  };
            agentObjectIndices = new NativeList<int>(2, Allocator.Temp);
            lakeFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Location>(),[1] = ComponentType.ReadWrite<Lake_Drink>(),  };
            lakeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            timeFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet_Time>(),  };
            timeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            thirstFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Need>(),  };
            thirstObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<DrinkFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "agent", StringComparison.OrdinalIgnoreCase))
                 return k_agentIndex;
            if (string.Equals(parameterName, "lake", StringComparison.OrdinalIgnoreCase))
                 return k_lakeIndex;
            if (string.Equals(parameterName, "time", StringComparison.OrdinalIgnoreCase))
                 return k_timeIndex;
            if (string.Equals(parameterName, "thirst", StringComparison.OrdinalIgnoreCase))
                 return k_thirstIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            agentObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(agentObjectIndices, agentFilter);
            
            lakeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(lakeObjectIndices, lakeFilter);
            
            timeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(timeObjectIndices, timeFilter);
            
            thirstObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(thirstObjectIndices, thirstFilter);
            
            var NeedBuffer = stateData.NeedBuffer;
            
            

            for (int i0 = 0; i0 < agentObjectIndices.Length; i0++)
            {
                var agentIndex = agentObjectIndices[i0];
                var agentObject = stateData.TraitBasedObjects[agentIndex];
                
                
                
                
                
            
            

            for (int i1 = 0; i1 < lakeObjectIndices.Length; i1++)
            {
                var lakeIndex = lakeObjectIndices[i1];
                var lakeObject = stateData.TraitBasedObjects[lakeIndex];
                
                
                
                
                
            
            

            for (int i2 = 0; i2 < timeObjectIndices.Length; i2++)
            {
                var timeIndex = timeObjectIndices[i2];
                var timeObject = stateData.TraitBasedObjects[timeIndex];
                
                
                
                
                
            
            

            for (int i3 = 0; i3 < thirstObjectIndices.Length; i3++)
            {
                var thirstIndex = thirstObjectIndices[i3];
                var thirstObject = stateData.TraitBasedObjects[thirstIndex];
                
                if (!(NeedBuffer[thirstObject.NeedIndex].ThirstTick <= 85))
                    continue;
                
                
                
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_agentIndex] = agentIndex,
                                                       [k_lakeIndex] = lakeIndex,
                                                       [k_timeIndex] = timeIndex,
                                                       [k_thirstIndex] = thirstIndex,
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
            var originalthirstObject = originalStateObjectBuffer[action[k_thirstIndex]];
            var originaltimeObject = originalStateObjectBuffer[action[k_timeIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newNeedBuffer = newState.NeedBuffer;
            var newPet_TimeBuffer = newState.Pet_TimeBuffer;
            {
                    var @Need = newNeedBuffer[originalthirstObject.NeedIndex];
                    @Need.ThirstLevel += newNeedBuffer[originalthirstObject.NeedIndex].ThirstTick;
                    newNeedBuffer[originalthirstObject.NeedIndex] = @Need;
            }
            {
                    var @Pet_Time = newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex];
                    @Pet_Time.@Value += 1;
                    newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex] = @Pet_Time;
            }
            {
                    new global::AI.Planner.Custom.PetAgentPlan.CustomDrankEffects().ApplyCustomActionEffectsToState(originalState, action, newState);
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
                TransitionInfo.Add(new DrinkFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<DrinkFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetAgentTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_agentIndex]);
        }
        
        public static T GetLakeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_lakeIndex]);
        }
        
        public static T GetTimeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_timeIndex]);
        }
        
        public static T GetThirstTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_thirstIndex]);
        }
        
    }

    public struct DrinkFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


