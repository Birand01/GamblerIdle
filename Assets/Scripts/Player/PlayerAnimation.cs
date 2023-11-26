using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private static string movementAnimName = "Speed";
    private static string breakingObjectAnimName = "Breaking";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        StoneInteraction.OnPlayerBreakObjectAnim += ObjectBreakAnimation;
        PlayerInput.OnPlayerInput += MovementAnimation;
    }
    private void OnDisable()
    {
        StoneInteraction.OnPlayerBreakObjectAnim -= ObjectBreakAnimation;
        PlayerInput.OnPlayerInput -= MovementAnimation;
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
    public void ScoreTextEvent()
    {
        Debug.Log("DONE!");
    }
    private void ObjectBreakAnimation(bool state)
    {
        anim.SetBool(breakingObjectAnimName, state);
    }
    
}
