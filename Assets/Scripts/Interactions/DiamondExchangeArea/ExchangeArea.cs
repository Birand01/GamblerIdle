using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeArea : InteractionBase
{
    public delegate void OnPlayerStartDropMoneyHandler(Transform transform);
    public static event OnPlayerStartDropMoneyHandler OnPlayerStartDropMoney;

    public delegate void OnPlayerFinishDropMoneyHandler();
    public static event OnPlayerFinishDropMoneyHandler OnPlayerStopDropMoney;

    protected override void OnTriggerEnterAction(Collider other)
    {
        OnPlayerStartDropMoney?.Invoke(this.transform);
    }

    protected override void OnTriggerExitAction(Collider other)
    {
        OnPlayerStopDropMoney?.Invoke();
    }
}
