using EventBusSample.Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace EventBusSample.Authorings
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        
        class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                
                AddComponent<ActiveTag>(entity);
                AddComponent<DeactivateTag>(entity);
                AddComponent<EnemySpawnerTag>(entity);
                
                AddComponent<SpawnerData>(entity, new()
                {
                    Enemy = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    CurrentTime = 0f,
                    MaxTime = 1f,
                    MinYPosition = -10f,
                    MaxYPosition = 10f,
                });

                uint seed = (uint)new System.Random().Next(0, int.MaxValue);
                AddComponent<RandomData>(entity, new()
                {
                    RandomValue = Random.CreateFromIndex(seed),
                });
            }
        }
    }
}