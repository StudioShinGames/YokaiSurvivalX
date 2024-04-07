using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
public partial struct PlayerAnimatorController : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<InputComponent>();
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer commandBuffer = new EntityCommandBuffer(state.WorldUpdateAllocator);
        InputComponent inputEntity = SystemAPI.GetSingleton<InputComponent>();

        foreach(var (playerAnimator, entity) in SystemAPI.Query<PlayerAnimator>().WithNone<PlayerAnimatorReference>().WithEntityAccess()){

            GameObject newCompanionGameObject = GameObject.Instantiate(playerAnimator.animatorValue);
            
            PlayerAnimatorReference newAnimatorRef = new PlayerAnimatorReference {
                animatorValue = newCompanionGameObject.GetComponent<Animator>() 
            };

            commandBuffer.AddComponent(entity, newAnimatorRef);
        }
        
        foreach(var (transform, animatorReference, playerProps) in SystemAPI.Query<LocalTransform, PlayerAnimatorReference, PlayerComponent>()){

            float2 movementVector = inputEntity.movement;

            //if(movementVector.x != 0f && movementVector.y != 0f){

                Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
                var vel = inputVector * playerProps.speed;

                animatorReference.animatorValue.SetFloat("speed", vel.magnitude );
                animatorReference.animatorValue.transform.SetPositionAndRotation(transform.Position, transform.Rotation);
            //}

            if(inputEntity.basicAttack == 1){
                animatorReference.animatorValue.SetTrigger("basic-attack");
            }
        }

        foreach (var (animatorReference, entity) in SystemAPI.Query<PlayerAnimatorReference>().WithNone<PlayerAnimator, LocalTransform>().WithEntityAccess()){
            GameObject.Destroy(animatorReference.animatorValue.gameObject);
            commandBuffer.RemoveComponent<PlayerAnimatorReference>(entity);
        }

        commandBuffer.Playback(state.EntityManager);
        commandBuffer.Dispose();
    }
}
