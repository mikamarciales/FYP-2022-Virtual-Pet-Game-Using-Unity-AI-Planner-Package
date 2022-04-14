using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct Home_EnergyData : ITraitData, IEquatable<Home_EnergyData>
    {

        public bool Equals(Home_EnergyData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Home_Energy";
        }
    }
}
