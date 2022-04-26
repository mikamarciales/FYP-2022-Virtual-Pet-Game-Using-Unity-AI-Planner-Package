using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.AI.Planner;
using Unity.AI.Planner.Traits;
using Unity.AI.Planner.Jobs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using PlanningAgent = Unity.AI.Planner.Traits.PlanningAgent;

namespace Generated.AI.Planner.StateRepresentation.PetAgentPlan
{
    // Plans don't share key types to enforce a specific state representation
    public struct StateEntityKey : IEquatable<StateEntityKey>, IStateKey
    {
        public Entity Entity;
        public int HashCode;

        public bool Equals(StateEntityKey other) => Entity == other.Entity;

        public bool Equals(IStateKey other) => other is StateEntityKey key && Equals(key);

        public override int GetHashCode() => HashCode;

        public override string ToString() => $"StateEntityKey ({Entity} {HashCode})";

        public string Label => $"State{Entity}";
    }

    public static class TraitArrayIndex<T> where T : struct, ITrait
    {
        public static readonly int Index = -1;

        static TraitArrayIndex()
        {
            var typeIndex = TypeManager.GetTypeIndex<T>();
            if (typeIndex == TypeManager.GetTypeIndex<Moveable>())
                Index = 0;
            else if (typeIndex == TypeManager.GetTypeIndex<Location>())
                Index = 1;
            else if (typeIndex == TypeManager.GetTypeIndex<Pet_Time>())
                Index = 2;
            else if (typeIndex == TypeManager.GetTypeIndex<Pet>())
                Index = 3;
            else if (typeIndex == TypeManager.GetTypeIndex<Tree_Food>())
                Index = 4;
            else if (typeIndex == TypeManager.GetTypeIndex<Need>())
                Index = 5;
            else if (typeIndex == TypeManager.GetTypeIndex<Lake_Drink>())
                Index = 6;
            else if (typeIndex == TypeManager.GetTypeIndex<Table_Happiness>())
                Index = 7;
            else if (typeIndex == TypeManager.GetTypeIndex<Home_Energy>())
                Index = 8;
            else if (typeIndex == TypeManager.GetTypeIndex<PlanningAgent>())
                Index = 9;
        }
    }

    [StructLayout(LayoutKind.Sequential, Size=12)]
    public struct TraitBasedObject : ITraitBasedObject, IEquatable<TraitBasedObject>
    {
        public int Length => 10;

        public byte this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return MoveableIndex;
                    case 1:
                        return LocationIndex;
                    case 2:
                        return Pet_TimeIndex;
                    case 3:
                        return PetIndex;
                    case 4:
                        return Tree_FoodIndex;
                    case 5:
                        return NeedIndex;
                    case 6:
                        return Lake_DrinkIndex;
                    case 7:
                        return Table_HappinessIndex;
                    case 8:
                        return Home_EnergyIndex;
                    case 9:
                        return PlanningAgentIndex;
                }

