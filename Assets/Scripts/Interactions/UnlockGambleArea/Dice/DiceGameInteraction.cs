using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameInteraction : InteractionBase
{

    public static event Action OnGoToDiceGameScene;
 
    protected override void OnTriggerEnterAction(Collider other)
    {
        Debug.Log("DICE GAME SCENE");
        OnGoToDiceGameScene?.Invoke();
    }

  
}
