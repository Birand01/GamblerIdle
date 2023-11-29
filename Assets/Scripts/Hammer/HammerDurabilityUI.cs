using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening.Core.Easing;
using System;
using Zenject;

public class HammerDurabilityUI : MonoBehaviour
{
    public static event Action<float, float> OnUpdateHammerCurrentDuration;
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private TMP_Text currentDamageText, totalDurabilityText;
    [Inject] HammerDurability hammerDurability;
   

    private void OnEnable()
    {
        IncreaseHammerDurabilityButton.OnIncreaseHammerDurability += IncreaseHammerDurability;
        DecreaseHammerDamageButton.OnDecreaseHammerDamage += DecreaseHammerDamage;
        StartCoroutine(Subscribe());
    }


    private void OnDisable()
    {
        IncreaseHammerDurabilityButton.OnIncreaseHammerDurability -= IncreaseHammerDurability;
        DecreaseHammerDamageButton.OnDecreaseHammerDamage -= DecreaseHammerDamage;
        subscriptions.Dispose();
    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                SetHammerDurability();

            })
            .AddTo(subscriptions);
       

    }



    private void SetHammerDurability()
    {
        currentDamageText.text = String.Format("{0:0.0}", hammerDurability.takenDamage);
        totalDurabilityText.text = String.Format("{0:0}", hammerDurability.totalHammerDurability);
        OnUpdateHammerCurrentDuration?.Invoke(hammerDurability.takenDamage, hammerDurability.totalHammerDurability);
    }

    private void DecreaseHammerDamage(float healAmount)
    {
        float currentDamageAmount = hammerDurability.takenDamage;
        currentDamageAmount -= healAmount;
        currentDamageAmount=Mathf.Clamp(currentDamageAmount, 0f, hammerDurability.totalHammerDurability);
        currentDamageText.text = String.Format("{0:0.0}", currentDamageAmount);
    }

    private void IncreaseHammerDurability(float amount)
    {
        hammerDurability.totalHammerDurability += amount;
        totalDurabilityText.text = String.Format("{0:0}", hammerDurability.totalHammerDurability);
    }

   
}
