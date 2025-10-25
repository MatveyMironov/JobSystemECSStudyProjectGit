using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct SpawnSystem : ISystem
{
    public struct SpawnedTag : IComponentData { }

    [BurstCompile]
    public readonly void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.HasSingleton<SpawnerComponent>())
            return;

        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerComponent>();
        if (state.EntityManager.HasComponent<SpawnedTag>(spawnerEntity))
            return;
        
        var spawnerData = SystemAPI.GetSingleton<SpawnerComponent>();
        if (spawnerData.Prefab == null)
            return;

        var entityManager = state.EntityManager;
        NativeArray<Entity> clones = entityManager.Instantiate(spawnerData.Prefab, spawnerData.Count, Allocator.Temp);
        int width = (int)math.sqrt(clones.Length);
        int column = 0;
        int row = 0;
        for (int i = 0; i < clones.Length; i++)
        {
            float positionX = column * spawnerData.Interval;
            float positionY = UnityEngine.Random.Range(spawnerData.MinY, spawnerData.MaxY);
            float positionZ = row * spawnerData.Interval;
            float3 position = new(positionX, positionY, positionZ);

            column++;
            if (column >= width)
            {
                column = 0;
                row++;
            }    

            entityManager.SetComponentData(clones[i], LocalTransform.FromPosition(position));
        }

        entityManager.AddComponent<SpawnedTag>(spawnerEntity);
        clones.Dispose();
    }
}
