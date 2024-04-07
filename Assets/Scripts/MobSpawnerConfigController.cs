using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct MobSpawnerConfigController : ISystem
{
   public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MobSpawnerConfig>();
    }

    public void OnUpdate(ref SystemState state)
    {
        //state.Enabled = false;

        MobSpawnerConfig mobSpawner = SystemAPI.GetSingleton<MobSpawnerConfig>();
        EntityCommandBuffer commandBuffer = new EntityCommandBuffer(state.WorldUpdateAllocator);
        
        if(mobSpawner.nextSpawnTime < SystemAPI.Time.ElapsedTime){


            NativeArray<Entity> multipleMobArray = new NativeArray<Entity>(mobSpawner.amountToSpawn, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            
            commandBuffer.Instantiate(mobSpawner.mobEntity, multipleMobArray);
            
            foreach(Entity newMob in multipleMobArray){

                commandBuffer.SetComponent(newMob, new LocalTransform {
                        Position = new float3(UnityEngine.Random.Range(-10f, +5), 1.0f, UnityEngine.Random.Range(-4f, +7)),
                        Scale = 1f
                });
            }
            

             SystemAPI.SetSingleton( new MobSpawnerConfig {
                amountToSpawn = mobSpawner.amountToSpawn,
                spawnRate = mobSpawner.spawnRate,
                mobEntity = mobSpawner.mobEntity,
                nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + mobSpawner.spawnRate
             });


            commandBuffer.Playback(state.EntityManager);
        }

    }
}
