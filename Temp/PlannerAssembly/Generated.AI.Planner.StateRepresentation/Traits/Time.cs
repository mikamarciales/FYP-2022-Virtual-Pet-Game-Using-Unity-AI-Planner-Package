using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Time : ITrait, IBufferElementData, IEquatable<Time>
    {
        public const string FieldValue = "Value";
        public System.Int32 Value;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(Value):
                    Value = (System.Int32)value;
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Time.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(Value):
                    return Value;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Time.");
            }
        }

        public bool Equals(Time other)
        {
            return Value == other.Value;
        }

        public override string ToString()
        {
            return $"Time\n  Value: {Value}";
        }
    }
}
