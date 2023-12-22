using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RollDiceButton : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    private Button button;
    [Inject] MoneyManager moneyManager;
    [Inject] DiceAwardManager diceAwardManager;
    public delegate IEnumerator OnRollDiceEventHandler();
    public static event OnRollDiceEventHandler OnRollDice;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        Dice.OnDisableRollDiceButton += IsButtonInteractable;
      //  DiceAwardManager.OnDiceGameBetValue += CanRollDice;
    }
    private void OnDisable()
    {
       // DiceAwardManager.OnDiceGameBetValue -= CanRollDice;
        Dice.OnDisableRollDiceButton -= IsButtonInteractable;

    }
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                CanRollDice();

            })
            .AddTo(subscriptions);

    }
    private void CanRollDice()
    {
        if (moneyManager.totalMoneyAmount >=diceAwardManager.betRate)
        {
            IsButtonInteractable(true);
        }
        else
        {
            IsButtonInteractable(false);
        }
    }


    private void OnButtonClickEvent()
    {
       StartCoroutine( OnRollDice?.Invoke());
    }

    private void IsButtonInteractable(bool state)
    {
        if (state)
        {
            button.interactable = state;
        }
        else
        {
            button.interactable = false;
        }
    }
}
