using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicInteraction : InteractionBase
{
    protected override void OnTriggerEnterAction(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
