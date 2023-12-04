using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class HammerStoreInteraction : InteractionBase
{
    public static event Action<CanvasType> OnSwitchHammerStoreUI;
    public static event Action<bool> OnFreezePlayerMovementState;
    protected override void OnTriggerEnterAction(Collider other)
    {
        OnSwitchHammerStoreUI?.Invoke(CanvasType.HammerStoreUI);
        OnFreezePlayerMovementState?.Invoke(true);
    }

    protected override void OnTriggerExitAction(Collider other)
    {
        OnSwitchHammerStoreUI?.Invoke(CanvasType.GameUI);
    }

}