                return Unset;
            }
            set
            {
                switch (i)
                {
                    case 0:
                        MoveableIndex = value;
                        break;
                    case 1:
                        LocationIndex = value;
                        break;
                    case 2:
                        Pet_TimeIndex = value;
                        break;
                    case 3:
                        PetIndex = value;
                        break;
                    case 4:
                        Tree_FoodIndex = value;
                        break;
                    case 5:
                        NeedIndex = value;
                        break;
                    case 6:
                        Lake_DrinkIndex = value;
                        break;
                    case 7:
                        Table_HappinessIndex = value;
                        break;
                    case 8:
                        Home_EnergyIndex = value;
                        break;
                    case 9:
                        PlanningAgentIndex = value;
                        break;
                }
            }
        }

        public static readonly byte Unset = Byte.MaxValue;

        public static TraitBasedObject Default => new TraitBasedObject
        {
            MoveableIndex = Unset,
            LocationIndex = Unset,
            Pet_TimeIndex = Unset,
            PetIndex = Unset,
            Tree_FoodIndex = Unset,
            NeedIndex = Unset,
            Lake_DrinkIndex = Unset,
            Table_HappinessIndex = Unset,
            Home_EnergyIndex = Unset,
            PlanningAgentIndex = Unset,
        };


        public byte MoveableIndex;
        public byte LocationIndex;
        public byte Pet_TimeIndex;
        public byte PetIndex;
        public byte Tree_FoodIndex;
        public byte NeedIndex;
        public byte Lake_DrinkIndex;
        public byte Table_HappinessIndex;
        public byte Home_EnergyIndex;
        public byte PlanningAgentIndex;


        static readonly int s_MoveableTypeIndex = TypeManager.GetTypeIndex<Moveable>();
        static readonly int s_LocationTypeIndex = TypeManager.GetTypeIndex<Location>();
        static readonly int s_Pet_TimeTypeIndex = TypeManager.GetTypeIndex<Pet_Time>();
        static readonly int s_PetTypeIndex = TypeManager.GetTypeIndex<Pet>();
        static readonly int s_Tree_FoodTypeIndex = TypeManager.GetTypeIndex<Tree_Food>();
        static readonly int s_NeedTypeIndex = TypeManager.GetTypeIndex<Need>();
        static readonly int s_Lake_DrinkTypeIndex = TypeManager.GetTypeIndex<Lake_Drink>();
        static readonly int s_Table_HappinessTypeIndex = TypeManager.GetTypeIndex<Table_Happiness>();
        static readonly int s_Home_EnergyTypeIndex = TypeManager.GetTypeIndex<Home_Energy>();
        static readonly int s_PlanningAgentTypeIndex = TypeManager.GetTypeIndex<PlanningAgent>();

        public bool HasSameTraits(TraitBasedObject other)
        {
            for (var i = 0; i < Length; i++)
            {
                var traitIndex = this[i];
                var otherTraitIndex = other[i];
                if (traitIndex == Unset && otherTraitIndex != Unset || traitIndex != Unset && otherTraitIndex == Unset)
                    return false;
            }
            return true;
        }

        public bool HasTraitSubset(TraitBasedObject traitSubset)
        {
            for (var i = 0; i < Length; i++)
            {
                var requiredTrait = traitSubset[i];
                if (requiredTrait != Unset && this[i] == Unset)
                    return false;
            }
            return true;
        }

        // todo - replace with more efficient subset check
        public bool MatchesTraitFilter(NativeArray<ComponentType> componentTypes)
        {
            for (int i = 0; i < componentTypes.Length; i++)
            {
                var t = componentTypes[i];
                if (t == default || t.TypeIndex == 0)
                {
                    // This seems to be necessary for Burst compilation; Doesn't happen with non-Burst compilation
                }
                else if (t.TypeIndex == s_MoveableTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ MoveableIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_LocationTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ LocationIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Pet_TimeTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Pet_TimeIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_PetTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ PetIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Tree_FoodTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Tree_FoodIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_NeedTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ NeedIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Lake_DrinkTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Lake_DrinkIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Table_HappinessTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Table_HappinessIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Home_EnergyTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Home_EnergyIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_PlanningAgentTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ PlanningAgentIndex == Unset)
                        return false;
                }
                else
                    throw new ArgumentException($"Incorrect trait type used in object query: {t}");
            }

            return true;
        }

        public bool MatchesTraitFilter(ComponentType[] componentTypes)
        {
            for (int i = 0; i < componentTypes.Length; i++)
            {
                var t = componentTypes[i];
                if (t == default || t == null || t.TypeIndex == 0)
                {
                    // This seems to be necessary for Burst compilation; Doesn't happen with non-Burst compilation
                }
                else if (t.TypeIndex == s_MoveableTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ MoveableIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_LocationTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ LocationIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Pet_TimeTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Pet_TimeIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_PetTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ PetIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Tree_FoodTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Tree_FoodIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_NeedTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ NeedIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Lake_DrinkTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Lake_DrinkIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Table_HappinessTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Table_HappinessIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_Home_EnergyTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ Home_EnergyIndex == Unset)
                        return false;
                }
                else if (t.TypeIndex == s_PlanningAgentTypeIndex)
                {
                    if (t.AccessModeType == ComponentType.AccessMode.Exclude ^ PlanningAgentIndex == Unset)
                        return false;
                }
                else
                    throw new ArgumentException($"Incorrect trait type used in object query: {t}");
            }

            return true;
        }

        public bool Equals(TraitBasedObject other)
        {

                return MoveableIndex == other.MoveableIndex && LocationIndex == other.LocationIndex && Pet_TimeIndex == other.Pet_TimeIndex && PetIndex == other.PetIndex && Tree_FoodIndex == other.Tree_FoodIndex && NeedIndex == other.NeedIndex && Lake_DrinkIndex == other.Lake_DrinkIndex && Table_HappinessIndex == other.Table_HappinessIndex && Home_EnergyIndex == other.Home_EnergyIndex && PlanningAgentIndex == other.PlanningAgentIndex;
        }

        public override bool Equals(object obj)
        {
            return obj is TraitBasedObject other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {

                    var hashCode = MoveableIndex.GetHashCode();
                    
                     hashCode = (hashCode * 397) ^ LocationIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ Pet_TimeIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ PetIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ Tree_FoodIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ NeedIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ Lake_DrinkIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ Table_HappinessIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ Home_EnergyIndex.GetHashCode();
                     hashCode = (hashCode * 397) ^ PlanningAgentIndex.GetHashCode();
                return hashCode;
            }
        }
    }

    public struct StateData : ITraitBasedStateData<TraitBasedObject, StateData>
    {
        public Entity StateEntity;
        public DynamicBuffer<TraitBasedObject> TraitBasedObjects;
        public DynamicBuffer<TraitBasedObjectId> TraitBasedObjectIds;

        public DynamicBuffer<Moveable> MoveableBuffer;
        public DynamicBuffer<Location> LocationBuffer;
        public DynamicBuffer<Pet_Time> Pet_TimeBuffer;
        public DynamicBuffer<Pet> PetBuffer;
        public DynamicBuffer<Tree_Food> Tree_FoodBuffer;
        public DynamicBuffer<Need> NeedBuffer;
        public DynamicBuffer<Lake_Drink> Lake_DrinkBuffer;
        public DynamicBuffer<Table_Happiness> Table_HappinessBuffer;
        public DynamicBuffer<Home_Energy> Home_EnergyBuffer;
        public DynamicBuffer<PlanningAgent> PlanningAgentBuffer;

        static readonly int s_MoveableTypeIndex = TypeManager.GetTypeIndex<Moveable>();
        static readonly int s_LocationTypeIndex = TypeManager.GetTypeIndex<Location>();
        static readonly int s_Pet_TimeTypeIndex = TypeManager.GetTypeIndex<Pet_Time>();
        static readonly int s_PetTypeIndex = TypeManager.GetTypeIndex<Pet>();
        static readonly int s_Tree_FoodTypeIndex = TypeManager.GetTypeIndex<Tree_Food>();
        static readonly int s_NeedTypeIndex = TypeManager.GetTypeIndex<Need>();
        static readonly int s_Lake_DrinkTypeIndex = TypeManager.GetTypeIndex<Lake_Drink>();
        static readonly int s_Table_HappinessTypeIndex = TypeManager.GetTypeIndex<Table_Happiness>();
        static readonly int s_Home_EnergyTypeIndex = TypeManager.GetTypeIndex<Home_Energy>();
        static readonly int s_PlanningAgentTypeIndex = TypeManager.GetTypeIndex<PlanningAgent>();

        public StateData(ExclusiveEntityTransaction transaction, Entity stateEntity)
        {
            StateEntity = stateEntity;
            TraitBasedObjects = transaction.GetBuffer<TraitBasedObject>(stateEntity);
            TraitBasedObjectIds = transaction.GetBuffer<TraitBasedObjectId>(stateEntity);

            MoveableBuffer = transaction.GetBuffer<Moveable>(stateEntity);
            LocationBuffer = transaction.GetBuffer<Location>(stateEntity);
            Pet_TimeBuffer = transaction.GetBuffer<Pet_Time>(stateEntity);
            PetBuffer = transaction.GetBuffer<Pet>(stateEntity);
            Tree_FoodBuffer = transaction.GetBuffer<Tree_Food>(stateEntity);
            NeedBuffer = transaction.GetBuffer<Need>(stateEntity);
            Lake_DrinkBuffer = transaction.GetBuffer<Lake_Drink>(stateEntity);
            Table_HappinessBuffer = transaction.GetBuffer<Table_Happiness>(stateEntity);
            Home_EnergyBuffer = transaction.GetBuffer<Home_Energy>(stateEntity);
            PlanningAgentBuffer = transaction.GetBuffer<PlanningAgent>(stateEntity);
        }

        public StateData(int jobIndex, EntityCommandBuffer.ParallelWriter entityCommandBuffer, Entity stateEntity)
        {
            StateEntity = stateEntity;
            TraitBasedObjects = entityCommandBuffer.AddBuffer<TraitBasedObject>(jobIndex, stateEntity);
            TraitBasedObjectIds = entityCommandBuffer.AddBuffer<TraitBasedObjectId>(jobIndex, stateEntity);

            MoveableBuffer = entityCommandBuffer.AddBuffer<Moveable>(jobIndex, stateEntity);
            LocationBuffer = entityCommandBuffer.AddBuffer<Location>(jobIndex, stateEntity);
            Pet_TimeBuffer = entityCommandBuffer.AddBuffer<Pet_Time>(jobIndex, stateEntity);
            PetBuffer = entityCommandBuffer.AddBuffer<Pet>(jobIndex, stateEntity);
            Tree_FoodBuffer = entityCommandBuffer.AddBuffer<Tree_Food>(jobIndex, stateEntity);
            NeedBuffer = entityCommandBuffer.AddBuffer<Need>(jobIndex, stateEntity);
            Lake_DrinkBuffer = entityCommandBuffer.AddBuffer<Lake_Drink>(jobIndex, stateEntity);
            Table_HappinessBuffer = entityCommandBuffer.AddBuffer<Table_Happiness>(jobIndex, stateEntity);
            Home_EnergyBuffer = entityCommandBuffer.AddBuffer<Home_Energy>(jobIndex, stateEntity);
            PlanningAgentBuffer = entityCommandBuffer.AddBuffer<PlanningAgent>(jobIndex, stateEntity);
        }

        public StateData Copy(int jobIndex, EntityCommandBuffer.ParallelWriter entityCommandBuffer)
        {
            var stateEntity = entityCommandBuffer.Instantiate(jobIndex, StateEntity);
            var traitBasedObjects = entityCommandBuffer.SetBuffer<TraitBasedObject>(jobIndex, stateEntity);
            traitBasedObjects.CopyFrom(TraitBasedObjects.AsNativeArray());
            var traitBasedObjectIds = entityCommandBuffer.SetBuffer<TraitBasedObjectId>(jobIndex, stateEntity);
            traitBasedObjectIds.CopyFrom(TraitBasedObjectIds.AsNativeArray());

            var Moveables = entityCommandBuffer.SetBuffer<Moveable>(jobIndex, stateEntity);
            Moveables.CopyFrom(MoveableBuffer.AsNativeArray());
            var Locations = entityCommandBuffer.SetBuffer<Location>(jobIndex, stateEntity);
            Locations.CopyFrom(LocationBuffer.AsNativeArray());
            var Pet_Times = entityCommandBuffer.SetBuffer<Pet_Time>(jobIndex, stateEntity);
            Pet_Times.CopyFrom(Pet_TimeBuffer.AsNativeArray());
            var Pets = entityCommandBuffer.SetBuffer<Pet>(jobIndex, stateEntity);
            Pets.CopyFrom(PetBuffer.AsNativeArray());
            var Tree_Foods = entityCommandBuffer.SetBuffer<Tree_Food>(jobIndex, stateEntity);
            Tree_Foods.CopyFrom(Tree_FoodBuffer.AsNativeArray());
            var Needs = entityCommandBuffer.SetBuffer<Need>(jobIndex, stateEntity);
            Needs.CopyFrom(NeedBuffer.AsNativeArray());
            var Lake_Drinks = entityCommandBuffer.SetBuffer<Lake_Drink>(jobIndex, stateEntity);
            Lake_Drinks.CopyFrom(Lake_DrinkBuffer.AsNativeArray());
            var Table_Happinesss = entityCommandBuffer.SetBuffer<Table_Happiness>(jobIndex, stateEntity);
            Table_Happinesss.CopyFrom(Table_HappinessBuffer.AsNativeArray());
            var Home_Energys = entityCommandBuffer.SetBuffer<Home_Energy>(jobIndex, stateEntity);
            Home_Energys.CopyFrom(Home_EnergyBuffer.AsNativeArray());
            var PlanningAgents = entityCommandBuffer.SetBuffer<PlanningAgent>(jobIndex, stateEntity);
            PlanningAgents.CopyFrom(PlanningAgentBuffer.AsNativeArray());

            return new StateData
            {
                StateEntity = stateEntity,
                TraitBasedObjects = traitBasedObjects,
                TraitBasedObjectIds = traitBasedObjectIds,

                MoveableBuffer = Moveables,
                LocationBuffer = Locations,
                Pet_TimeBuffer = Pet_Times,
                PetBuffer = Pets,
                Tree_FoodBuffer = Tree_Foods,
                NeedBuffer = Needs,
                Lake_DrinkBuffer = Lake_Drinks,
                Table_HappinessBuffer = Table_Happinesss,
                Home_EnergyBuffer = Home_Energys,
                PlanningAgentBuffer = PlanningAgents,
            };
        }

        public void AddObject(NativeArray<ComponentType> types, out TraitBasedObject traitBasedObject, TraitBasedObjectId objectId, FixedString64 name = default)
        {
            traitBasedObject = TraitBasedObject.Default;
#if DEBUG
            objectId.Name.CopyFrom(name);
#endif

            for (int i = 0; i < types.Length; i++)
            {
                var t = types[i];
                if (t.TypeIndex == s_MoveableTypeIndex)
                {
                    MoveableBuffer.Add(default);
                    traitBasedObject.MoveableIndex = (byte) (MoveableBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_LocationTypeIndex)
                {
                    LocationBuffer.Add(default);
                    traitBasedObject.LocationIndex = (byte) (LocationBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_Pet_TimeTypeIndex)
                {
                    Pet_TimeBuffer.Add(default);
                    traitBasedObject.Pet_TimeIndex = (byte) (Pet_TimeBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_PetTypeIndex)
                {
                    PetBuffer.Add(default);
                    traitBasedObject.PetIndex = (byte) (PetBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_Tree_FoodTypeIndex)
                {
                    Tree_FoodBuffer.Add(default);
                    traitBasedObject.Tree_FoodIndex = (byte) (Tree_FoodBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_NeedTypeIndex)
                {
                    NeedBuffer.Add(default);
                    traitBasedObject.NeedIndex = (byte) (NeedBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_Lake_DrinkTypeIndex)
                {
                    Lake_DrinkBuffer.Add(default);
                    traitBasedObject.Lake_DrinkIndex = (byte) (Lake_DrinkBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_Table_HappinessTypeIndex)
                {
                    Table_HappinessBuffer.Add(default);
                    traitBasedObject.Table_HappinessIndex = (byte) (Table_HappinessBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_Home_EnergyTypeIndex)
                {
                    Home_EnergyBuffer.Add(default);
                    traitBasedObject.Home_EnergyIndex = (byte) (Home_EnergyBuffer.Length - 1);
                }
                else if (t.TypeIndex == s_PlanningAgentTypeIndex)
                {
                    PlanningAgentBuffer.Add(default);
                    traitBasedObject.PlanningAgentIndex = (byte) (PlanningAgentBuffer.Length - 1);
                }
            }

            TraitBasedObjectIds.Add(objectId);
            TraitBasedObjects.Add(traitBasedObject);
        }

        public void AddObject(NativeArray<ComponentType> types, out TraitBasedObject traitBasedObject, out TraitBasedObjectId objectId, FixedString64 name = default)
        {
            objectId = new TraitBasedObjectId() { Id = ObjectId.GetNext() };
            AddObject(types, out traitBasedObject, objectId, name);
        }

        public void ConvertAndSetPlannerTrait(Entity sourceEntity, EntityManager sourceEntityManager,
            NativeArray<ComponentType> sourceTraitTypes, IDictionary<Entity, TraitBasedObjectId> entityToObjectId,
            ref TraitBasedObject traitBasedObject)
        {
            unsafe
            {
                foreach (var type in sourceTraitTypes)
                {
                    if (type == typeof(Generated.Semantic.Traits.LocationData))
                    {
                        var traitData = sourceEntityManager.GetComponentData<Generated.Semantic.Traits.LocationData>(sourceEntity);
                        var plannerTraitData = new Location();
                        UnsafeUtility.CopyStructureToPtr(ref traitData, UnsafeUtility.AddressOf(ref plannerTraitData));
                        SetTraitOnObject(plannerTraitData, ref traitBasedObject);
                    }
                    if (type == typeof(Generated.Semantic.Traits.Pet_TimeData))
                    {
                        var traitData = sourceEntityManager.GetComponentData<Generated.Semantic.Traits.Pet_TimeData>(sourceEntity);
                        var plannerTraitData = new Pet_Time();
                        UnsafeUtility.CopyStructureToPtr(ref traitData, UnsafeUtility.AddressOf(ref plannerTraitData));
                        SetTraitOnObject(plannerTraitData, ref traitBasedObject);
                    }
                    if (type == typeof(Generated.Semantic.Traits.NeedData))
                    {
                        var traitData = sourceEntityManager.GetComponentData<Generated.Semantic.Traits.NeedData>(sourceEntity);
                        var plannerTraitData = new Need();
                        UnsafeUtility.CopyStructureToPtr(ref traitData, UnsafeUtility.AddressOf(ref plannerTraitData));
                        SetTraitOnObject(plannerTraitData, ref traitBasedObject);
                    }
                }
            }
        }

        public void SetTraitOnObject(ITrait trait, ref TraitBasedObject traitBasedObject)
        {
            if (trait is Moveable MoveableTrait)
                SetTraitOnObject(MoveableTrait, ref traitBasedObject);
            else if (trait is Location LocationTrait)
                SetTraitOnObject(LocationTrait, ref traitBasedObject);
            else if (trait is Pet_Time Pet_TimeTrait)
                SetTraitOnObject(Pet_TimeTrait, ref traitBasedObject);
            else if (trait is Pet PetTrait)
                SetTraitOnObject(PetTrait, ref traitBasedObject);
            else if (trait is Tree_Food Tree_FoodTrait)
                SetTraitOnObject(Tree_FoodTrait, ref traitBasedObject);
            else if (trait is Need NeedTrait)
                SetTraitOnObject(NeedTrait, ref traitBasedObject);
            else if (trait is Lake_Drink Lake_DrinkTrait)
                SetTraitOnObject(Lake_DrinkTrait, ref traitBasedObject);
            else if (trait is Table_Happiness Table_HappinessTrait)
                SetTraitOnObject(Table_HappinessTrait, ref traitBasedObject);
            else if (trait is Home_Energy Home_EnergyTrait)
                SetTraitOnObject(Home_EnergyTrait, ref traitBasedObject);
            else if (trait is PlanningAgent PlanningAgentTrait)
                SetTraitOnObject(PlanningAgentTrait, ref traitBasedObject);
            else 
                throw new ArgumentException($"Trait {trait} of type {trait.GetType()} is not supported in this state representation.");
        }

        public void SetTraitOnObjectAtIndex(ITrait trait, int traitBasedObjectIndex)
        {
            if (trait is Moveable MoveableTrait)
                SetTraitOnObjectAtIndex(MoveableTrait, traitBasedObjectIndex);
            else if (trait is Location LocationTrait)
                SetTraitOnObjectAtIndex(LocationTrait, traitBasedObjectIndex);
            else if (trait is Pet_Time Pet_TimeTrait)
                SetTraitOnObjectAtIndex(Pet_TimeTrait, traitBasedObjectIndex);
            else if (trait is Pet PetTrait)
                SetTraitOnObjectAtIndex(PetTrait, traitBasedObjectIndex);
            else if (trait is Tree_Food Tree_FoodTrait)
                SetTraitOnObjectAtIndex(Tree_FoodTrait, traitBasedObjectIndex);
            else if (trait is Need NeedTrait)
                SetTraitOnObjectAtIndex(NeedTrait, traitBasedObjectIndex);
            else if (trait is Lake_Drink Lake_DrinkTrait)
                SetTraitOnObjectAtIndex(Lake_DrinkTrait, traitBasedObjectIndex);
            else if (trait is Table_Happiness Table_HappinessTrait)
                SetTraitOnObjectAtIndex(Table_HappinessTrait, traitBasedObjectIndex);
            else if (trait is Home_Energy Home_EnergyTrait)
                SetTraitOnObjectAtIndex(Home_EnergyTrait, traitBasedObjectIndex);
            else if (trait is PlanningAgent PlanningAgentTrait)
                SetTraitOnObjectAtIndex(PlanningAgentTrait, traitBasedObjectIndex);
            else 
                throw new ArgumentException($"Trait {trait} of type {trait.GetType()} is not supported in this state representation.");
        }


        public TTrait GetTraitOnObject<TTrait>(TraitBasedObject traitBasedObject) where TTrait : struct, ITrait
        {
            var traitBasedObjectTraitIndex = TraitArrayIndex<TTrait>.Index;
            if (traitBasedObjectTraitIndex == -1)
                throw new ArgumentException($"Trait {typeof(TTrait)} not supported in this plan");

            var traitBufferIndex = traitBasedObject[traitBasedObjectTraitIndex];
            if (traitBufferIndex == TraitBasedObject.Unset)
                throw new ArgumentException($"Trait of type {typeof(TTrait)} does not exist on object {traitBasedObject}.");

            return GetBuffer<TTrait>()[traitBufferIndex];
        }

        public bool HasTraitOnObject<TTrait>(TraitBasedObject traitBasedObject) where TTrait : struct, ITrait
        {
            var traitBasedObjectTraitIndex = TraitArrayIndex<TTrait>.Index;
            if (traitBasedObjectTraitIndex == -1)
                throw new ArgumentException($"Trait {typeof(TTrait)} not supported in this plan");

            var traitBufferIndex = traitBasedObject[traitBasedObjectTraitIndex];
            return traitBufferIndex != TraitBasedObject.Unset;
        }

        public void SetTraitOnObject<TTrait>(TTrait trait, ref TraitBasedObject traitBasedObject) where TTrait : struct, ITrait
        {
            var objectIndex = GetTraitBasedObjectIndex(traitBasedObject);
            if (objectIndex == -1)
                throw new ArgumentException($"Object {traitBasedObject} does not exist within the state data {this}.");

            var traitIndex = TraitArrayIndex<TTrait>.Index;
            var traitBuffer = GetBuffer<TTrait>();

            var bufferIndex = traitBasedObject[traitIndex];
            if (bufferIndex == TraitBasedObject.Unset)
            {
                traitBuffer.Add(trait);
                traitBasedObject[traitIndex] = (byte) (traitBuffer.Length - 1);

                TraitBasedObjects[objectIndex] = traitBasedObject;
            }
            else
            {
                traitBuffer[bufferIndex] = trait;
            }
        }

        public bool RemoveTraitOnObject<TTrait>(ref TraitBasedObject traitBasedObject) where TTrait : struct, ITrait
        {
            var objectTraitIndex = TraitArrayIndex<TTrait>.Index;
            var traitBuffer = GetBuffer<TTrait>();

            var traitBufferIndex = traitBasedObject[objectTraitIndex];
            if (traitBufferIndex == TraitBasedObject.Unset)
                return false;

            // last index
            var lastBufferIndex = traitBuffer.Length - 1;

            // Swap back and remove
            var lastTrait = traitBuffer[lastBufferIndex];
            traitBuffer[lastBufferIndex] = traitBuffer[traitBufferIndex];
            traitBuffer[traitBufferIndex] = lastTrait;
            traitBuffer.RemoveAt(lastBufferIndex);

            // Update index for object with last trait in buffer
            var numObjects = TraitBasedObjects.Length;
            for (int i = 0; i < numObjects; i++)
            {
                var otherTraitBasedObject = TraitBasedObjects[i];
                if (otherTraitBasedObject[objectTraitIndex] == lastBufferIndex)
                {
                    otherTraitBasedObject[objectTraitIndex] = traitBufferIndex;
                    TraitBasedObjects[i] = otherTraitBasedObject;
                    break;
                }
            }

            // Update traitBasedObject in buffer (ref is to a copy)
            for (int i = 0; i < numObjects; i++)
            {
                if (traitBasedObject.Equals(TraitBasedObjects[i]))
                {
                    traitBasedObject[objectTraitIndex] = TraitBasedObject.Unset;
                    TraitBasedObjects[i] = traitBasedObject;
                    return true;
                }
            }

            throw new ArgumentException($"TraitBasedObject {traitBasedObject} does not exist in the state container {this}.");
        }

        public bool RemoveObject(TraitBasedObject traitBasedObject)
        {
            var objectIndex = GetTraitBasedObjectIndex(traitBasedObject);
            if (objectIndex == -1)
                return false;


            RemoveTraitOnObject<Moveable>(ref traitBasedObject);
            RemoveTraitOnObject<Location>(ref traitBasedObject);
            RemoveTraitOnObject<Pet_Time>(ref traitBasedObject);
            RemoveTraitOnObject<Pet>(ref traitBasedObject);
            RemoveTraitOnObject<Tree_Food>(ref traitBasedObject);
            RemoveTraitOnObject<Need>(ref traitBasedObject);
            RemoveTraitOnObject<Lake_Drink>(ref traitBasedObject);
            RemoveTraitOnObject<Table_Happiness>(ref traitBasedObject);
            RemoveTraitOnObject<Home_Energy>(ref traitBasedObject);
            RemoveTraitOnObject<PlanningAgent>(ref traitBasedObject);

            TraitBasedObjects.RemoveAt(objectIndex);
            TraitBasedObjectIds.RemoveAt(objectIndex);

            return true;
        }


        public TTrait GetTraitOnObjectAtIndex<TTrait>(int traitBasedObjectIndex) where TTrait : struct, ITrait
        {
            var traitBasedObjectTraitIndex = TraitArrayIndex<TTrait>.Index;
            if (traitBasedObjectTraitIndex == -1)
                throw new ArgumentException($"Trait {typeof(TTrait)} not supported in this state representation");

            var traitBasedObject = TraitBasedObjects[traitBasedObjectIndex];
            var traitBufferIndex = traitBasedObject[traitBasedObjectTraitIndex];
            if (traitBufferIndex == TraitBasedObject.Unset)
                throw new Exception($"Trait index for {typeof(TTrait)} is not set for object {traitBasedObject}");

            return GetBuffer<TTrait>()[traitBufferIndex];
        }

        public void SetTraitOnObjectAtIndex<T>(T trait, int traitBasedObjectIndex) where T : struct, ITrait
        {
            var traitBasedObjectTraitIndex = TraitArrayIndex<T>.Index;
            if (traitBasedObjectTraitIndex == -1)
                throw new ArgumentException($"Trait {typeof(T)} not supported in this state representation");

            var traitBasedObject = TraitBasedObjects[traitBasedObjectIndex];
            var traitBufferIndex = traitBasedObject[traitBasedObjectTraitIndex];
            var traitBuffer = GetBuffer<T>();
            if (traitBufferIndex == TraitBasedObject.Unset)
            {
                traitBuffer.Add(trait);
                traitBufferIndex = (byte)(traitBuffer.Length - 1);
                traitBasedObject[traitBasedObjectTraitIndex] = traitBufferIndex;
                TraitBasedObjects[traitBasedObjectIndex] = traitBasedObject;
            }
            else
            {
                traitBuffer[traitBufferIndex] = trait;
            }
        }

        public bool RemoveTraitOnObjectAtIndex<TTrait>(int traitBasedObjectIndex) where TTrait : struct, ITrait
        {
            var objectTraitIndex = TraitArrayIndex<TTrait>.Index;
            var traitBuffer = GetBuffer<TTrait>();

            var traitBasedObject = TraitBasedObjects[traitBasedObjectIndex];
            var traitBufferIndex = traitBasedObject[objectTraitIndex];
            if (traitBufferIndex == TraitBasedObject.Unset)
                return false;

            // last index
            var lastBufferIndex = traitBuffer.Length - 1;

            // Swap back and remove
            var lastTrait = traitBuffer[lastBufferIndex];
            traitBuffer[lastBufferIndex] = traitBuffer[traitBufferIndex];
            traitBuffer[traitBufferIndex] = lastTrait;
            traitBuffer.RemoveAt(lastBufferIndex);

            // Update index for object with last trait in buffer
            var numObjects = TraitBasedObjects.Length;
            for (int i = 0; i < numObjects; i++)
            {
                var otherTraitBasedObject = TraitBasedObjects[i];
                if (otherTraitBasedObject[objectTraitIndex] == lastBufferIndex)
                {
                    otherTraitBasedObject[objectTraitIndex] = traitBufferIndex;
                    TraitBasedObjects[i] = otherTraitBasedObject;
                    break;
                }
            }

            traitBasedObject[objectTraitIndex] = TraitBasedObject.Unset;
            TraitBasedObjects[traitBasedObjectIndex] = traitBasedObject;

            return true;
        }

        public bool RemoveTraitBasedObjectAtIndex(int traitBasedObjectIndex)
        {
            RemoveTraitOnObjectAtIndex<Moveable>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Location>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Pet_Time>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Pet>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Tree_Food>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Need>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Lake_Drink>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Table_Happiness>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<Home_Energy>(traitBasedObjectIndex);
            RemoveTraitOnObjectAtIndex<PlanningAgent>(traitBasedObjectIndex);

            TraitBasedObjects.RemoveAt(traitBasedObjectIndex);
            TraitBasedObjectIds.RemoveAt(traitBasedObjectIndex);

            return true;
        }


        public NativeArray<int> GetTraitBasedObjectIndices(NativeList<int> traitBasedObjectIndices, NativeArray<ComponentType> traitFilter)
        {
            var numObjects = TraitBasedObjects.Length;
            for (var i = 0; i < numObjects; i++)
            {
                var traitBasedObject = TraitBasedObjects[i];
                if (traitBasedObject.MatchesTraitFilter(traitFilter))
                    traitBasedObjectIndices.Add(i);
            }

            return traitBasedObjectIndices.AsArray();
        }

        public NativeArray<int> GetTraitBasedObjectIndices(NativeList<int> traitBasedObjectIndices, params ComponentType[] traitFilter)
        {
            var numObjects = TraitBasedObjects.Length;
            for (var i = 0; i < numObjects; i++)
            {
                var traitBasedObject = TraitBasedObjects[i];
                if (traitBasedObject.MatchesTraitFilter(traitFilter))
                    traitBasedObjectIndices.Add(i);
            }

            return traitBasedObjectIndices.AsArray();
        }

        public int GetTraitBasedObjectIndex(TraitBasedObject traitBasedObject)
        {
            for (int objectIndex = TraitBasedObjects.Length - 1; objectIndex >= 0; objectIndex--)
            {
                bool match = true;
                var other = TraitBasedObjects[objectIndex];
                for (int i = 0; i < traitBasedObject.Length && match; i++)
                {
                    match &= traitBasedObject[i] == other[i];
                }

                if (match)
                    return objectIndex;
            }

            return -1;
        }

        public int GetTraitBasedObjectIndex(TraitBasedObjectId traitBasedObjectId)
        {
            var objectIndex = -1;
            for (int i = TraitBasedObjectIds.Length - 1; i >= 0; i--)
            {
                if (TraitBasedObjectIds[i].Equals(traitBasedObjectId))
                {
                    objectIndex = i;
                    break;
                }
            }

            return objectIndex;
        }

        public TraitBasedObjectId GetTraitBasedObjectId(TraitBasedObject traitBasedObject)
        {
            var index = GetTraitBasedObjectIndex(traitBasedObject);
            return TraitBasedObjectIds[index];
        }

        public TraitBasedObjectId GetTraitBasedObjectId(int traitBasedObjectIndex)
        {
            return TraitBasedObjectIds[traitBasedObjectIndex];
        }

        public TraitBasedObject GetTraitBasedObject(TraitBasedObjectId traitBasedObject)
        {
            var index = GetTraitBasedObjectIndex(traitBasedObject);
            return TraitBasedObjects[index];
        }


        DynamicBuffer<T> GetBuffer<T>() where T : struct, ITrait
        {
            var index = TraitArrayIndex<T>.Index;
            switch (index)
            {
                case 0:
                    return MoveableBuffer.Reinterpret<T>();
                case 1:
                    return LocationBuffer.Reinterpret<T>();
                case 2:
                    return Pet_TimeBuffer.Reinterpret<T>();
                case 3:
                    return PetBuffer.Reinterpret<T>();
                case 4:
                    return Tree_FoodBuffer.Reinterpret<T>();
                case 5:
                    return NeedBuffer.Reinterpret<T>();
                case 6:
                    return Lake_DrinkBuffer.Reinterpret<T>();
                case 7:
                    return Table_HappinessBuffer.Reinterpret<T>();
                case 8:
                    return Home_EnergyBuffer.Reinterpret<T>();
                case 9:
                    return PlanningAgentBuffer.Reinterpret<T>();
            }

            return default;
        }

        public bool Equals(IStateData other) => other is StateData otherData && Equals(otherData);

        public bool Equals(StateData rhsState)
        {
            if (StateEntity == rhsState.StateEntity)
                return true;

            // Easy check is to make sure each state has the same number of trait-based objects
            if (TraitBasedObjects.Length != rhsState.TraitBasedObjects.Length
                || MoveableBuffer.Length != rhsState.MoveableBuffer.Length
                || LocationBuffer.Length != rhsState.LocationBuffer.Length
                || Pet_TimeBuffer.Length != rhsState.Pet_TimeBuffer.Length
                || PetBuffer.Length != rhsState.PetBuffer.Length
                || Tree_FoodBuffer.Length != rhsState.Tree_FoodBuffer.Length
                || NeedBuffer.Length != rhsState.NeedBuffer.Length
                || Lake_DrinkBuffer.Length != rhsState.Lake_DrinkBuffer.Length
                || Table_HappinessBuffer.Length != rhsState.Table_HappinessBuffer.Length
                || Home_EnergyBuffer.Length != rhsState.Home_EnergyBuffer.Length
                || PlanningAgentBuffer.Length != rhsState.PlanningAgentBuffer.Length)
                return false;

            var objectMap = new ObjectCorrespondence(TraitBasedObjectIds.Length, Allocator.Temp);
            bool statesEqual = TryGetObjectMapping(rhsState, objectMap);
            objectMap.Dispose();

            return statesEqual;
        }

        bool ITraitBasedStateData<TraitBasedObject, StateData>.TryGetObjectMapping(StateData rhsState, ObjectCorrespondence objectMap)
        {
            // Easy check is to make sure each state has the same number of domain objects
            if (TraitBasedObjects.Length != rhsState.TraitBasedObjects.Length
                || MoveableBuffer.Length != rhsState.MoveableBuffer.Length
                || LocationBuffer.Length != rhsState.LocationBuffer.Length
                || Pet_TimeBuffer.Length != rhsState.Pet_TimeBuffer.Length
                || PetBuffer.Length != rhsState.PetBuffer.Length
                || Tree_FoodBuffer.Length != rhsState.Tree_FoodBuffer.Length
                || NeedBuffer.Length != rhsState.NeedBuffer.Length
                || Lake_DrinkBuffer.Length != rhsState.Lake_DrinkBuffer.Length
                || Table_HappinessBuffer.Length != rhsState.Table_HappinessBuffer.Length
                || Home_EnergyBuffer.Length != rhsState.Home_EnergyBuffer.Length
                || PlanningAgentBuffer.Length != rhsState.PlanningAgentBuffer.Length)
                return false;

            return TryGetObjectMapping(rhsState, objectMap);
        }

        internal bool TryGetObjectMapping(StateData rhsState, ObjectCorrespondence objectMap)
        {
            objectMap.Initialize(TraitBasedObjectIds, rhsState.TraitBasedObjectIds);

            bool statesEqual = true;
            var numObjects = TraitBasedObjects.Length;
            for (int lhsIndex = 0; lhsIndex < numObjects; lhsIndex++)
            {
                var lhsId = TraitBasedObjectIds[lhsIndex].Id;
                if (objectMap.TryGetValue(lhsId, out _)) // already matched
                    continue;

                // todo lhsIndex to start? would require swapping rhs on assignments, though
                bool matchFound = true;

                for (var rhsIndex = 0; rhsIndex < numObjects; rhsIndex++)
                {
                    var rhsId = rhsState.TraitBasedObjectIds[rhsIndex].Id;
                    if (objectMap.ContainsRHS(rhsId)) // skip if already assigned todo optimize this
                        continue;

                    objectMap.BeginNewTraversal();
                    objectMap.Add(lhsId, rhsId);

                    // Traversal comparing all reachable objects
                    matchFound = true;
                    while (objectMap.Next(out var lhsIdToEvaluate, out var rhsIdToEvaluate))
                    {
                        // match objects, queueing as needed
                        var lhsTraitBasedObject = TraitBasedObjects[objectMap.GetLHSIndex(lhsIdToEvaluate)];
                        var rhsTraitBasedObject = rhsState.TraitBasedObjects[objectMap.GetRHSIndex(rhsIdToEvaluate)];

                        if (!ObjectsMatchAttributes(lhsTraitBasedObject, rhsTraitBasedObject, rhsState) ||
                            !CheckRelationsAndQueueObjects(lhsTraitBasedObject, rhsTraitBasedObject, rhsState, objectMap))
                        {
                            objectMap.RevertTraversalChanges();

                            matchFound = false;
                            break;
                        }
                    }

                    if (matchFound)
                        break;
                }

                if (!matchFound)
                {
                    statesEqual = false;
                    break;
                }
            }

            return statesEqual;
        }

        bool ObjectsMatchAttributes(TraitBasedObject traitBasedObjectLHS, TraitBasedObject traitBasedObjectRHS, StateData rhsState)
        {
            if (!traitBasedObjectLHS.HasSameTraits(traitBasedObjectRHS))
                return false;


            if (traitBasedObjectLHS.LocationIndex != TraitBasedObject.Unset
                && !LocationTraitAttributesEqual(LocationBuffer[traitBasedObjectLHS.LocationIndex], rhsState.LocationBuffer[traitBasedObjectRHS.LocationIndex]))
                return false;


            if (traitBasedObjectLHS.Pet_TimeIndex != TraitBasedObject.Unset
                && !Pet_TimeTraitAttributesEqual(Pet_TimeBuffer[traitBasedObjectLHS.Pet_TimeIndex], rhsState.Pet_TimeBuffer[traitBasedObjectRHS.Pet_TimeIndex]))
                return false;




            if (traitBasedObjectLHS.NeedIndex != TraitBasedObject.Unset
                && !NeedTraitAttributesEqual(NeedBuffer[traitBasedObjectLHS.NeedIndex], rhsState.NeedBuffer[traitBasedObjectRHS.NeedIndex]))
                return false;






            return true;
        }
        
        bool LocationTraitAttributesEqual(Location one, Location two)
        {
            return
                    one.Position == two.Position && 
                    one.Forward == two.Forward;
        }
        
        bool Pet_TimeTraitAttributesEqual(Pet_Time one, Pet_Time two)
        {
            return
                    one.Value == two.Value;
        }
        
        bool NeedTraitAttributesEqual(Need one, Need two)
        {
            return
                    one.NeedType == two.NeedType && 
                    one.HungerLevel == two.HungerLevel && 
                    one.HungerTick == two.HungerTick && 
                    one.ThirstLevel == two.ThirstLevel && 
                    one.ThirstTick == two.ThirstTick && 
                    one.HappinessLevel == two.HappinessLevel && 
                    one.HappinessTick == two.HappinessTick && 
                    one.EnergyLevel == two.EnergyLevel && 
                    one.EnergyTick == two.EnergyTick;
        }
        
        bool CheckRelationsAndQueueObjects(TraitBasedObject traitBasedObjectLHS, TraitBasedObject traitBasedObjectRHS, StateData rhsState, ObjectCorrespondence objectMap)
        {

            return true;
        }

        public override int GetHashCode()
        {
            // h = 3860031 + (h+y)*2779 + (h*y*2)   // from How to Hash a Set by Richard O’Keefe
            var stateHashValue = 3860031 + (397 + TraitBasedObjectIds.Length) * 2779 + (397 * TraitBasedObjectIds.Length * 2);

            int bufferLength;

            bufferLength = MoveableBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = LocationBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var element = LocationBuffer[i];
                var value = 397
                    ^ element.Position.GetHashCode()
                    ^ element.Forward.GetHashCode();
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = Pet_TimeBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var element = Pet_TimeBuffer[i];
                var value = 397
                    ^ element.Value.GetHashCode();
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = PetBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = Tree_FoodBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = NeedBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var element = NeedBuffer[i];
                var value = 397
                    ^ (int) element.NeedType
                    ^ element.HungerLevel.GetHashCode()
                    ^ element.HungerTick.GetHashCode()
                    ^ element.ThirstLevel.GetHashCode()
                    ^ element.ThirstTick.GetHashCode()
                    ^ element.HappinessLevel.GetHashCode()
                    ^ element.HappinessTick.GetHashCode()
                    ^ element.EnergyLevel.GetHashCode()
                    ^ element.EnergyTick.GetHashCode();
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = Lake_DrinkBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = Table_HappinessBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = Home_EnergyBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }
            bufferLength = PlanningAgentBuffer.Length;
            for (int i = 0; i < bufferLength; i++)
            {
                var value = 397;
                stateHashValue = 3860031 + (stateHashValue + value) * 2779 + (stateHashValue * value * 2);
            }

            return stateHashValue;
        }

        public override string ToString()
        {
            if (StateEntity.Equals(default))
                return string.Empty;

            var sb = new StringBuilder();
            var numObjects = TraitBasedObjects.Length;
            for (var traitBasedObjectIndex = 0; traitBasedObjectIndex < numObjects; traitBasedObjectIndex++)
            {
                var traitBasedObject = TraitBasedObjects[traitBasedObjectIndex];
                sb.AppendLine(TraitBasedObjectIds[traitBasedObjectIndex].ToString());

                var i = 0;

                var traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(MoveableBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(LocationBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(Pet_TimeBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(PetBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(Tree_FoodBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(NeedBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(Lake_DrinkBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(Table_HappinessBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(Home_EnergyBuffer[traitIndex].ToString());

                traitIndex = traitBasedObject[i++];
                if (traitIndex != TraitBasedObject.Unset)
                    sb.AppendLine(PlanningAgentBuffer[traitIndex].ToString());

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    public struct StateDataContext : ITraitBasedStateDataContext<TraitBasedObject, StateEntityKey, StateData>
    {
        public bool IsCreated;
        internal EntityCommandBuffer.ParallelWriter EntityCommandBuffer;
        internal EntityArchetype m_StateArchetype;
        internal int JobIndex;

        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<TraitBasedObject> TraitBasedObjects;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<TraitBasedObjectId> TraitBasedObjectIds;

        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Moveable> MoveableData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Location> LocationData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Pet_Time> Pet_TimeData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Pet> PetData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Tree_Food> Tree_FoodData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Need> NeedData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Lake_Drink> Lake_DrinkData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Table_Happiness> Table_HappinessData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<Home_Energy> Home_EnergyData;
        [ReadOnly,NativeDisableContainerSafetyRestriction] public BufferFromEntity<PlanningAgent> PlanningAgentData;

        [NativeDisableContainerSafetyRestriction,ReadOnly] ObjectCorrespondence m_ObjectCorrespondence;

        public StateDataContext(SystemBase system, EntityArchetype stateArchetype)
        {
            EntityCommandBuffer = default;
            TraitBasedObjects = system.GetBufferFromEntity<TraitBasedObject>(true);
            TraitBasedObjectIds = system.GetBufferFromEntity<TraitBasedObjectId>(true);

            MoveableData = system.GetBufferFromEntity<Moveable>(true);
            LocationData = system.GetBufferFromEntity<Location>(true);
            Pet_TimeData = system.GetBufferFromEntity<Pet_Time>(true);
            PetData = system.GetBufferFromEntity<Pet>(true);
            Tree_FoodData = system.GetBufferFromEntity<Tree_Food>(true);
            NeedData = system.GetBufferFromEntity<Need>(true);
            Lake_DrinkData = system.GetBufferFromEntity<Lake_Drink>(true);
            Table_HappinessData = system.GetBufferFromEntity<Table_Happiness>(true);
            Home_EnergyData = system.GetBufferFromEntity<Home_Energy>(true);
            PlanningAgentData = system.GetBufferFromEntity<PlanningAgent>(true);

            m_StateArchetype = stateArchetype;
            JobIndex = 0;
            m_ObjectCorrespondence = default;
            IsCreated = true;
        }

        public StateData GetStateData(StateEntityKey stateKey)
        {
            var stateEntity = stateKey.Entity;

            return new StateData
            {
                StateEntity = stateEntity,
                TraitBasedObjects = TraitBasedObjects[stateEntity],
                TraitBasedObjectIds = TraitBasedObjectIds[stateEntity],

                MoveableBuffer = MoveableData[stateEntity],
                LocationBuffer = LocationData[stateEntity],
                Pet_TimeBuffer = Pet_TimeData[stateEntity],
                PetBuffer = PetData[stateEntity],
                Tree_FoodBuffer = Tree_FoodData[stateEntity],
                NeedBuffer = NeedData[stateEntity],
                Lake_DrinkBuffer = Lake_DrinkData[stateEntity],
                Table_HappinessBuffer = Table_HappinessData[stateEntity],
                Home_EnergyBuffer = Home_EnergyData[stateEntity],
                PlanningAgentBuffer = PlanningAgentData[stateEntity],
            };
        }

        public StateData CopyStateData(StateData stateData)
        {
            return stateData.Copy(JobIndex, EntityCommandBuffer);
        }

        public StateEntityKey GetStateDataKey(StateData stateData)
        {
            return new StateEntityKey { Entity = stateData.StateEntity, HashCode = stateData.GetHashCode()};
        }

        public void DestroyState(StateEntityKey stateKey)
        {
            EntityCommandBuffer.DestroyEntity(JobIndex, stateKey.Entity);
        }

        public StateData CreateStateData()
        {
            return new StateData(JobIndex, EntityCommandBuffer, EntityCommandBuffer.CreateEntity(JobIndex, m_StateArchetype));
        }

        public bool Equals(StateData x, StateData y)
        {
            if (x.TraitBasedObjectIds.Length != y.TraitBasedObjectIds.Length)
                return false;

            if (!m_ObjectCorrespondence.IsCreated)
                m_ObjectCorrespondence = new ObjectCorrespondence(x.TraitBasedObjectIds.Length, Allocator.Temp);

            return x.TryGetObjectMapping(y, m_ObjectCorrespondence);
        }

        public int GetHashCode(StateData obj)
        {
            return obj.GetHashCode();
        }
    }

    [DisableAutoCreation, AlwaysUpdateSystem]
    public class StateManager : SystemBase, ITraitBasedStateManager<TraitBasedObject, StateEntityKey, StateData, StateDataContext>
    {
        public new EntityManager EntityManager
        {
            get
            {
                if (!m_EntityTransactionActive)
                    BeginEntityExclusivity();

                return ExclusiveEntityTransaction.EntityManager;
            }
        }

        ExclusiveEntityTransaction m_ExclusiveEntityTransaction;
        public ExclusiveEntityTransaction ExclusiveEntityTransaction
        {
            get
            {
                if (!m_EntityTransactionActive)
                    BeginEntityExclusivity();

                return m_ExclusiveEntityTransaction;
            }
        }

        StateDataContext m_StateDataContext;
        public StateDataContext StateDataContext
        {
            get
            {
                if (m_StateDataContext.IsCreated)
                    return m_StateDataContext;

                m_StateDataContext = new StateDataContext(this, m_StateArchetype);
                return m_StateDataContext;
            }
        }

        public event Action Destroying;

        List<EntityCommandBuffer> m_EntityCommandBuffers;
        EntityArchetype m_StateArchetype;
        bool m_EntityTransactionActive = false;
        int m_LastClearedFrame = 0;

        protected override void OnCreate()
        {
            m_StateArchetype = base.EntityManager.CreateArchetype(typeof(State), typeof(TraitBasedObject), typeof(TraitBasedObjectId), typeof(HashCode),
                typeof(Moveable),
                typeof(Location),
                typeof(Pet_Time),
                typeof(Pet),
                typeof(Tree_Food),
                typeof(Need),
                typeof(Lake_Drink),
                typeof(Table_Happiness),
                typeof(Home_Energy),
                typeof(PlanningAgent));

            m_EntityCommandBuffers = new List<EntityCommandBuffer>();
        }

        protected override void OnDestroy()
        {
            Destroying?.Invoke();
            EndEntityExclusivity();
            ClearECBs();
            base.OnDestroy();
        }

        public EntityCommandBuffer GetEntityCommandBuffer()
        {
            //todo determine a dedicated point to dispose of aggregated ECBs
            var jobDependencyHandle = ExclusiveEntityTransaction.EntityManager.ExclusiveEntityTransactionDependency;
            if (jobDependencyHandle.IsCompleted && UnityEngine.Time.frameCount != m_LastClearedFrame)
            {
                jobDependencyHandle.Complete();
                ClearECBs();
                m_LastClearedFrame = UnityEngine.Time.frameCount;
            }

            var ecb = new EntityCommandBuffer(Allocator.Persistent);
            m_EntityCommandBuffers.Add(ecb);
            return ecb;
        }

        public StateData CreateStateData()
        {
            var stateEntity = ExclusiveEntityTransaction.CreateEntity(m_StateArchetype);
            return new StateData(ExclusiveEntityTransaction, stateEntity);;
        }

        public StateData GetStateData(StateEntityKey stateKey, bool readWrite = false)
        {
            return !Enabled || !ExclusiveEntityTransaction.Exists(stateKey.Entity) ?
                default : new StateData(ExclusiveEntityTransaction, stateKey.Entity);
        }

        public void DestroyState(StateEntityKey stateKey)
        {
            var stateEntity = stateKey.Entity;
            if (ExclusiveEntityTransaction.Exists(stateEntity))
                ExclusiveEntityTransaction.DestroyEntity(stateEntity);
        }

        public StateEntityKey GetStateDataKey(StateData stateData)
        {
            return new StateEntityKey { Entity = stateData.StateEntity, HashCode = stateData.GetHashCode()};
        }

        public StateData CopyStateData(StateData stateData)
        {
            var copyStateEntity = ExclusiveEntityTransaction.Instantiate(stateData.StateEntity);
            return new StateData(ExclusiveEntityTransaction, copyStateEntity);
        }

        public StateEntityKey CopyState(StateEntityKey stateKey)
        {
            var copyStateEntity = ExclusiveEntityTransaction.Instantiate(stateKey.Entity);
            var stateData = new StateData(ExclusiveEntityTransaction, copyStateEntity);
            return new StateEntityKey { Entity = copyStateEntity, HashCode = stateData.GetHashCode()};
        }

        protected override void OnUpdate()
        {
            var jobDependencyHandle = ExclusiveEntityTransaction.EntityManager.ExclusiveEntityTransactionDependency;
            if (jobDependencyHandle.IsCompleted)
            {
                jobDependencyHandle.Complete();
                ClearECBs();
            }
        }

        void ClearECBs()
        {
            foreach (var ecb in m_EntityCommandBuffers)
            {
                ecb.Dispose();
            }
            m_EntityCommandBuffers.Clear();
        }

        public bool Equals(StateData x, StateData y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(StateData obj)
        {
            return obj.GetHashCode();
        }

        void BeginEntityExclusivity()
        {
            m_StateDataContext = new StateDataContext(this, m_StateArchetype);
            m_ExclusiveEntityTransaction = base.EntityManager.BeginExclusiveEntityTransaction();
            m_EntityTransactionActive = true;
        }

        void EndEntityExclusivity()
        {
            base.EntityManager.EndExclusiveEntityTransaction();
            m_EntityTransactionActive = false;
            m_StateDataContext = default;
        }
    }

    struct DestroyStatesJobScheduler : IDestroyStatesScheduler<StateEntityKey, StateData, StateDataContext, StateManager>
    {
        public StateManager StateManager { private get; set; }
        public NativeQueue<StateEntityKey> StatesToDestroy { private get; set; }

        public JobHandle Schedule(JobHandle inputDeps)
        {
            var stateDataContext = StateManager.StateDataContext;
            var ecb = StateManager.GetEntityCommandBuffer();
            stateDataContext.EntityCommandBuffer = ecb.AsParallelWriter();
            var destroyStatesJobHandle = new DestroyStatesJob<StateEntityKey, StateData, StateDataContext>()
            {
                StateDataContext = stateDataContext,
                StatesToDestroy = StatesToDestroy
            }.Schedule(inputDeps);

            var playbackECBJobHandle = new PlaybackSingleECBJob()
            {
                ExclusiveEntityTransaction = StateManager.ExclusiveEntityTransaction,
                EntityCommandBuffer = ecb
            }.Schedule(destroyStatesJobHandle);

            var entityManager = StateManager.ExclusiveEntityTransaction.EntityManager;
            entityManager.ExclusiveEntityTransactionDependency = playbackECBJobHandle;
            return playbackECBJobHandle;
        }
    }
}
