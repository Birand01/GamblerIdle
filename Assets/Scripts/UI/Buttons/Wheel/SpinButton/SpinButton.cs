using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Triggers;
using Zenject;

public class SpinButton : MonoBehaviour
{
    [Inject] MoneyManager moneyManager;
    [Inject] BetAmount betAmount;
    [Inject] Wheel wheel;
    private CompositeDisposable subscriptions = new CompositeDisposable();
    private Button button;

    public static event Action OnSpinWheel;
    public static event Action<int> OnUpdateTotalMoneyAmount;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    protected virtual void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    protected virtual void OnDisable()
    {

        subscriptions.Clear();
    }

    private void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }

    private void OnButtonClickEvent()
    {
        Debug.Log("SPIN BUTTON IS CLICKED");
        OnSpinWheel?.Invoke();
        OnUpdateTotalMoneyAmount?.Invoke(betAmount.currentBetValue);
    }

    protected virtual IEnumerator Subscribe()
    {
        yield return null;

        this.UpdateAsObservable().Subscribe(x =>
        {
            if ((betAmount.currentBetValue) <= moneyManager.totalMoneyAmount && !wheel._isStarted)
            {

                IsButtonInteractable(true);
            }
            else
            {
                IsButtonInteractable(false);
            }

        })
            .AddTo(subscriptions);
    }
    protected void IsButtonInteractable(bool state)
    {
        button.interactable = state;
    }
}
