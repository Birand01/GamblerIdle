using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class DiceAwardManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static event Action<int> OnDiceGameBetValue;
    public static event Action<Operation, int> OnExpectedDiceOutCome;

    internal Operation currentOperation;
    internal int currentExpectedDiceValue, betRate;
   
    private void OnEnable()
    {
       
        StartCoroutine(Subscribe());
        Dice.OnDiceCalculation += QuestionStatusCheck;
       
       
    }
    private void Start()
    {
       
    }

    private void OnDisable()
    {
        Dice.OnDiceCalculation -= QuestionStatusCheck;
        subscriptions.Clear();
    }

    private IEnumerator QuestionStatusCheck(int sum,int multiplication,int difference)
    {
        yield return null;
        for (int j = 0; j < transform.childCount; j++)
        {
            if (transform.GetChild(j).gameObject.activeInHierarchy)
            {
                betRate = transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.betRate;
                currentOperation = transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation;
                currentExpectedDiceValue = transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.expectedRolledDiceValue;

                switch (currentOperation)
                {
                    case Operation.Sum:
                        if(sum==currentExpectedDiceValue)
                        {
                            Debug.Log("A");
                        }
                        else
                        {
                            Debug.Log("B");
                        }
                        break;
                    case Operation.Multiplication:
                        break;
                    case Operation.Difference:
                        break;
                    default:
                        break;
                }

                //OnExpectedDiceOutCome?.Invoke(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation,
                // transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.expectedRolledDiceValue);
                // Debug.Log(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation + "--" + transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.expectedRolledDiceValue);
                break;
            }
        }
       
    }

   

    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
                // QuestionStatusCheck();
                //DiceRollButtonInteractibility();
            })
            .AddTo(subscriptions);

    }
  
}
