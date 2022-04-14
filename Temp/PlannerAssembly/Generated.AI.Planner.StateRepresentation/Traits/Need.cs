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
        public const string FieldLevel = "Level";
        public const string FieldTick = "Tick";
        public Generated.Semantic.Traits.Enums.NeedType NeedType;
        public System.Int32 Level;
        public System.Int32 Tick;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(NeedType):
                    NeedType = (Generated.Semantic.Traits.Enums.NeedType)Enum.ToObject(typeof(Generated.Semantic.Traits.Enums.NeedType), value);
                    break;
                case nameof(Level):
                    Level = (System.Int32)value;
                    break;
                case nameof(Tick):
                    Tick = (System.Int32)value;
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
                case nameof(Level):
                    return Level;
                case nameof(Tick):
                    return Tick;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Need.");
            }
        }

        public bool Equals(Need other)
        {
            return NeedType == other.NeedType && Level == other.Level && Tick == other.Tick;
        }

        public override string ToString()
        {
            return $"Need\n  NeedType: {NeedType}\n  Level: {Level}\n  Tick: {Tick}";
        }
    }
}
