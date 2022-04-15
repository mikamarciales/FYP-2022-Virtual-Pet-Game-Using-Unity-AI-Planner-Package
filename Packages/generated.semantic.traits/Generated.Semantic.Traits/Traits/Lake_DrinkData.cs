using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct Lake_DrinkData : ITraitData, IEquatable<Lake_DrinkData>
    {

        public bool Equals(Lake_DrinkData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Lake_Drink";
        }
    }
}
