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
    [Range(0, 36)] public int expectedRolledDiceValue;
   
    private void OnValidate()
    {
        SetUpOperationValue();
    }
    private void SetUpOperationValue()
    {
        if(operation == Operation.Sum)
        {
            expectedRolledDiceValue = Mathf.Clamp(expectedRolledDiceValue,2,12);
        }
        else if(operation == Operation.Multiplication)
        {
            expectedRolledDiceValue = Mathf.Clamp(expectedRolledDiceValue, 1, 36);

        }
        else if(operation == Operation.Difference)
        {
            expectedRolledDiceValue = Mathf.Clamp(expectedRolledDiceValue,0, 5);

        }
    }

   
}
