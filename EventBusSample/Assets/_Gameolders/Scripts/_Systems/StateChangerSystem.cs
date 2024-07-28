using EventBusSample.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace EventBusSample.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateBefore(typeof(EnemySpawnSystem))]
    [UpdateAfter(typeof(StateListenerSystem))]
    public partial struct StateChangerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<StateListenerData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var stateListenerData = SystemAPI.GetSingleton<StateListenerData>();
            if (!stateListenerData.IsStateChanged) return;

            stateListenerData.IsStateChanged = false;

            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            if (stateListenerData.StateValue)
            {
                var job = new PlayStateJob()
                {
                    Ecb = entityCommandBuffer.AsParallelWriter()
                };

                var jobHandle = job.ScheduleParallel(state.Dependency);
                state.Dependency = jobHandle;
                jobHandle.Complete();
            }
            else
            {
                var job = new StopStateJob()
                {
                    Ecb = entityCommandBuffer.AsParallelWriter()
                };
                
                var jobHandle = job.ScheduleParallel(state.Dependency);
                state.Dependency = jobHandle;
                jobHandle.Complete();
            }
            
            SystemAPI.SetSingleton(stateListenerData);
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }

        [BurstCompile]
        [WithDisabled(typeof(ActiveTag))]
        private partial struct PlayStateJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter Ecb;
            
            [BurstCompile]
            private void Execute(Entity entity, in DeactivateTag deactivateTag, [ChunkIndexInQuery]int sortKey)
            {
                Ecb.SetComponentEnabled<DeactivateTag>(sortKey, entity, false);
                Ecb.SetComponentEnabled<ActiveTag>(sortKey, entity, true);
            }
        }
        
        [BurstCompile]
        [WithDisabled(typeof(DeactivateTag))]
        private partial struct StopStateJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter Ecb;
            
            [BurstCompile]
            private void Execute(Entity entity, in ActiveTag activateTag, [ChunkIndexInQuery]int sortKey)
            {
                Ecb.SetComponentEnabled<DeactivateTag>(sortKey, entity, true);
                Ecb.SetComponentEnabled<ActiveTag>(sortKey, entity, false);
            }
        }
    }
}