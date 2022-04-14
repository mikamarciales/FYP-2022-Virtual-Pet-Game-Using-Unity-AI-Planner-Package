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
        public System.Int32 Urgency
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p1 = m_EntityManager.GetComponentData<NeedData>(m_Entity).Urgency;
                }

                return m_p1;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.Urgency = m_p1 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Int32 ChangePerSecond
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity))
                {
                    m_p2 = m_EntityManager.GetComponentData<NeedData>(m_Entity).ChangePerSecond;
                }

                return m_p2;
            }
            set
            {
                NeedData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<NeedData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<NeedData>(m_Entity);
                data.ChangePerSecond = m_p2 = value;
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
        [InspectorName("Urgency")]
        System.Int32 m_p1 = 0;
        [SerializeField]
        [InspectorName("ChangePerSecond")]
        System.Int32 m_p2 = 1;
        #pragma warning restore 649

        EntityManager m_EntityManager;
        World m_World;
        Entity m_Entity;

        NeedData GetData()
        {
            NeedData data = default;
            data.NeedType = m_p0;
            data.Urgency = m_p1;
            data.ChangePerSecond = m_p2;

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
