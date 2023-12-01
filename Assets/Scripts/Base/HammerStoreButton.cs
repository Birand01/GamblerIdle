using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
public abstract class HammerStoreButton : MonoBehaviour
{
    [SerializeField] private StoreButtonSO storeButtonSO;
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static event Action<float> OnDecreaseMoneyAmount;
   

    [SerializeField] protected TMP_Text priceText, amountText;
   
    protected Button button;
    protected virtual void Awake()
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
    protected virtual void OnButtonClickEvent()
    {
        OnDecreaseMoneyAmount?.Invoke(int.Parse(priceText.text));
        IncreaseButtonValues();
    }
    private void IncreaseButtonValues()
    {
        storeButtonSO.skillAmount += storeButtonSO.multiplier;
        storeButtonSO.price += storeButtonSO.multiplier;
        InitializeButtonValues();

    }
    protected virtual void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
        InitializeButtonValues();
    }

    private void InitializeButtonValues()
    {
        priceText.text= storeButtonSO.price.ToString();
        amountText.text= storeButtonSO.skillAmount.ToString();
    }

    protected virtual IEnumerator Subscribe()
    {
        yield return null;

        this.UpdateAsObservable().Subscribe(x =>
        {
            if ((float.Parse(priceText.text) <= MoneyManager.Instance.totalMoneyAmount))
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
