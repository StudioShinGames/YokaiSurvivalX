using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct PlayerComponentController : ISystem
{

    private Entity gamePlayer;
    private Entity inputEntity;
    private EntityManager entityManager;
    private PlayerComponent playerComponent;
    private InputComponent inputComponent;
    

    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        gamePlayer = SystemAPI.GetSingletonEntity<PlayerComponent>();
        inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();

        playerComponent = entityManager.GetComponentData<PlayerComponent>(gamePlayer);
        inputComponent = entityManager.GetComponentData<InputComponent>(inputEntity);

        Movement(ref state);
        basicAttack(ref state);
    }

    private void Movement(ref SystemState state){
        LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(gamePlayer);

        float2 movementVector = inputComponent.movement;

        float3 inputVector = new float3(movementVector.x, 0, movementVector.y);
        var vel = inputVector * playerComponent.speed;
        var rot = Vector3.Slerp( playerTransform.Forward(), inputVector, SystemAPI.Time.DeltaTime * playerComponent.speedR); 
        
        playerTransform.Position += vel * SystemAPI.Time.DeltaTime;
        playerTransform.Rotation = Quaternion.LookRotation(rot);

        entityManager.SetComponentData(gamePlayer, playerTransform);
    }

    private void basicAttack(ref SystemState state){
        if(inputComponent.basicAttack > 0){
            
        }
    }
}
