using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MobMovementAuthoring : MonoBehaviour
{
    public float movementSpeed;

    private class Baker : Baker<MobMovementAuthoring>
    {
        public override void Bake(MobMovementAuthoring authoring)
        {
             Entity entity = GetEntity(TransformUsageFlags.Dynamic);
             AddComponent(entity, new MobMovement { 
                movementSpeed = authoring.movementSpeed, 
            });
        }
    }
}

public struct MobMovement : IComponentData
{
    public float movementSpeed;

}

