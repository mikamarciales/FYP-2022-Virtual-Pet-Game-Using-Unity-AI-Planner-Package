using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Entities;
using UnityEngine;

namespace Generated.Semantic.Traits
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [AddComponentMenu("Semantic/Traits/Need (Trait)")]
    [RequireComponent(typeof(SemanticObject))]
    public partial class Need : MonoBehaviour, ITrait
    {
        public Generated.Semantic.Traits.Enums.NeedType NeedType
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p0 = m_EntityManager.GetComponentData<NeedData>(m_Entity).NeedType;
                }

                return m_p0;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.NeedType = m_p0 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 HungerLevel
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p1 = m_EntityManager.GetComponentData<NeedData>(m_Entity).HungerLevel;
                }

                return m_p1;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.HungerLevel = m_p1 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 HungerTick
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p2 = m_EntityManager.GetComponentData<NeedData>(m_Entity).HungerTick;
                }

                return m_p2;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.HungerTick = m_p2 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 ThirstLevel
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p4 = m_EntityManager.GetComponentData<NeedData>(m_Entity).ThirstLevel;
                }

                return m_p4;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.ThirstLevel = m_p4 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 ThirstTick
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p5 = m_EntityManager.GetComponentData<NeedData>(m_Entity).ThirstTick;
                }

                return m_p5;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.ThirstTick = m_p5 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 HappinessLevel
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p6 = m_EntityManager.GetComponentData<NeedData>(m_Entity).HappinessLevel;
                }

                return m_p6;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.HappinessLevel = m_p6 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 HappinessTick
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p7 = m_EntityManager.GetComponentData<NeedData>(m_Entity).HappinessTick;
                }

                return m_p7;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.HappinessTick = m_p7 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 EnergyLevel
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p8 = m_EntityManager.GetComponentData<NeedData>(m_Entity).EnergyLevel;
                }

                return m_p8;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.EnergyLevel = m_p8 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 EnergyTick
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p9 = m_EntityManager.GetComponentData<NeedData>(m_Entity).EnergyTick;
                }

                return m_p9;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.EnergyTick = m_p9 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public NeedData Data
        {
            get => m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity) ?
                m_EntityManager.GetComponentData<NeedData>(m_Entity) : GetData();
            set
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                    m_EntityManager.SetComponentData(m_Entity, value);
            }
        }

        #pragma warning disable 649
        [SerializeField]
        [InspectorName("NeedType")]
        Generated.Semantic.Traits.Enums.NeedType m_p0 = (Generated.Semantic.Traits.Enums.NeedType)0;
        [SerializeField]
        [InspectorName("HungerLevel")]
        System.Int32 m_p1 = 100;
        [SerializeField]
        [InspectorName("HungerTick")]
        System.Int32 m_p2 = 10;
        [SerializeField]
        [InspectorName("ThirstLevel")]
        System.Int32 m_p4 = 100;
        [SerializeField]
        [InspectorName("ThirstTick")]
        System.Int32 m_p5 = 10;
        [SerializeField]
        [InspectorName("HappinessLevel")]
        System.Int32 m_p6 = 100;
        [SerializeField]
        [InspectorName("HappinessTick")]
        System.Int32 m_p7 = 5;
        [SerializeField]
        [InspectorName("EnergyLevel")]
        System.Int32 m_p8 = 100;
        [SerializeField]
        [InspectorName("EnergyTick")]
        System.Int32 m_p9 = 5;
        #pragma warning restore 649

        EntityManager m_EntityManager;
        World m_World;
        Entity m_Entity;

        NeedData GetData()
        {
            NeedData data = default;
            data.NeedType = m_p0;
            data.HungerLevel = m_p1;
            data.HungerTick = m_p2;
            data.ThirstLevel = m_p4;
            data.ThirstTick = m_p5;
            data.HappinessLevel = m_p6;
            data.HappinessTick = m_p7;
            data.EnergyLevel = m_p8;
            data.EnergyTick = m_p9;

            return data;
        }

        
        void OnEnable()
        {
            // Handle the case where this trait is added after conversion
            var semanticObject = GetComponent<SemanticObject>();
            if (semanticObject && !semanticObject.Entity.Equals(default))
                Convert(semanticObject.Entity, semanticObject.EntityManager, null);
        }

        public void Convert(Entity entity, EntityManager destinationManager, GameObjectConversionSystem _)
        {
            m_Entity = entity;
            m_EntityManager = destinationManager;
            m_World = destinationManager.World;

            if (!destinationManager.HasComponent(entity, typeof(NeedData)))
            {
                destinationManager.AddComponentData(entity, GetData());
            }
        }

        void OnDestroy()
        {
            if (m_World != default && m_World.IsCreated)
            {
                m_EntityManager.RemoveComponent<NeedData>(m_Entity);
                if (m_EntityManager.GetComponentCount(m_Entity) == 0)
                    m_EntityManager.DestroyEntity(m_Entity);
            }
        }

        void OnValidate()
        {

            // Commit local fields to backing store
            Data = GetData();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            TraitGizmos.DrawGizmoForTrait(nameof(NeedData), gameObject,Data);
        }
#endif
    }
}
