using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening.Core.Easing;
using System;

public class HammerDurabilityUI : MonoBehaviour
{
    public static event Action<float, float> OnUpdateHammerCurrentDuration;
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private TMP_Text currentDamageText, totalDurabilityText;

   

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
        currentDamageText.text = String.Format("{0:0.0}",HammerDurability.Instance.takenDamage);
        totalDurabilityText.text = String.Format("{0:0}", HammerDurability.Instance.totalHammerDurability);
        OnUpdateHammerCurrentDuration?.Invoke(HammerDurability.Instance.takenDamage, HammerDurability.Instance.totalHammerDurability);
    }

    private void DecreaseHammerDamage(float healAmount)
    {
        float currentDamageAmount = HammerDurability.Instance.takenDamage;
        currentDamageAmount -= healAmount;
        currentDamageAmount=Mathf.Clamp(currentDamageAmount, 0f, HammerDurability.Instance.totalHammerDurability);
        currentDamageText.text = String.Format("{0:0.0}", currentDamageAmount);
    }

    private void IncreaseHammerDurability(float amount)
    {
        HammerDurability.Instance.totalHammerDurability += amount;
        totalDurabilityText.text = String.Format("{0:0}", HammerDurability.Instance.totalHammerDurability);
    }

   
}
