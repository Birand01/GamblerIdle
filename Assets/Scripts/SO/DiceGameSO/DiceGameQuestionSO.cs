using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Operation
{
    Sum,
    Multiplication,
    Difference
}
[CreateAssetMenu(fileName = "DiceGameQuestion", menuName = "ScriptableObjects/DiceGameQuestion", order = 2)]
public class DiceGameQuestionSO : ScriptableObject
{
    public Operation operation;
    public string questionText;
    public int betRate;
    [Range(0, 36)] public int rolledDiceValue;
    private void OnEnable()
    {
        Dice.OnDiceResult += CalculateAward;
    }
    private void OnDisable()
    {
        Dice.OnDiceResult -= CalculateAward;

    }
    private void OnValidate()
    {
        SetUpOperationValue();
    }
    private void SetUpOperationValue()
    {
        if(operation == Operation.Sum)
        {
            rolledDiceValue = Mathf.Clamp(rolledDiceValue,2,12);
        }
        else if(operation == Operation.Multiplication)
        {
            rolledDiceValue = Mathf.Clamp(rolledDiceValue, 1, 36);

        }
        else if(operation == Operation.Difference)
        {
            rolledDiceValue = Mathf.Clamp(rolledDiceValue,0, 5);

        }
    }

    private void CalculateAward(int topface)
    {
        if (operation == Operation.Sum)
        {
            int result = topface + topface;
            Debug.Log("SUM IS :" +result);
            Debug.Log(operation);
        }
        else if (operation == Operation.Multiplication)
        {
            int result = topface * topface;
            Debug.Log("MULTIPLICATION IS : "+result);
            Debug.Log(operation);

        }
        else if (operation == Operation.Difference)
        {
            int result =Mathf.Abs(topface - topface);
            Debug.Log("DIFFERENCE IS " +result);
            Debug.Log(operation);

        }
    }
}
