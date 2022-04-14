using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct PetData : ITraitData, IEquatable<PetData>
    {

        public bool Equals(PetData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Pet";
        }
    }
}
