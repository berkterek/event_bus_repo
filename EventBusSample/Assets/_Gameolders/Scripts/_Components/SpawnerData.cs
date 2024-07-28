using Unity.Entities;

namespace EventBusSample.Components
{
    public struct SpawnerData : IComponentData
    {
        public Entity Enemy;
        public float MinYPosition;
        public float MaxYPosition;
        public float CurrentTime;
        public float MaxTime;
    }
}