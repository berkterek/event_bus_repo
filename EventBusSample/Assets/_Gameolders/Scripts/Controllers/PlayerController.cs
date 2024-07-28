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

        void OnValidate()
        {
            if (_transform == null) _transform = GetComponent<Transform>();
        }

        void Awake()
        {
            _inputReader = new NewInputReader();
            _mover = new MoveWithTransform(_transform, _speed);
        }

        void Update()
        {
            _mover.Tick(_inputReader.Direction);
        }

        void FixedUpdate()
        {
            _mover.FixedTick();
        }
    }
}