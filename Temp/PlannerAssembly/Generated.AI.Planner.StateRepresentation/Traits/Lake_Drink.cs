using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Lake_Drink : ITrait, IBufferElementData, IEquatable<Lake_Drink>
    {

        public void SetField(string fieldName, object value)
        {
        }

        public object GetField(string fieldName)
        {
            throw new ArgumentException("No fields exist on trait Lake_Drink.");
        }

        public bool Equals(Lake_Drink other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Lake_Drink";
        }
    }
}
