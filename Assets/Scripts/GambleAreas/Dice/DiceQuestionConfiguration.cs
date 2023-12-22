using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceQuestionConfiguration : MonoBehaviour
{
    [SerializeField] internal DiceGameQuestionSO gameQuestionSO;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text betRateText;
    [SerializeField] internal TMP_Text expectedDiceValue;
  
    private void OnEnable()
    {
       
        SetUpConfiguration();

    }
    private void SetUpConfiguration()
    {
        
            questionText.text = gameQuestionSO.questionText.ToString();
            betRateText.text = gameQuestionSO.betRate.ToString();
            expectedDiceValue.text = gameQuestionSO.expectedRolledDiceValue.ToString();       
    }

}
