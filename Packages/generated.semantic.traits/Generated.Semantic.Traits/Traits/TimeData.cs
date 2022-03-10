using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct TimeData : ITraitData, IEquatable<TimeData>
    {
        public System.Int32 Value;

        public bool Equals(TimeData other)
        {
            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return $"Time: {Value}";
        }
    }
}
