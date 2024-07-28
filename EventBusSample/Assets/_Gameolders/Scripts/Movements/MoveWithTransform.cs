using UnityEngine;

namespace EventBusSample.Movements
{
    public class MoveWithTransform
    {
        readonly Transform _transform;
        readonly float _speed;

        Vector2 _movement;

        public MoveWithTransform(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void Tick(Vector2 direction)
        {
            if (direction.magnitude <= 0) return;
            
            Debug.Log("Tick");
            _movement = Time.deltaTime * _speed * direction;
        }

        public void FixedTick()
        {
            _transform.Translate(_movement);
        }
    }
}