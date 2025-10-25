using Unity.Burst;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial struct MovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new MovementJob() { DeltaTime = Time.deltaTime };
        job.ScheduleParallel();
    }
}