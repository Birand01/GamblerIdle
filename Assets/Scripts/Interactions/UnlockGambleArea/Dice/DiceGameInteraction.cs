using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameInteraction : InteractionBase
{
    protected override void OnTriggerEnterAction(Collider other)
    {
        Debug.Log("WELCOME TO DICE GAMBLE GAME");
    }
}
