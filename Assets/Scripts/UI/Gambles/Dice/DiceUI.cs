using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text diceOneText, diceTwoText;
    private void OnEnable()
    {
        Dice.OnDiceResult += SetText;
    }
    private void OnDisable()
    {
        Dice.OnDiceResult -= SetText;

    }

    private void SetText(int diceIndex, int diceResult)
    {
        if(diceIndex == 0)
        {
            diceOneText.text=diceResult.ToString();
        }
        else if(diceIndex == 1)
        {
            diceTwoText.text = diceResult.ToString();
        }
    }
}
