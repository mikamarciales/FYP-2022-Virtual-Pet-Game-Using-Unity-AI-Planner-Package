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
        public System.Int32 HungerLevel;
        public System.Int32 HungerTick;
        public System.Int32 ThirstLevel;
        public System.Int32 ThirstTick;
        public System.Int32 HappinessLevel;
        public System.Int32 HappinessTick;
        public System.Int32 EnergyLevel;
        public System.Int32 EnergyTick;

        public bool Equals(NeedData other)
        {
            return NeedType.Equals(other.NeedType) && HungerLevel.Equals(other.HungerLevel) && HungerTick.Equals(other.HungerTick) && ThirstLevel.Equals(other.ThirstLevel) && ThirstTick.Equals(other.ThirstTick) && HappinessLevel.Equals(other.HappinessLevel) && HappinessTick.Equals(other.HappinessTick) && EnergyLevel.Equals(other.EnergyLevel) && EnergyTick.Equals(other.EnergyTick);
        }

        public override string ToString()
        {
            return $"Need: {NeedType} {HungerLevel} {HungerTick} {ThirstLevel} {ThirstTick} {HappinessLevel} {HappinessTick} {EnergyLevel} {EnergyTick}";
        }
    }
}
