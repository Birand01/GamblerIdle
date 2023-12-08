using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DecreaseBet : BetOptionBase
{
    protected override void LimitationOfButtons()
    {
        if (bet.currentBetValue != bet.initialBetValue)
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = false;

        }

    }
}
