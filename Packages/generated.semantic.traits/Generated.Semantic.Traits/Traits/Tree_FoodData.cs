using System;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Collections;
using Unity.Entities;

namespace Generated.Semantic.Traits
{
    [Serializable]
    public partial struct Tree_FoodData : ITraitData, IEquatable<Tree_FoodData>
    {

        public bool Equals(Tree_FoodData other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Tree_Food";
        }
    }
}
