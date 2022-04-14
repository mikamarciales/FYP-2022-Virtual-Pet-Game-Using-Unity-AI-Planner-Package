using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct Table_HappinessData : ITraitData, IEquatable<Table_HappinessData>
    {

        public bool Equals(Table_HappinessData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Table_Happiness";
        }
    }
}
