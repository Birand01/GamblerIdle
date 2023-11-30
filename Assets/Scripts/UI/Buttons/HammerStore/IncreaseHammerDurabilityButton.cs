using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHammerDurabilityButton : HammerStoreButton
{

    public static event Action<float> OnIncreaseHammerDurability;
    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();
        Debug.Log(this.gameObject.name);
        OnIncreaseHammerDurability?.Invoke(int.Parse(amountText.text));
    }
}
