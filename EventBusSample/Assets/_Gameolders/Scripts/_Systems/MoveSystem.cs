using EventBusSample.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EventBusSample.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            new MoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }

        [BurstCompile]
        private partial struct MoveJob : IJobEntity
        {
            public float DeltaTime;

            [BurstCompile]
            private void Execute(Entity entity, in ActiveTag activeTag, in MoveData moveData,
                ref LocalTransform localTransform)
            {
                var direction = new float3(-1f, 0f, 0f);
                localTransform.Position += DeltaTime * moveData.MoveSpeed * direction;
            }
        }
    }
}