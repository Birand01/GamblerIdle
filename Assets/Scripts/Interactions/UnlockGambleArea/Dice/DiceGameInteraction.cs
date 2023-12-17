using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameInteraction : InteractionBase
{
    public static event Action<int> OnEnableDiceGameCamera;
    protected override void OnTriggerEnterAction(Collider other)
    {
        OnEnableDiceGameCamera?.Invoke(1);
        Debug.Log("DICE GAME SCENE");
      
    }

  
}
