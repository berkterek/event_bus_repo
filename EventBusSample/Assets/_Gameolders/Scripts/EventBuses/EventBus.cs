using System.Collections.Generic;

namespace EventBusSample.EventBuses
{
    public class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> _bindings;

        static EventBus()
        {
            _bindings = new HashSet<IEventBinding<T>>();
        }
        
        public static void Register(IEventBinding<T> binding) => _bindings.Add(binding);

        public static void Deregister(IEventBinding<T> binding) => _bindings.Remove(binding);
        
        public static void Raise(T value)
        {
            foreach (var binding in _bindings)
            {
                binding.InvokeAll(value);
            }
        }
    }
}