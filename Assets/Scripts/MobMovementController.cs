using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics.Authoring;
using Unity.Transforms;
using UnityEngine;

public partial struct MobMovementController : ISystem
{
    private float3 PlayerPos;
    

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<MobMovement>();
        state.RequireForUpdate<PlayerAnimatorReference>();
    }
    public void OnUpdate(ref SystemState state)
    {
        foreach(var animatorReference in SystemAPI.Query<PlayerAnimatorReference>()){
            PlayerPos = animatorReference.animatorValue.transform.position;
        }

        WalkJob rotationJob = new WalkJob {
            deltaTime = SystemAPI.Time.DeltaTime,
            playerPos = PlayerPos
        };
        rotationJob.Schedule();
    }

    [WithNone(typeof(PlayerTag))]
    public partial struct WalkJob: IJobEntity {
        public float deltaTime;
        public float3 playerPos;
        public void Execute(ref LocalTransform localTransform, in MobMovement mobMovementData ){

            float3 movementVector = new float3(playerPos.x, 0, playerPos.z) - new float3(localTransform.Position.x, 0, localTransform.Position.z);

            localTransform.Position += math.normalize(movementVector) * mobMovementData.movementSpeed * deltaTime;

        }
    }
}
