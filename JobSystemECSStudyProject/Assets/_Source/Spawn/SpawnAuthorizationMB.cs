using Unity.Entities;
using UnityEngine;

public class SpawnAuthorizationMB : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int count;
    [SerializeField] private int interval;
    [SerializeField] private int maxY;
    [SerializeField] private int minY;

    public GameObject Prefab { get => prefab; }
    public int Count { get => count; }
    public int Interval { get => interval; }
    public int MaxY { get => maxY; }
    public int MinY { get => minY; }
}

public struct SpawnerComponent : IComponentData
{
    public Entity Prefab;
    public int Count;
    public int Interval;
    public int MinY;
    public int MaxY;
}

public class SpawnBaker : Baker<SpawnAuthorizationMB>
{
    public override void Bake(SpawnAuthorizationMB authoring)
    {
        Entity spawner = GetEntity(TransformUsageFlags.None);
        Entity prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic | TransformUsageFlags.Renderable);

        SpawnerComponent spawnerComponent = new()
        {
            Prefab = prefab,
            Count = authoring.Count,
            Interval = authoring.Interval,
            MaxY = authoring.MaxY,
            MinY = authoring.MinY
        };
        AddComponent(spawner, spawnerComponent);
    }
}
