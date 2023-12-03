using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGameInteraction : InteractionBase
{
    protected override void OnTriggerEnterAction(Collider other)
    {
        Debug.Log("WELCOME TO WHEEL GAMBLE GAME");
    }
}
