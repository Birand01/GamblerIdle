using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyCounterText;
    [SerializeField] internal float totalMoneyAmount;

    public static MoneyManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        moneyCounterText.text=totalMoneyAmount.ToString();
    }
    private void OnEnable()
    {
        IncreaseHammerDurabilityButton.OnDecreaseMoneyAmount += DecreaseMoneyAmount;
    }
    private void OnDisable()
    {
        IncreaseHammerDurabilityButton.OnDecreaseMoneyAmount -= DecreaseMoneyAmount;

    }
    private void DecreaseMoneyAmount(float amount)
    {
        totalMoneyAmount-=amount;
        totalMoneyAmount=Mathf.Clamp(totalMoneyAmount, 0, float.MaxValue);
        moneyCounterText.text = totalMoneyAmount.ToString();
    }
}
