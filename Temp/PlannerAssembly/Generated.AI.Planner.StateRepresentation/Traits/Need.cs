using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Need : ITrait, IBufferElementData, IEquatable<Need>
    {
        public const string FieldNeedType = "NeedType";
        public const string FieldHungerLevel = "HungerLevel";
        public const string FieldHungerTick = "HungerTick";
        public const string FieldThirstLevel = "ThirstLevel";
        public const string FieldThirstTick = "ThirstTick";
        public const string FieldHappinessLevel = "HappinessLevel";
        public const string FieldHappinessTick = "HappinessTick";
        public const string FieldEnergyLevel = "EnergyLevel";
        public const string FieldEnergyTick = "EnergyTick";
        public Generated.Semantic.Traits.Enums.NeedType NeedType;
        public System.Int32 HungerLevel;
        public System.Int32 HungerTick;
        public System.Int32 ThirstLevel;
        public System.Int32 ThirstTick;
        public System.Int32 HappinessLevel;
        public System.Int32 HappinessTick;
        public System.Int32 EnergyLevel;
        public System.Int32 EnergyTick;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(NeedType):
                    NeedType = (Generated.Semantic.Traits.Enums.NeedType)Enum.ToObject(typeof(Generated.Semantic.Traits.Enums.NeedType), value);
                    break;
                case nameof(HungerLevel):
                    HungerLevel = (System.Int32)value;
                    break;
                case nameof(HungerTick):
                    HungerTick = (System.Int32)value;
                    break;
                case nameof(ThirstLevel):
                    ThirstLevel = (System.Int32)value;
                    break;
                case nameof(ThirstTick):
                    ThirstTick = (System.Int32)value;
                    break;
                case nameof(HappinessLevel):
                    HappinessLevel = (System.Int32)value;
                    break;
                case nameof(HappinessTick):
                    HappinessTick = (System.Int32)value;
                    break;
                case nameof(EnergyLevel):
                    EnergyLevel = (System.Int32)value;
                    break;
                case nameof(EnergyTick):
                    EnergyTick = (System.Int32)value;
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Need.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(NeedType):
                    return NeedType;
                case nameof(HungerLevel):
                    return HungerLevel;
                case nameof(HungerTick):
                    return HungerTick;
                case nameof(ThirstLevel):
                    return ThirstLevel;
                case nameof(ThirstTick):
                    return ThirstTick;
                case nameof(HappinessLevel):
                    return HappinessLevel;
                case nameof(HappinessTick):
                    return HappinessTick;
                case nameof(EnergyLevel):
                    return EnergyLevel;
                case nameof(EnergyTick):
                    return EnergyTick;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Need.");
            }
        }

        public bool Equals(Need other)
        {
            return NeedType == other.NeedType && HungerLevel == other.HungerLevel && HungerTick == other.HungerTick && ThirstLevel == other.ThirstLevel && ThirstTick == other.ThirstTick && HappinessLevel == other.HappinessLevel && HappinessTick == other.HappinessTick && EnergyLevel == other.EnergyLevel && EnergyTick == other.EnergyTick;
        }

        public override string ToString()
        {
            return $"Need\n  NeedType: {NeedType}\n  HungerLevel: {HungerLevel}\n  HungerTick: {HungerTick}\n  ThirstLevel: {ThirstLevel}\n  ThirstTick: {ThirstTick}\n  HappinessLevel: {HappinessLevel}\n  HappinessTick: {HappinessTick}\n  EnergyLevel: {EnergyLevel}\n  EnergyTick: {EnergyTick}";
        }
    }
}
