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
    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.HasSingleton<SpawnerData>())
            return;

        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerData>();
        if (state.EntityManager.HasComponent<SpawnedTag>(spawnerEntity))
            return;
        
        var spawnerData = SystemAPI.GetSingleton<SpawnerData>();
        if (spawnerData.Prefab == null)
            return;

        var entityManager = state.EntityManager;
        NativeArray<Entity> clones = entityManager.Instantiate(spawnerData.Prefab, spawnerData.Count, Allocator.Temp);
        int width = (int)math.sqrt(clones.Length);
        int column = 0;
        int row = 0;
        float interval = 1.1f;
        for (int i = 0; i < clones.Length; i++)
        {
            float x = column * interval;
            float y = UnityEngine.Random.Range(-1.0f, 1.0f);
            float z = row * interval;
            float3 position = new(x, y, z);

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
