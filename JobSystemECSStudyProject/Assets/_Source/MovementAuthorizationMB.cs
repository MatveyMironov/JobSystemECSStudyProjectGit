using Unity.Entities;
using UnityEngine;

public class MovementAuthorizationMB : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float radius;

    public float MovementSpeed { get => movementSpeed; }
    public float Radius { get => radius; }
}

public struct MovementSpeed : IComponentData
{
    public float Value;
}

public struct Radius : IComponentData
{
    public float Value;
}

public class MovementBaker : Baker<MovementAuthorizationMB>
{
    public override void Bake(MovementAuthorizationMB authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic | TransformUsageFlags.Renderable);

        MovementSpeed movementSpeed = new() { Value = authoring.MovementSpeed };
        AddComponent(entity, movementSpeed);

        Radius radius = new() { Value = authoring.Radius };
        AddComponent(entity, radius);
    }
}
