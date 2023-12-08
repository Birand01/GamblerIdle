using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BetOptionBase : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [Inject] protected BetAmount bet;
    protected Button button;
    [SerializeField] protected TMP_Text betValueText;

    public static event Action<int> OnChangeBetAmount;
    [SerializeField] protected int betAmount;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }

    private void OnDisable()
    {
        subscriptions.Clear();
    }

    protected virtual void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                LimitationOfButtons();

            })
            .AddTo(subscriptions);


    }
    protected virtual void OnButtonClickEvent()
    {
        OnChangeBetAmount?.Invoke(betAmount);
    }


    protected abstract void LimitationOfButtons();
    
}
