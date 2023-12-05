using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] internal float totalMoneyAmount;
    [SerializeField] private TMP_Text wheelGameMoney;
   
    private void Awake()
    {
        moneyCounterText.text=totalMoneyAmount.ToString();
        wheelGameMoney.text = totalMoneyAmount.ToString();
    }
    private void OnEnable()
    {
        UnlockBuildingBase.OnPayForUnlockingGambleArea += DecreaseMoneyAmount;
        DiamondManager.OnExchangeDiamondToMoney += IncreaseMoneyAmount;
        HammerStoreButton.OnDecreaseMoneyAmount += DecreaseMoneyAmount;
    }
    private void OnDisable()
    {
        HammerStoreButton.OnDecreaseMoneyAmount -= DecreaseMoneyAmount;
        DiamondManager.OnExchangeDiamondToMoney -= IncreaseMoneyAmount;
        UnlockBuildingBase.OnPayForUnlockingGambleArea -= DecreaseMoneyAmount;

    }
    private void DecreaseMoneyAmount(int amount)
    {    
        totalMoneyAmount-=amount;
        totalMoneyAmount=Mathf.Clamp(totalMoneyAmount, 0, float.MaxValue);
        moneyCounterText.text = totalMoneyAmount.ToString();
        wheelGameMoney.text = totalMoneyAmount.ToString();

    }

    private void IncreaseMoneyAmount(int amount)
    {
        totalMoneyAmount+=amount;
        moneyCounterText.text = totalMoneyAmount.ToString();
        wheelGameMoney.text = totalMoneyAmount.ToString();


    }


   
}
