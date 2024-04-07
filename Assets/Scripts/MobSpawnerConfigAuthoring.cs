using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MobSpawnerAuthoring : MonoBehaviour
{
    public GameObject mobEntity;
    public int amountToSpawn;

    public float spawnRate;

    public class Baker : Baker<MobSpawnerAuthoring>
    {
        public override void Bake(MobSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new MobSpawnerConfig {
                mobEntity = GetEntity(authoring.mobEntity, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn,
                nextSpawnTime = 0.0f,
                spawnRate = authoring.spawnRate
            });
        }
    }
}

public struct MobSpawnerConfig : IComponentData
{
    
    public Entity mobEntity;
    public int amountToSpawn;
    
    public float nextSpawnTime;
    public float spawnRate;
}
