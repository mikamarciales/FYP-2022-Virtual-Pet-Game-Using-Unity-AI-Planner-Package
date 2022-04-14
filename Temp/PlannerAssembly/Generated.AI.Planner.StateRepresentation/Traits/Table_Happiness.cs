using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Table_Happiness : ITrait, IBufferElementData, IEquatable<Table_Happiness>
    {

        public void SetField(string fieldName, object value)
        {
        }

        public object GetField(string fieldName)
        {
            throw new ArgumentException("No fields exist on trait Table_Happiness.");
        }

        public bool Equals(Table_Happiness other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Table_Happiness";
        }
    }
}
