using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct MovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float time = (float)SystemAPI.Time.ElapsedTime;
        var job = new MovementJob() { Time = time };
        job.ScheduleParallel();
    }
}