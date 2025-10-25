using Unity.Entities;
using UnityEngine;

public class MovementAuthorizationMB : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    public float MovementSpeed { get => movementSpeed; }
    public float MaxY { get => maxY; }
    public float MinY { get => minY; }
}

public struct MovementComponent : IComponentData
{
    public float Speed;
    public float MaxY;
    public float MinY;
}

public struct DirectionComponent : IComponentData
{
    public bool IsMovingUp;
}

public class MovementBaker : Baker<MovementAuthorizationMB>
{
    public override void Bake(MovementAuthorizationMB authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic | TransformUsageFlags.Renderable);

        MovementComponent movement = new()
        {
            Speed = authoring.MovementSpeed,
            MaxY = authoring.MaxY,
            MinY = authoring.MinY,
        };
        AddComponent(entity, movement);

        int i = Random.Range(0, 2);
        bool isMovingUp = i == 0;
        DirectionComponent direction = new() { IsMovingUp = isMovingUp };
        AddComponent(entity, direction);
    }
}
