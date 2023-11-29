using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHammerDurabilityButton : HammerStoreButton
{
    public static event Action<float> OnDecreaseMoneyAmount;

    public static event Action<float> OnIncreaseHammerDurability;
    protected override void OnButtonClickEvent()
    {
        OnDecreaseMoneyAmount?.Invoke(int.Parse(priceText.text));
        Debug.Log(this.gameObject.name);
        OnIncreaseHammerDurability?.Invoke(int.Parse(amountText.text));
    }
}
