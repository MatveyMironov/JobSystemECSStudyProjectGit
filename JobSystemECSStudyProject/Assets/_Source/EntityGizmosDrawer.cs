using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EntityGizmosDrawer : MonoBehaviour
{
    [SerializeField] private int maxDraw;
    [SerializeField] private Color color;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
            return;

        var entityManager = world.EntityManager;
        var query = entityManager.CreateEntityQuery(typeof(LocalTransform));
        int count = query.CalculateEntityCount();
        if (count == 0)
            return;

        Gizmos.color = color;

        using var transform = query.ToComponentDataArray<LocalTransform>(Allocator.Temp);

        int sample = math.min(maxDraw, transform.Length);
        int step = math.max(1, transform.Length / sample);
        int drawn = 0;

        for (int i = 0; i < transform.Length && drawn < sample; i += step)
        {
            var position = transform[i].Position;
            Gizmos.DrawWireCube(new(position.x, position.y, position.z), Vector3.one);
            drawn++;
        }
    }
}
