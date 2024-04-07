using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial class InputController : SystemBase
{
    private InputActions inputActions;
    protected override void OnCreate()
    {
        if (!SystemAPI.TryGetSingleton<InputComponent>(out InputComponent input))
        {
            EntityManager.CreateEntity(typeof(InputComponent));
        }

        inputActions = new InputActions();
        inputActions.Enable();
    }
    protected override void OnUpdate()
    {
        float2 moveVector = inputActions.Player.Movement.ReadValue<Vector2>();
        float basicAttack = inputActions.Player.basicAttack.ReadValue<float>();

        SystemAPI.SetSingleton(new InputComponent { movement = moveVector, basicAttack = basicAttack });
    }
}
