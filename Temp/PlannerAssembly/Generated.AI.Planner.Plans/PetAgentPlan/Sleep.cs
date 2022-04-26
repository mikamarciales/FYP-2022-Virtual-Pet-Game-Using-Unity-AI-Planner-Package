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
    struct Sleep : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_agentIndex = 0;
        const int k_homeIndex = 1;
        const int k_timeIndex = 2;
        const int k_energyIndex = 3;
        const int k_MaxArguments = 4;

        public static readonly string[] parameterNames = {
            "agent",
            "home",
            "time",
            "energy",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> agentFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> agentObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> homeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> homeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> timeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> timeObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> energyFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> energyObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<SleepFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal Sleep(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            agentFilter = default;
            agentObjectIndices = default;
            homeFilter = default;
            homeObjectIndices = default;
            timeFilter = default;
            timeObjectIndices = default;
            energyFilter = default;
            energyObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            agentFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet>(),[1] = ComponentType.ReadWrite<Location>(),  };
            agentObjectIndices = new NativeList<int>(2, Allocator.Temp);
            homeFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Home_Energy>(),[1] = ComponentType.ReadWrite<Location>(),  };
            homeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            timeFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet_Time>(),  };
            timeObjectIndices = new NativeList<int>(2, Allocator.Temp);
            energyFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Need>(),  };
            energyObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<SleepFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "agent", StringComparison.OrdinalIgnoreCase))
                 return k_agentIndex;
            if (string.Equals(parameterName, "home", StringComparison.OrdinalIgnoreCase))
                 return k_homeIndex;
            if (string.Equals(parameterName, "time", StringComparison.OrdinalIgnoreCase))
                 return k_timeIndex;
            if (string.Equals(parameterName, "energy", StringComparison.OrdinalIgnoreCase))
                 return k_energyIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            agentObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(agentObjectIndices, agentFilter);
            
            homeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(homeObjectIndices, homeFilter);
            
            timeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(timeObjectIndices, timeFilter);
            
            energyObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(energyObjectIndices, energyFilter);
            
            var NeedBuffer = stateData.NeedBuffer;
            var LocationBuffer = stateData.LocationBuffer;
            
            

            for (int i0 = 0; i0 < agentObjectIndices.Length; i0++)
            {
                var agentIndex = agentObjectIndices[i0];
                var agentObject = stateData.TraitBasedObjects[agentIndex];
                
                
                
                
                
                
            
            

            for (int i1 = 0; i1 < homeObjectIndices.Length; i1++)
            {
                var homeIndex = homeObjectIndices[i1];
                var homeObject = stateData.TraitBasedObjects[homeIndex];
                
                
                if (!(LocationBuffer[agentObject.LocationIndex].Position != LocationBuffer[homeObject.LocationIndex].Position))
                    continue;
                
                
                
                
            
            

            for (int i2 = 0; i2 < timeObjectIndices.Length; i2++)
            {
                var timeIndex = timeObjectIndices[i2];
                var timeObject = stateData.TraitBasedObjects[timeIndex];
                
                
                
                
                
                
            
            

            for (int i3 = 0; i3 < energyObjectIndices.Length; i3++)
            {
                var energyIndex = energyObjectIndices[i3];
                var energyObject = stateData.TraitBasedObjects[energyIndex];
                
                if (!(NeedBuffer[energyObject.NeedIndex].EnergyLevel <= 80))
                    continue;
                
                
                
                
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_agentIndex] = agentIndex,
                                                       [k_homeIndex] = homeIndex,
                                                       [k_timeIndex] = timeIndex,
                                                       [k_energyIndex] = energyIndex,
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
            var originalagentObject = originalStateObjectBuffer[action[k_agentIndex]];
            var originalhomeObject = originalStateObjectBuffer[action[k_homeIndex]];
            var originalenergyObject = originalStateObjectBuffer[action[k_energyIndex]];
            var originaltimeObject = originalStateObjectBuffer[action[k_timeIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newLocationBuffer = newState.LocationBuffer;
            var newNeedBuffer = newState.NeedBuffer;
            var newPet_TimeBuffer = newState.Pet_TimeBuffer;
            {
                    var @Location = newLocationBuffer[originalagentObject.LocationIndex];
                    @Location.Position = newLocationBuffer[originalhomeObject.LocationIndex].Position;
                    newLocationBuffer[originalagentObject.LocationIndex] = @Location;
            }
            {
                    var @Need = newNeedBuffer[originalenergyObject.NeedIndex];
                    @Need.EnergyLevel += newNeedBuffer[originalenergyObject.NeedIndex].EnergyTick;
                    newNeedBuffer[originalenergyObject.NeedIndex] = @Need;
            }
            {
                    var @Pet_Time = newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex];
                    @Pet_Time.@Value += 7;
                    newPet_TimeBuffer[originaltimeObject.Pet_TimeIndex] = @Pet_Time;
            }
            {
                    new global::AI.Planner.Custom.PetAgentPlan.CustomSleptEffects().ApplyCustomActionEffectsToState(originalState, action, newState);
            }

            

            var reward = Reward(originalState, action, newState);
            var StateTransitionInfo = new StateTransitionInfo { Probability = 1f, TransitionUtilityValue = reward };
            var resultingStateKey = m_StateDataContext.GetStateDataKey(newState);

            return new StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>(originalStateEntityKey, action, resultingStateKey, StateTransitionInfo);
        }

        float Reward(StateData originalState, ActionKey action, StateData newState)
        {
            var reward = -3f;

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
                TransitionInfo.Add(new SleepFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<SleepFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetAgentTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_agentIndex]);
        }
        
        public static T GetHomeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_homeIndex]);
        }
        
        public static T GetTimeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_timeIndex]);
        }
        
        public static T GetEnergyTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_energyIndex]);
        }
        
    }

    public struct SleepFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


