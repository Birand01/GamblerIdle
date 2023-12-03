using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public abstract class UnlockBuildingBase : MonoBehaviour
{
    public static event Action<int> OnPayForUnlockingGambleArea;

    [Inject] private MoneyManager moneyManager;
    
    [SerializeField] private int totalCost;
    [SerializeField] private TMP_Text unlockText;
    private int paidMoney;
    [SerializeField] private GameObject gamePlayArea;
    protected virtual void Awake()
    {
        paidMoney = 0;
        PriceTextIndicator();
    }
  
  
    private void PriceTextIndicator()
    {
        unlockText.text = string.Format("{0}/{1}", paidMoney, totalCost);
    }
    public virtual void UnlockArea(bool state)
    {
        if (moneyManager.totalMoneyAmount > 0 && state)
        {
            paidMoney += 1;
            OnPayForUnlockingGambleArea?.Invoke(1);
            PriceTextIndicator();
            unlockText.DOBlendableColor(Color.green, 0.5f).OnComplete(() => unlockText.DOColor(Color.white, 0.2f));
            if (paidMoney >= totalCost)
            {

                this.gameObject.SetActive(false);
                gamePlayArea.transform.DOScale(1, 0.1f).SetEase(Ease.InBounce);
            }
        }
        else
            return;
       
    }
    
}
