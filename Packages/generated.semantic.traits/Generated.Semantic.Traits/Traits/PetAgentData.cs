using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct Agent_PetData : ITraitData, IEquatable<Agent_PetData>
    {

        public bool Equals(Agent_PetData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Agent_Pet";
        }
    }
}
