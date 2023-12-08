using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] internal int totalMoneyAmount;
    [SerializeField] private TMP_Text wheelGameMoney;
   
    private void Awake()
    {
        moneyCounterText.text=totalMoneyAmount.ToString();
        wheelGameMoney.text = totalMoneyAmount.ToString();
    }
    private void OnEnable()
    {
        SpinButton.OnUpdateTotalMoneyAmount += DecreaseMoneyAmount;
        UnlockBuildingBase.OnPayForUnlockingGambleArea += DecreaseMoneyAmount;
        DiamondManager.OnExchangeDiamondToMoney += IncreaseMoneyAmount;
        HammerStoreButton.OnDecreaseMoneyAmount += DecreaseMoneyAmount;
    }
    private void OnDisable()
    {
        HammerStoreButton.OnDecreaseMoneyAmount -= DecreaseMoneyAmount;
        DiamondManager.OnExchangeDiamondToMoney -= IncreaseMoneyAmount;
        UnlockBuildingBase.OnPayForUnlockingGambleArea -= DecreaseMoneyAmount;
        SpinButton.OnUpdateTotalMoneyAmount -= DecreaseMoneyAmount;

    }
    private void DecreaseMoneyAmount(int amount)
    {    
        totalMoneyAmount-=amount;
        totalMoneyAmount=Mathf.Clamp(totalMoneyAmount, 0, int.MaxValue);
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
