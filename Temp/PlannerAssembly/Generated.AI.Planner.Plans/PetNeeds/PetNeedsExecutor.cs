using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using UnityEngine;
using Generated.AI.Planner.StateRepresentation;
using Generated.AI.Planner.StateRepresentation.PetNeeds;

namespace Generated.AI.Planner.Plans.PetNeeds
{
    public struct DefaultCumulativeRewardEstimator : ICumulativeRewardEstimator<StateData>
    {
        public BoundedValue Evaluate(StateData state)
        {
            return new BoundedValue(-100, 0, 100);
        }
    }

    public struct TerminationEvaluator : ITerminationEvaluator<StateData>
    {
        public bool IsTerminal(StateData state, out float terminalReward)
        {
            terminalReward = 0f;
            var terminal = false;
            
            var DieInstance = new Die();
            if (DieInstance.IsTerminal(state))
            {
                terminal = true;
                terminalReward += DieInstance.TerminalReward(state);
            }
            return terminal;
        }
    }

    class PetNeedsExecutor : BaseTraitBasedPlanExecutor<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager, ActionKey>
    {
        static Dictionary<Guid, string> s_ActionGuidToNameLookup = new Dictionary<Guid,string>()
        {
            { ActionScheduler.EatGuid, nameof(Eat) },
        };

        PlannerStateConverter<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager> m_StateConverter;

        public  PetNeedsExecutor(StateManager stateManager, PlannerStateConverter<TraitBasedObject, StateEntityKey, StateData, StateDataContext, StateManager> stateConverter)
        {
            m_StateManager = stateManager;
            m_StateConverter = stateConverter;
        }

        public override string GetActionName(IActionKey actionKey)
        {
            s_ActionGuidToNameLookup.TryGetValue(((IActionKeyWithGuid)actionKey).ActionGuid, out var name);
            return name;
        }

        protected override void Act(ActionKey actionKey)
        {
            var stateData = m_StateManager.GetStateData(CurrentPlanState, false);
            var actionName = string.Empty;

            switch (actionKey.ActionGuid)
            {
                case var actionGuid when actionGuid == ActionScheduler.EatGuid:
                    actionName = nameof(Eat);
                    break;
            }

            var executeInfos = GetExecutionInfo(actionName);
            if (executeInfos == null)
                return;

            var argumentMapping = executeInfos.GetArgumentValues();
            var arguments = new object[argumentMapping.Count()];
            var i = 0;
            foreach (var argument in argumentMapping)
            {
                var split = argument.Split('.');

                int parameterIndex = -1;
                var traitBasedObjectName = split[0];

                if (string.IsNullOrEmpty(traitBasedObjectName))
                    throw new ArgumentException($"An argument to the '{actionName}' callback on '{m_Actor?.name}' DecisionController is invalid");

                switch (actionName)
                {
                    case nameof(Eat):
                        parameterIndex = Eat.GetIndexForParameterName(traitBasedObjectName);
                        break;
                }

                if (parameterIndex == -1)
                    throw new ArgumentException($"Argument '{traitBasedObjectName}' to the '{actionName}' callback on '{m_Actor?.name}' DecisionController is invalid");

                var traitBasedObjectIndex = actionKey[parameterIndex];
                if (split.Length > 1) // argument is a trait
                {
                    switch (split[1])
                    {
                        case nameof(Pet):
                            var traitPet = stateData.GetTraitOnObjectAtIndex<Pet>(traitBasedObjectIndex);
                            arguments[i] = split.Length == 3 ? traitPet.GetField(split[2]) : traitPet;
                            break;
                        case nameof(Location):
                            var traitLocation = stateData.GetTraitOnObjectAtIndex<Location>(traitBasedObjectIndex);
                            arguments[i] = split.Length == 3 ? traitLocation.GetField(split[2]) : traitLocation;
                            break;
                        case nameof(Tree_Food):
                            var traitTree_Food = stateData.GetTraitOnObjectAtIndex<Tree_Food>(traitBasedObjectIndex);
                            arguments[i] = split.Length == 3 ? traitTree_Food.GetField(split[2]) : traitTree_Food;
                            break;
                        case nameof(Pet_Time):
                            var traitPet_Time = stateData.GetTraitOnObjectAtIndex<Pet_Time>(traitBasedObjectIndex);
                            arguments[i] = split.Length == 3 ? traitPet_Time.GetField(split[2]) : traitPet_Time;
                            break;
                        case nameof(Need):
                            var traitNeed = stateData.GetTraitOnObjectAtIndex<Need>(traitBasedObjectIndex);
                            arguments[i] = split.Length == 3 ? traitNeed.GetField(split[2]) : traitNeed;
                            break;
                    }
                }
                else // argument is an object
                {
                    var planStateId = stateData.GetTraitBasedObjectId(traitBasedObjectIndex);
                    GameObject dataSource;
                    if (m_PlanStateToGameStateIdLookup.TryGetValue(planStateId.Id, out var gameStateId))
                        dataSource = m_StateConverter.GetDataSource(new TraitBasedObjectId { Id = gameStateId });
                    else
                        dataSource = m_StateConverter.GetDataSource(planStateId);

                    Type expectedType = executeInfos.GetParameterType(i);
                    // FIXME - if this is still needed
                    // if (typeof(ITraitBasedObjectData).IsAssignableFrom(expectedType))
                    // {
                    //     arguments[i] = dataSource;
                    // }
                    // else
                    {
                        arguments[i] = null;
                        var obj = dataSource;
                        if (obj != null && obj is GameObject gameObject)
                        {
                            if (expectedType == typeof(GameObject))
                                arguments[i] = gameObject;

                            if (typeof(Component).IsAssignableFrom(expectedType))
                                arguments[i] = gameObject == null ? null : gameObject.GetComponent(expectedType);
                        }
                    }
                }

                i++;
            }

            CurrentActionKey = actionKey;
            StartAction(executeInfos, arguments);
        }

        public override ActionParameterInfo[] GetActionParametersInfo(IStateKey stateKey, IActionKey actionKey)
        {
            string[] parameterNames = {};
            var stateData = m_StateManager.GetStateData((StateEntityKey)stateKey, false);

            switch (((IActionKeyWithGuid)actionKey).ActionGuid)
            {
                 case var actionGuid when actionGuid == ActionScheduler.EatGuid:
                    parameterNames = Eat.parameterNames;
                        break;
            }

            var parameterInfo = new ActionParameterInfo[parameterNames.Length];
            for (var i = 0; i < parameterNames.Length; i++)
            {
                var traitBasedObjectId = stateData.GetTraitBasedObjectId(((ActionKey)actionKey)[i]);

#if DEBUG
                parameterInfo[i] = new ActionParameterInfo { ParameterName = parameterNames[i], TraitObjectName = traitBasedObjectId.Name.ToString(), TraitObjectId = traitBasedObjectId.Id };
#else
                parameterInfo[i] = new ActionParameterInfo { ParameterName = parameterNames[i], TraitObjectName = traitBasedObjectId.ToString(), TraitObjectId = traitBasedObjectId.Id };
#endif
            }

            return parameterInfo;
        }
    }
}
