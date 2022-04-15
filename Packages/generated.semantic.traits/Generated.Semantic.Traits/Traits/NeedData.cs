using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct NeedData : ITraitData, IEquatable<NeedData>
    {
        public Generated.Semantic.Traits.Enums.NeedType NeedType;
        public System.Int32 Level;
        public System.Int32 Tick;

        public bool Equals(NeedData other)
        {
            return NeedType.Equals(other.NeedType) && Level.Equals(other.Level) && Tick.Equals(other.Tick);
        }

        public override string ToString()
        {
            return $"Need: {NeedType} {Level} {Tick}";
        }
    }
}
