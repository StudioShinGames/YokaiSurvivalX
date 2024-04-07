using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAnimatorAuthoring : MonoBehaviour
{
    public GameObject PlayerAnimator;
    private class Baker : Baker<PlayerAnimatorAuthoring>
    {
        public override void Bake(PlayerAnimatorAuthoring authoring)
        {
             Entity entity = GetEntity(TransformUsageFlags.Dynamic);

             AddComponentObject(entity, new PlayerAnimator { 
                animatorValue = authoring.PlayerAnimator
            });
        }
    }
   
}

public class PlayerAnimator : IComponentData {
    public GameObject animatorValue;
}

public class PlayerAnimatorReference : ICleanupComponentData {
    public Animator animatorValue;
}