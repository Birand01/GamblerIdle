using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDiggingRateButton : HammerStoreButton
{
    public static event Action OnIncreaseDiggingRate;
    protected override void OnButtonClickEvent()
    {
        OnIncreaseDiggingRate?.Invoke();
        base.OnButtonClickEvent();
    }
}
