using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void PlayerInputHandler(Vector3 movement);
    public static event PlayerInputHandler OnPlayerInput;


    [SerializeField] private DynamicJoystick joystick;
    private Vector3 movementVector;

    private void FixedUpdate()
    {
        JoyStickMovement(movementVector);
    }

    private void JoyStickMovement(Vector3 moveVector)
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.HorizontalAxis();
        moveVector.z = joystick.VerticalAxis();
        OnPlayerInput?.Invoke(moveVector);

    }
}
