using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct PetAgent : ITrait, IBufferElementData, IEquatable<PetAgent>
    {

        public void SetField(string fieldName, object value)
        {
        }

        public object GetField(string fieldName)
        {
            throw new ArgumentException("No fields exist on trait PetAgent.");
        }

        public bool Equals(PetAgent other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"PetAgent";
        }
    }
}
