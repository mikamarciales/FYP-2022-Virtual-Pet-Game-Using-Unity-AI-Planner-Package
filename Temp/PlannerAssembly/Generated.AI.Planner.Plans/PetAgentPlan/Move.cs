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
    struct Move : IJobParallelForDefer
    {
        public Guid ActionGuid;
        
        const int k_MoverIndex = 0;
        const int k_DestinationIndex = 1;
        const int k_TimeIndex = 2;
        const int k_MaxArguments = 3;

        public static readonly string[] parameterNames = {
            "Mover",
            "Destination",
            "Time",
        };

        [ReadOnly] NativeArray<StateEntityKey> m_StatesToExpand;
        StateDataContext m_StateDataContext;

        // local allocations
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> MoverFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> MoverObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> DestinationFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> DestinationObjectIndices;
        [NativeDisableContainerSafetyRestriction] NativeArray<ComponentType> TimeFilter;
        [NativeDisableContainerSafetyRestriction] NativeList<int> TimeObjectIndices;

        [NativeDisableContainerSafetyRestriction] NativeList<ActionKey> ArgumentPermutations;
        [NativeDisableContainerSafetyRestriction] NativeList<MoveFixupReference> TransitionInfo;

        bool LocalContainersInitialized => ArgumentPermutations.IsCreated;

        internal Move(Guid guid, NativeList<StateEntityKey> statesToExpand, StateDataContext stateDataContext)
        {
            ActionGuid = guid;
            m_StatesToExpand = statesToExpand.AsDeferredJobArray();
            m_StateDataContext = stateDataContext;
            MoverFilter = default;
            MoverObjectIndices = default;
            DestinationFilter = default;
            DestinationObjectIndices = default;
            TimeFilter = default;
            TimeObjectIndices = default;
            ArgumentPermutations = default;
            TransitionInfo = default;
        }

        void InitializeLocalContainers()
        {
            MoverFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Moveable>(),[1] = ComponentType.ReadWrite<Location>(),  };
            MoverObjectIndices = new NativeList<int>(2, Allocator.Temp);
            DestinationFilter = new NativeArray<ComponentType>(2, Allocator.Temp){[0] = ComponentType.ReadWrite<Location>(), [1] = ComponentType.Exclude<Moveable>(),  };
            DestinationObjectIndices = new NativeList<int>(2, Allocator.Temp);
            TimeFilter = new NativeArray<ComponentType>(1, Allocator.Temp){[0] = ComponentType.ReadWrite<Pet_Time>(),  };
            TimeObjectIndices = new NativeList<int>(2, Allocator.Temp);

            ArgumentPermutations = new NativeList<ActionKey>(4, Allocator.Temp);
            TransitionInfo = new NativeList<MoveFixupReference>(ArgumentPermutations.Length, Allocator.Temp);
        }

        public static int GetIndexForParameterName(string parameterName)
        {
            
            if (string.Equals(parameterName, "Mover", StringComparison.OrdinalIgnoreCase))
                 return k_MoverIndex;
            if (string.Equals(parameterName, "Destination", StringComparison.OrdinalIgnoreCase))
                 return k_DestinationIndex;
            if (string.Equals(parameterName, "Time", StringComparison.OrdinalIgnoreCase))
                 return k_TimeIndex;

            return -1;
        }

        void GenerateArgumentPermutations(StateData stateData, NativeList<ActionKey> argumentPermutations)
        {
            MoverObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(MoverObjectIndices, MoverFilter);
            
            DestinationObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(DestinationObjectIndices, DestinationFilter);
            
            TimeObjectIndices.Clear();
            stateData.GetTraitBasedObjectIndices(TimeObjectIndices, TimeFilter);
            
            var LocationBuffer = stateData.LocationBuffer;
            
            

            for (int i0 = 0; i0 < MoverObjectIndices.Length; i0++)
            {
                var MoverIndex = MoverObjectIndices[i0];
                var MoverObject = stateData.TraitBasedObjects[MoverIndex];
                
                
                
                
            
            

            for (int i1 = 0; i1 < DestinationObjectIndices.Length; i1++)
            {
                var DestinationIndex = DestinationObjectIndices[i1];
                var DestinationObject = stateData.TraitBasedObjects[DestinationIndex];
                
                if (!(LocationBuffer[MoverObject.LocationIndex].Position != LocationBuffer[DestinationObject.LocationIndex].Position))
                    continue;
                
                
                
            
            

            for (int i2 = 0; i2 < TimeObjectIndices.Length; i2++)
            {
                var TimeIndex = TimeObjectIndices[i2];
                var TimeObject = stateData.TraitBasedObjects[TimeIndex];
                
                
                
                

                var actionKey = new ActionKey(k_MaxArguments) {
                                                        ActionGuid = ActionGuid,
                                                       [k_MoverIndex] = MoverIndex,
                                                       [k_DestinationIndex] = DestinationIndex,
                                                       [k_TimeIndex] = TimeIndex,
                                                    };
                argumentPermutations.Add(actionKey);
            
            }
            
            }
            
            }
        }

        StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> ApplyEffects(ActionKey action, StateEntityKey originalStateEntityKey)
        {
            var originalState = m_StateDataContext.GetStateData(originalStateEntityKey);
            var originalStateObjectBuffer = originalState.TraitBasedObjects;
            var originalMoverObject = originalStateObjectBuffer[action[k_MoverIndex]];
            var originalDestinationObject = originalStateObjectBuffer[action[k_DestinationIndex]];
            var originalTimeObject = originalStateObjectBuffer[action[k_TimeIndex]];

            var newState = m_StateDataContext.CopyStateData(originalState);
            var newLocationBuffer = newState.LocationBuffer;
            var newPet_TimeBuffer = newState.Pet_TimeBuffer;
            {
                    var @Location = newLocationBuffer[originalMoverObject.LocationIndex];
                    @Location.Position = newLocationBuffer[originalDestinationObject.LocationIndex].Position;
                    newLocationBuffer[originalMoverObject.LocationIndex] = @Location;
            }
            {
                    new global::AI.Planner.Custom.PetAgentPlan.CustomMoveEffects().ApplyCustomActionEffectsToState(originalState, action, newState);
            }
            {
                    var @Pet_Time = newPet_TimeBuffer[originalTimeObject.Pet_TimeIndex];
                    @Pet_Time.@Value += 1;
                    newPet_TimeBuffer[originalTimeObject.Pet_TimeIndex] = @Pet_Time;
            }

            

            var reward = Reward(originalState, action, newState);
            var StateTransitionInfo = new StateTransitionInfo { Probability = 1f, TransitionUtilityValue = reward };
            var resultingStateKey = m_StateDataContext.GetStateDataKey(newState);

            return new StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>(originalStateEntityKey, action, resultingStateKey, StateTransitionInfo);
        }

        float Reward(StateData originalState, ActionKey action, StateData newState)
        {
            var reward = -2f;
            {
                var param0 = originalState.GetTraitOnObjectAtIndex<Unity.AI.Planner.Traits.Location>(action[0]);
                var param1 = originalState.GetTraitOnObjectAtIndex<Unity.AI.Planner.Traits.Location>(action[1]);
                reward -= new global::Unity.AI.Planner.Navigation.LocationDistance().RewardModifier( param0, param1);
            }

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
                TransitionInfo.Add(new MoveFixupReference { TransitionInfo = ApplyEffects(ArgumentPermutations[i], stateEntityKey) });
            }

            // fixups
            var stateEntity = stateEntityKey.Entity;
            var fixupBuffer = m_StateDataContext.EntityCommandBuffer.AddBuffer<MoveFixupReference>(jobIndex, stateEntity);
            fixupBuffer.CopyFrom(TransitionInfo);
        }

        
        public static T GetMoverTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_MoverIndex]);
        }
        
        public static T GetDestinationTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_DestinationIndex]);
        }
        
        public static T GetTimeTrait<T>(StateData state, ActionKey action) where T : struct, ITrait
        {
            return state.GetTraitOnObjectAtIndex<T>(action[k_TimeIndex]);
        }
        
    }

    public struct MoveFixupReference : IBufferElementData
    {
        internal StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo> TransitionInfo;
    }
}


