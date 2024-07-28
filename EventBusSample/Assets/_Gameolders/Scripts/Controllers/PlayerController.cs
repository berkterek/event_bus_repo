using EventBusSample.EventBuses;
using EventBusSample.Inputs;
using EventBusSample.Movements;
using UnityEngine;

namespace EventBusSample.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;
        [SerializeField] Transform _transform;

        NewInputReader _inputReader;
        MoveWithTransform _mover;
        EventBinding<StopStateEvent> _stopEventBiding;
        EventBinding<PlayStateEvent> _playEventBiding;
        bool _isPlay;

        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        void Awake()
        {
            _inputReader = new NewInputReader();
            _mover = new MoveWithTransform(_transform, _speed);
            
            _stopEventBiding = new EventBinding<StopStateEvent>();
            _playEventBiding = new EventBinding<PlayStateEvent>();
        }

        void Start()
        {
            _isPlay = true;
        }

        void OnEnable()
        {
            _stopEventBiding.Add(HandleOnStopWithoutArgsEvent);
            _playEventBiding.Add(HandleOnPlayWithoutArgsEvent);
            
            EventBus<StopStateEvent>.Register(_stopEventBiding);
            EventBus<PlayStateEvent>.Register(_playEventBiding);
        }

        void OnDisable()
        {
            _stopEventBiding.Remove(HandleOnStopWithoutArgsEvent);
            _playEventBiding.Remove(HandleOnPlayWithoutArgsEvent);
            
            EventBus<StopStateEvent>.Deregister(_stopEventBiding);
            EventBus<PlayStateEvent>.Deregister(_playEventBiding);
        }

        void Update()
        {
            if (!_isPlay) return;
            
            _mover.Tick(_inputReader.Direction);
        }

        void FixedUpdate()
        {
            _mover.FixedTick();
        }
        
        void HandleOnStopWithoutArgsEvent()
        {
            _isPlay = false;
        }
        
        void HandleOnPlayWithoutArgsEvent()
        {
            _isPlay = true;
        }
    }
}