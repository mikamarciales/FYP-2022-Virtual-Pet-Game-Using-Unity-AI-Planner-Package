using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Home_Energy : ITrait, IBufferElementData, IEquatable<Home_Energy>
    {

        public void SetField(string fieldName, object value)
        {
        }

        public object GetField(string fieldName)
        {
            throw new ArgumentException("No fields exist on trait Home_Energy.");
        }

        public bool Equals(Home_Energy other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Home_Energy";
        }
    }
}
