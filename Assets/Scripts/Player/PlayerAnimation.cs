using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private static string movementName = "Speed";
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
       
        PlayerInput.OnPlayerInput += MovementAnimation;
    }
    private void MovementAnimation(Vector3 movementVector)
    {
        if (movementVector.x != 0 || movementVector.z != 0)
        {
            anim.SetFloat(movementName, 1.0f);
        }
        else
        {
            anim.SetFloat(movementName, 0.0f);
        }

    }
    private void OnDisable()
    {
        PlayerInput.OnPlayerInput -= MovementAnimation;
    }
}
