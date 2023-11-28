using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveSpeed;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
   

    internal void CharacterMovement(Vector3 movementVector)
    {
        _rigidbody.MovePosition(transform.position + movementVector * Time.deltaTime * moveSpeed);
    }

    private void StopPlayer(bool state)
    {
        if (state)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _rigidbody.velocity = Vector3.zero;
        }
        else
            return;
      
    }
    public void CharacterAngularMovement(Vector3 moveVector)
    {
        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, moveSpeed, 0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
   
}
