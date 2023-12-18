using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] internal int totalMoneyAmount;
    [SerializeField] private TMP_Text wheelGameMoneyText, diceGameMoneyText;
   
    private void Awake()
    {
        moneyCounterText.text=totalMoneyAmount.ToString();
        wheelGameMoneyText.text = totalMoneyAmount.ToString();
        diceGameMoneyText.text=totalMoneyAmount.ToString();
    }
    private void OnEnable()
    {
        Wheel.OnGiveRewardMoney += IncreaseWheelMoneyAmount;
        SpinButton.OnUpdateTotalMoneyAmount += DecreaseMoneyAmount;
        UnlockBuildingBase.OnPayForUnlockingGambleArea += DecreaseMoneyAmount;
        DiamondManager.OnExchangeDiamondToMoney += IncreaseDiamondMoneyAmount;
        HammerStoreButton.OnDecreaseMoneyAmount += DecreaseMoneyAmount;
    }
    private void OnDisable()
    {
        Wheel.OnGiveRewardMoney -= IncreaseWheelMoneyAmount;
        HammerStoreButton.OnDecreaseMoneyAmount -= DecreaseMoneyAmount;
        DiamondManager.OnExchangeDiamondToMoney -= IncreaseDiamondMoneyAmount;
        UnlockBuildingBase.OnPayForUnlockingGambleArea -= DecreaseMoneyAmount;
        SpinButton.OnUpdateTotalMoneyAmount -= DecreaseMoneyAmount;

    }
    private void DecreaseMoneyAmount(int amount)
    {    
        totalMoneyAmount-=amount;
        totalMoneyAmount=Mathf.Clamp(totalMoneyAmount, 0, int.MaxValue);
        moneyCounterText.text = totalMoneyAmount.ToString();
        wheelGameMoneyText.text = totalMoneyAmount.ToString();
        diceGameMoneyText.text = totalMoneyAmount.ToString();
    }
    private void IncreaseDiamondMoneyAmount(int amount)
    {
        totalMoneyAmount += amount;
        moneyCounterText.text = totalMoneyAmount.ToString();
    }
    private void IncreaseWheelMoneyAmount(int amount)
    {
       
        StartCoroutine(UpdateCoinsAmount(wheelGameMoneyText, amount));

    }
    private IEnumerator UpdateCoinsAmount(TMP_Text totalMoneyText,int amount)
    {
        const float seconds = 0.5f; // Animation duration
        float elapsedTime = 0;
        int currentMoneyAmount=totalMoneyAmount+amount;
        while (elapsedTime < seconds)
        {
            totalMoneyText.text = Mathf.Floor(Mathf.Lerp(totalMoneyAmount, currentMoneyAmount, (elapsedTime / seconds))).ToString();
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        totalMoneyAmount = currentMoneyAmount;

        totalMoneyText.text = currentMoneyAmount.ToString();
    }


}
