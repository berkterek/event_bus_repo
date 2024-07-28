
namespace EventBusSample.EventBuses
{
    public interface IEventBinding<T> where T : IEvent
    {
        void Add(System.Action<T> onEventWithArgs);
        void Add(System.Action onEventWithoutArgs);
        void Remove(System.Action<T> onEventWithArgs);
        void Remove(System.Action onEventWithoutArgs);
        void InvokeAll(T value);
    }
    
    public class EventBinding<T> : IEventBinding<T> where T: IEvent
    {
        event System.Action OnEventWithoutArgs;
        event System.Action<T> OnEventWithArgs;

        public void Add(System.Action<T> onEventWithArgs) => OnEventWithArgs += onEventWithArgs;

        public void Add(System.Action onEventWithoutArgs) => OnEventWithoutArgs += onEventWithoutArgs;

        public void Remove(System.Action<T> onEventWithArgs) => OnEventWithArgs -= onEventWithArgs;

        public void Remove(System.Action onEventWithoutArgs) => OnEventWithoutArgs -= onEventWithoutArgs;

        public void InvokeAll(T value)
        {
            OnEventWithoutArgs?.Invoke();
            OnEventWithArgs?.Invoke(value);
        }
    }
}