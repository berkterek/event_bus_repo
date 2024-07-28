using Unity.Entities;
using Unity.Mathematics;

namespace EventBusSample.Components
{
    public struct RandomData : IComponentData
    {
        public Random RandomValue;
    }
}