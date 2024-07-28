using EventBusSample.Components;
using Unity.Burst;
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
            
            SystemAPI.SetSingleton(stateListenerData);
        }
    }
}