using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerComponentAuthoring : MonoBehaviour
{
    public float speed = 3f;
    public float speedR = 10f;
    public float attackCooldown = 10f;

    private class Baker : Baker<PlayerComponentAuthoring>
    {
        public override void Bake(PlayerComponentAuthoring authoring)
        {
             Entity entity = GetEntity(TransformUsageFlags.None);

             AddComponent(entity, new PlayerComponent { 
                speed = authoring.speed,
                speedR = authoring.speedR,
                attackCooldown = authoring.attackCooldown
            });
        }
    }
}

public struct PlayerComponent : IComponentData {

    public float speed;
    public float speedR;
    public float attackCooldown;
}
