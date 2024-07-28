using EventBusSample.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EventBusSample.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemySpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            new EnemySpawnJob()
            {
                Ecb = entityCommandBuffer,
                DeltaTime = deltaTime
            }.Run();
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }

        [BurstCompile]
        [WithDisabled(typeof(DeactivateTag))]
        private partial struct EnemySpawnJob : IJobEntity
        {
            public float DeltaTime;
            public EntityCommandBuffer Ecb;
            
            [BurstCompile]
            private void Execute(Entity entity, in ActiveTag activeTag, ref SpawnerData spawnerData, in EnemySpawnerTag enemySpawnerTag, ref RandomData randomData)
            {
                spawnerData.CurrentTime += DeltaTime;

                if (spawnerData.CurrentTime < spawnerData.MaxTime) return;
                spawnerData.CurrentTime = 0f;

                var randomYPosition =
                    randomData.RandomValue.NextFloat(spawnerData.MinYPosition, spawnerData.MaxYPosition);
                var randomPosition = new float3(20f, randomYPosition, 0f);
                
                var enemy = Ecb.Instantiate(spawnerData.Enemy);
                Ecb.SetComponent(enemy, new LocalTransform()
                {
                    Position = randomPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
        }
    }
}