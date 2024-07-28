using EventBusSample.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace EventBusSample.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct FirstTimeActiveDeactivateCheckerSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            var job = new FirstTimeActiveDeactivateCheckerJob()
            {
                Ecb = entityCommandBuffer
            };

            var jobHandle = job.Schedule(state.Dependency);
            state.Dependency = jobHandle;
            jobHandle.Complete();
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }

        [BurstCompile]
        public partial struct FirstTimeActiveDeactivateCheckerJob : IJobEntity
        {
            public EntityCommandBuffer Ecb;
            
            [BurstCompile]
            private void Execute(Entity entity, in ActiveTag activeTag, in DeactivateTag deactivateTag)
            {
                Ecb.SetComponentEnabled<DeactivateTag>(entity, false);
            }
        }
    }
}