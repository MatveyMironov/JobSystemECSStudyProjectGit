using Unity.Entities;
using UnityEngine;

public class SpawnAuthorizationMB : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int count;

    public GameObject Prefab { get => prefab; }
    public int Count { get => count; }
}

public struct SpawnerData : IComponentData
{
    public Entity Prefab;
    public int Count;
}

public class SpawnBaker : Baker<SpawnAuthorizationMB>
{
    public override void Bake(SpawnAuthorizationMB authoring)
    {
        Entity spawner = GetEntity(TransformUsageFlags.None);
        Entity prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic | TransformUsageFlags.Renderable);

        SpawnerData spawnerData = new() { Prefab = prefab, Count = authoring.Count };
        AddComponent(spawner, spawnerData);
    }
}
