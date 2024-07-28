using Unity.Entities;

namespace EventBusSample.Components
{
    public struct StateListenerData : IComponentData
    {
        public bool IsStateChanged;
        public bool StateValue;
    }
}