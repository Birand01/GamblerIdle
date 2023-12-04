using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelGameInteraction : InteractionBase
{
    public static event Action<CanvasType> OnSwitchWheelGambleUI;
    protected override void OnTriggerEnterAction(Collider other)
    {
        OnSwitchWheelGambleUI?.Invoke(CanvasType.WheelGambleUI);
    }
}
