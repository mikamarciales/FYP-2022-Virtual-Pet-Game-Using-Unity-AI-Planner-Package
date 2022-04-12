using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.AI.Planner.Traits;
using Generated.Semantic.Traits.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Tree_Food : ITrait, IBufferElementData, IEquatable<Tree_Food>
    {

        public void SetField(string fieldName, object value)
        {
        }

        public object GetField(string fieldName)
        {
            throw new ArgumentException("No fields exist on trait Tree_Food.");
        }

        public bool Equals(Tree_Food other)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Tree_Food";
        }
    }
}
