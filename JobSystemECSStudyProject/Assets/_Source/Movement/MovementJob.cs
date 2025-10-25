using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct MovementJob : IJobEntity
{
    public float DeltaTime;

    public readonly void Execute(ref LocalTransform transform, in MovementComponent movement, ref DirectionComponent direction)
    {
        float x = transform.Position.x;
        float z = transform.Position.z;
        float y = transform.Position.y;

        float targetY = direction.IsMovingUp ? movement.MaxY : movement.MinY;
        y = Mathf.MoveTowards(y, targetY, movement.Speed * DeltaTime);
        if (y == targetY)
        {
            direction.IsMovingUp = !direction.IsMovingUp;
        }

        transform = transform.WithPosition(new float3(x, y, z));
    }
}

