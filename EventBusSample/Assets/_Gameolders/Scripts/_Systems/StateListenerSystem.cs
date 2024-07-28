using EventBusSample.Components;
using EventBusSample.EventBuses;
using Unity.Entities;

namespace EventBusSample.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateBefore(typeof(EnemySpawnSystem))]
    public partial class StateListenerSystem : SystemBase
    {
        bool _isStateChanged;
        bool _isPlay;
        
        EventBinding<StopStateEvent> _stopEventBiding;
        EventBinding<PlayStateEvent> _playEventBiding;

        protected override void OnCreate()
        {
            _stopEventBiding = new EventBinding<StopStateEvent>();
            _playEventBiding = new EventBinding<PlayStateEvent>();
            RequireForUpdate<StateListenerData>();
        }

        protected override void OnStartRunning()
        {
            _stopEventBiding.Add(HandleOnStopWithoutArgsEvent);
            _playEventBiding.Add(HandleOnPlayWithoutArgsEvent);
            
            EventBus<StopStateEvent>.Register(_stopEventBiding);
            EventBus<PlayStateEvent>.Register(_playEventBiding);
        }

        protected override void OnStopRunning()
        {
            _stopEventBiding.Remove(HandleOnStopWithoutArgsEvent);
            _playEventBiding.Remove(HandleOnPlayWithoutArgsEvent);
            
            EventBus<StopStateEvent>.Deregister(_stopEventBiding);
            EventBus<PlayStateEvent>.Deregister(_playEventBiding);
        }

        protected override void OnUpdate()
        {
            if (!_isStateChanged) return;

            var stateListenerData = SystemAPI.GetSingleton<StateListenerData>();
            stateListenerData.IsStateChanged = true;
            stateListenerData.StateValue = _isPlay;
            SystemAPI.SetSingleton(stateListenerData);
            
            _isStateChanged = false;
        }
        
        void HandleOnStopWithoutArgsEvent()
        {
            _isStateChanged = true;
            _isPlay = false;
        }
        
        void HandleOnPlayWithoutArgsEvent()
        {
            _isStateChanged = true;
            _isPlay = true;
        }
    }
}