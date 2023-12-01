using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    public AnimationClip clip;
    private static string movementAnimName = "Speed";
    private static string diggingAnimName = "Digging";
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        IncreaseDiggingRateButton.OnIncreaseDiggingRate += IncreaseDiggingAnimationSpeed;
        StoneAreaInteraction.OnStoneAreaInteractionExit += DiggingAnimation;
        HammerDurability.OnChangeStateOfDiggingAnimation += DiggingAnimation;
        PlayerInput.OnPlayerInput += MovementAnimation;
    }
    private void OnDisable()
    {
        HammerDurability.OnChangeStateOfDiggingAnimation -= DiggingAnimation;
        PlayerInput.OnPlayerInput -= MovementAnimation;
        StoneAreaInteraction.OnStoneAreaInteractionExit -= DiggingAnimation;
        IncreaseDiggingRateButton.OnIncreaseDiggingRate -= IncreaseDiggingAnimationSpeed;

    }
    private void MovementAnimation(Vector3 movementVector)
    {
        if (movementVector.x != 0 || movementVector.z != 0)
        {
            anim.SetFloat(movementAnimName, 1.0f);
        }
        else
        {
           
            anim.SetFloat(movementAnimName, 0.0f);
        }

    }

    private void IncreaseDiggingAnimationSpeed()
    {
        //anim.speed += 2f;
      
    }

    public void OnDurabilityDecreaseEvent()
    {
        
    }
    private void DiggingAnimation(bool state)
    {
        anim.SetBool(diggingAnimName, state);
       
    }
    
}
