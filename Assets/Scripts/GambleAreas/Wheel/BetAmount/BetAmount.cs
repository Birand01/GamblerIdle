using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BetAmount : MonoBehaviour
{
    private TMP_Text betValueText;
    private int initialBetValue = 100,maxBetValue=500;
    internal int currentBetValue;
    private void Awake()
    {
        betValueText = GetComponent<TMP_Text>();
        currentBetValue = initialBetValue;
        betValueText.text=currentBetValue.ToString();
    }
    private void OnEnable()
    {
        BetOptionBase.OnChangeBetAmount += OnChangeBetAmount;
    }

    private void OnDisable()
    {
        BetOptionBase.OnChangeBetAmount -= OnChangeBetAmount;

    }
    private void OnChangeBetAmount(int amount)
    {
        currentBetValue += amount;
        currentBetValue = Mathf.Clamp(currentBetValue, initialBetValue, maxBetValue);
        betValueText.text= currentBetValue.ToString();
    }
}
