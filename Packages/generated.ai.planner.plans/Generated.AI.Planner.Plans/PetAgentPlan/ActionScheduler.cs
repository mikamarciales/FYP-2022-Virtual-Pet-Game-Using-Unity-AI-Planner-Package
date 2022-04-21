using System;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.AI.Planner.Jobs;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetAgentPlan;

namespace Generated.AI.Planner.Plans.PetAgentPlan
{
    public struct ActionScheduler :
        ITraitBasedActionScheduler<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager, ActionKey>
    {
        public static readonly Guid MoveGuid = Guid.NewGuid();
        public static readonly Guid EatGuid = Guid.NewGuid();
        public static readonly Guid DrinkGuid = Guid.NewGuid();
        public static readonly Guid PlayGuid = Guid.NewGuid();
        public static readonly Guid SleepGuid = Guid.NewGuid();

        // Input
        public NativeList<StateEntityKey> UnexpandedStates { get; set; }
        public StateManager StateManager { get; set; }

        // Output
        NativeQueue<StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>> IActionScheduler<StateEntityKey, StateData, StateDataContext, StateManager, ActionKey>.CreatedStateInfo
        {
            set => m_CreatedStateInfo = value;
        }

        NativeQueue<StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>> m_CreatedStateInfo;

        struct PlaybackECB : IJob
        {
            public ExclusiveEntityTransaction ExclusiveEntityTransaction;

            [ReadOnly]
            public NativeList<StateEntityKey> UnexpandedStates;
            public NativeQueue<StateTransitionInfoPair<StateEntityKey, ActionKey, StateTransitionInfo>> CreatedStateInfo;
            public EntityCommandBuffer MoveECB;
            public EntityCommandBuffer EatECB;
            public EntityCommandBuffer DrinkECB;
            public EntityCommandBuffer PlayECB;
            public EntityCommandBuffer SleepECB;

            public void Execute()
            {
                // Playback entity changes and output state transition info
                var entityManager = ExclusiveEntityTransaction;

                MoveECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var MoveRefs = entityManager.GetBuffer<MoveFixupReference>(stateEntity);
                    for (int j = 0; j < MoveRefs.Length; j++)
                        CreatedStateInfo.Enqueue(MoveRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(MoveFixupReference));
                }

                EatECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var EatRefs = entityManager.GetBuffer<EatFixupReference>(stateEntity);
                    for (int j = 0; j < EatRefs.Length; j++)
                        CreatedStateInfo.Enqueue(EatRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(EatFixupReference));
                }

                DrinkECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var DrinkRefs = entityManager.GetBuffer<DrinkFixupReference>(stateEntity);
                    for (int j = 0; j < DrinkRefs.Length; j++)
                        CreatedStateInfo.Enqueue(DrinkRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(DrinkFixupReference));
                }

                PlayECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var PlayRefs = entityManager.GetBuffer<PlayFixupReference>(stateEntity);
                    for (int j = 0; j < PlayRefs.Length; j++)
                        CreatedStateInfo.Enqueue(PlayRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(PlayFixupReference));
                }

                SleepECB.Playback(entityManager);
                for (int i = 0; i < UnexpandedStates.Length; i++)
                {
                    var stateEntity = UnexpandedStates[i].Entity;
                    var SleepRefs = entityManager.GetBuffer<SleepFixupReference>(stateEntity);
                    for (int j = 0; j < SleepRefs.Length; j++)
                        CreatedStateInfo.Enqueue(SleepRefs[j].TransitionInfo);
                    entityManager.RemoveComponent(stateEntity, typeof(SleepFixupReference));
                }
            }
        }

        public JobHandle Schedule(JobHandle inputDeps)
        {
            var entityManager = StateManager.ExclusiveEntityTransaction.EntityManager;
            var MoveDataContext = StateManager.StateDataContext;
            var MoveECB = StateManager.GetEntityCommandBuffer();
            MoveDataContext.EntityCommandBuffer = MoveECB.AsParallelWriter();
            var EatDataContext = StateManager.StateDataContext;
            var EatECB = StateManager.GetEntityCommandBuffer();
            EatDataContext.EntityCommandBuffer = EatECB.AsParallelWriter();
            var DrinkDataContext = StateManager.StateDataContext;
            var DrinkECB = StateManager.GetEntityCommandBuffer();
            DrinkDataContext.EntityCommandBuffer = DrinkECB.AsParallelWriter();
            var PlayDataContext = StateManager.StateDataContext;
            var PlayECB = StateManager.GetEntityCommandBuffer();
            PlayDataContext.EntityCommandBuffer = PlayECB.AsParallelWriter();
            var SleepDataContext = StateManager.StateDataContext;
            var SleepECB = StateManager.GetEntityCommandBuffer();
            SleepDataContext.EntityCommandBuffer = SleepECB.AsParallelWriter();

            var allActionJobs = new NativeArray<JobHandle>(6, Allocator.TempJob)
            {
                [0] = new Move(MoveGuid, UnexpandedStates, MoveDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [1] = new Eat(EatGuid, UnexpandedStates, EatDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [2] = new Drink(DrinkGuid, UnexpandedStates, DrinkDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [3] = new Play(PlayGuid, UnexpandedStates, PlayDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [4] = new Sleep(SleepGuid, UnexpandedStates, SleepDataContext).Schedule(UnexpandedStates, 0, inputDeps),
                [5] = entityManager.ExclusiveEntityTransactionDependency
            };

            var allActionJobsHandle = JobHandle.CombineDependencies(allActionJobs);
            allActionJobs.Dispose();

            // Playback entity changes and output state transition info
            var playbackJob = new PlaybackECB()
            {
                ExclusiveEntityTransaction = StateManager.ExclusiveEntityTransaction,
                UnexpandedStates = UnexpandedStates,
                CreatedStateInfo = m_CreatedStateInfo,
                MoveECB = MoveECB,
                EatECB = EatECB,
                DrinkECB = DrinkECB,
                PlayECB = PlayECB,
                SleepECB = SleepECB,
            };

            var playbackJobHandle = playbackJob.Schedule(allActionJobsHandle);
            entityManager.ExclusiveEntityTransactionDependency = playbackJobHandle;

            return playbackJobHandle;
        }
    }
}
