using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGameInteraction : InteractionBase
{
    public static event Action<bool> OnActivateDiceTable;
    protected override void OnTriggerEnterAction(Collider other)
    {
        OnActivateDiceTable?.Invoke(true);
    }

    protected override void OnTriggerExitAction(Collider other)
    {
        OnActivateDiceTable?.Invoke(false);
    }
}
