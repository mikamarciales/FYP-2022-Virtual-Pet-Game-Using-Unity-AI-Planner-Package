using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct PetAgentData : ITraitData, IEquatable<PetAgentData>
    {

        public bool Equals(PetAgentData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"PetAgent";
        }
    }
}
