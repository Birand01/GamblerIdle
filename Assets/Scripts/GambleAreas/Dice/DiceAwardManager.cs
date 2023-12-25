using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DiceAwardManager : MonoBehaviour
{
    [Inject] MoneyManager moneyManager;
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static event Action<bool> OnDiceGameBetValue;
    public static event Action<Operation, int> OnExpectedDiceOutCome;

    public delegate IEnumerator OnDiceGameBetAwardHandler(int amount);

    public static event OnDiceGameBetAwardHandler OnGetAward;

    internal Operation currentOperation;
    internal int currentExpectedDiceValue, betRate;
   
    private void OnEnable()
    {
       
        StartCoroutine(Subscribe());
      
      
    }
   

    private void OnDisable()
    {
      
        subscriptions.Clear();
    }

    private void OnDiceRollButtonActivation()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            if (transform.GetChild(j).gameObject.activeInHierarchy)
            {
                betRate = transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.betRate;
                if (betRate < moneyManager.totalMoneyAmount)
                {
                    OnDiceGameBetValue?.Invoke(true);
                }
                else
                {
                    OnDiceGameBetValue?.Invoke(false);
                }
            }
        }
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
                           //StartCoroutine(OnGetAward?.Invoke(betRate));
                        }
                        else
                        {

                            //StartCoroutine(OnGetAward?.Invoke(-betRate));
                            moneyManager.totalMoneyAmount -= betRate;
                            moneyManager.moneyCounterText.text=moneyManager.totalMoneyAmount.ToString();
                            Debug.Log("TOTAL MONEY AMOUNT " +moneyManager.totalMoneyAmount);
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
                OnDiceRollButtonActivation();
            })
            .AddTo(subscriptions);

    }
  
}
