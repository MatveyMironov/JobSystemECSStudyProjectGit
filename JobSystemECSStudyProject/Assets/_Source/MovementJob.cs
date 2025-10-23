using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct MovementJob : IJobEntity
{
    public float Time;

    public void Execute(ref LocalTransform transform, in MovementSpeed movementSpeed, in Radius radius)
    {
        float x = transform.Position.x;
        float z = transform.Position.z;
        float y = transform.Position.y;
        
        transform = transform.WithPosition(new float3(x, y, z));
    }
}

