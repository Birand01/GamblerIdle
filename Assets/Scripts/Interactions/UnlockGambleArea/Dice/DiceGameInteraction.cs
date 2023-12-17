using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameInteraction : InteractionBase
{
    public static event Action<CameraType> OnSwitchDiceGameCamera;
    protected override void OnTriggerEnterAction(Collider other)
    {
       
        OnSwitchDiceGameCamera?.Invoke(CameraType.diceGameCamera);
        Debug.Log("DICE GAME SCENE");
      
    }

  
}
