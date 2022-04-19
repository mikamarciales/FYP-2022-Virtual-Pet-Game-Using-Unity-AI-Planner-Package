using System;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.Entities;
using Generated.AI.Planner.StateRepresentation.PetNeeds;

namespace Generated.AI.Planner.Plans.PetNeeds
{
    public class PlanningSystemsProvider : IPlanningSystemsProvider
    {
        public ITraitBasedStateConverter StateConverter => m_StateConverter;
        PlannerStateConverter<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager> m_StateConverter;

        public ITraitBasedPlanExecutor PlanExecutor => m_Executor;
        PetNeedsExecutor m_Executor;

        public IPlannerScheduler PlannerScheduler => m_Scheduler;
        PlannerScheduler<StateEntityKey, ActionKey, StateManager, StateData, StateDataContext, ActionScheduler, DefaultCumulativeRewardEstimator, TerminationEvaluator, DestroyStatesJobScheduler> m_Scheduler;

        public void Initialize(ProblemDefinition problemDefinition, string planningSimulationWorldName)
        {
            var world = new World(planningSimulationWorldName);
            var stateManager = world.GetOrCreateSystem<StateManager>();

            m_StateConverter = new PlannerStateConverter<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager>(problemDefinition, stateManager);

            m_Scheduler = new PlannerScheduler<StateEntityKey, ActionKey, StateManager, StateData, StateDataContext, ActionScheduler, DefaultCumulativeRewardEstimator, TerminationEvaluator, DestroyStatesJobScheduler>();
            m_Scheduler.Initialize(stateManager, new DefaultCumulativeRewardEstimator(), new TerminationEvaluator(), problemDefinition.DiscountFactor);

            m_Executor = new PetNeedsExecutor(stateManager, m_StateConverter);

            // Ensure planning jobs are not running when destroying the state manager
            stateManager.Destroying += () => m_Scheduler.CurrentJobHandle.Complete();
        }
    }
}
