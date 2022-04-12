using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct Pet_TimeData : ITraitData, IEquatable<Pet_TimeData>
    {
        public System.Int32 Value;

        public bool Equals(Pet_TimeData other)
        {
            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return $"Pet_Time: {Value}";
        }
    }
}
