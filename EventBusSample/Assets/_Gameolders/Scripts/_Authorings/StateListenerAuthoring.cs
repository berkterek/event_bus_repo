using EventBusSample.Components;
using Unity.Entities;
using UnityEngine;

namespace EventBusSample.Authorings
{
    public class StateListenerAuthoring : MonoBehaviour
    {
        private class StateListenerAuthoringBaker : Baker<StateListenerAuthoring>
        {
            public override void Bake(StateListenerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent<StateListenerData>(entity);
            }
        }
    }
}