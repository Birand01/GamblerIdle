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
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static event Action<float> OnDecreaseMoneyAmount;
    public static event Action<float> OnIncreasePriceValue;
    public static event Action<float> OnIncreaseSkillValue;

    [SerializeField] protected TMP_Text priceText, amountText;
    [SerializeField] protected float initialPriceAmount,initialSkillAmount,constantMultiplier;
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
        initialSkillAmount += constantMultiplier;
        initialPriceAmount += constantMultiplier;
        InitializeButtonValues();

    }
    protected virtual void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
        InitializeButtonValues();
    }

    private void InitializeButtonValues()
    {
        priceText.text=initialPriceAmount.ToString();
        amountText.text=initialSkillAmount.ToString();
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
