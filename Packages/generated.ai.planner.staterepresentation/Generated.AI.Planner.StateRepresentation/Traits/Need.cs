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
        public const string FieldUrgency = "Urgency";
        public const string FieldChangePerSecond = "ChangePerSecond";
        public Generated.Semantic.Traits.Enums.NeedType NeedType;
        public System.Int32 Urgency;
        public System.Int32 ChangePerSecond;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(NeedType):
                    NeedType = (Generated.Semantic.Traits.Enums.NeedType)Enum.ToObject(typeof(Generated.Semantic.Traits.Enums.NeedType), value);
                    break;
                case nameof(Urgency):
                    Urgency = (System.Int32)value;
                    break;
                case nameof(ChangePerSecond):
                    ChangePerSecond = (System.Int32)value;
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
                case nameof(Urgency):
                    return Urgency;
                case nameof(ChangePerSecond):
                    return ChangePerSecond;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Need.");
            }
        }

        public bool Equals(Need other)
        {
            return NeedType == other.NeedType && Urgency == other.Urgency && ChangePerSecond == other.ChangePerSecond;
        }

        public override string ToString()
        {
            return $"Need\n  NeedType: {NeedType}\n  Urgency: {Urgency}\n  ChangePerSecond: {ChangePerSecond}";
        }
    }
}
