using EventBusSample.Components;
using Unity.Entities;
using UnityEngine;

namespace EventBusSample.Authorings
{
    public class EnemyAuthoring : MonoBehaviour
    {
        class EnemyBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<ActiveTag>(entity);
                AddComponent<EnemyTag>(entity);
                AddComponent<MoveData>(entity, new()
                {
                    MoveSpeed = 5f
                });
            }
        }
    }
}

