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
        public System.Int32 Urgency;
        public System.Int32 ChangePerSecond;

        public bool Equals(NeedData other)
        {
            return NeedType.Equals(other.NeedType) && Urgency.Equals(other.Urgency) && ChangePerSecond.Equals(other.ChangePerSecond);
        }

        public override string ToString()
        {
            return $"Need: {NeedType} {Urgency} {ChangePerSecond}";
        }
    }
}
