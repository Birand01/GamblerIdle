using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseHammerDamageButton : HammerStoreButton
{
    public static event Action<float> OnDecreaseHammerDamage;
    protected override void OnButtonClickEvent()
    {
        base.OnButtonClickEvent();
        OnDecreaseHammerDamage?.Invoke(int.Parse(amountText.text));
        Debug.Log(this.gameObject.name);
    }
}
