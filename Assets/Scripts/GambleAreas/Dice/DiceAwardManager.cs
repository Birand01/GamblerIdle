using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class DiceAwardManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static event Action<int> OnDiceGameBetValue;
    public static event Action<Operation, int> OnExpectedDiceOutCome;
    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        for (int j = 0; j < transform.childCount; j++)
        {
            if (transform.GetChild(j).gameObject.activeInHierarchy)
            {
                OnDiceGameBetValue?.Invoke(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.betRate);
                // Debug.Log(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.betRate);
                OnExpectedDiceOutCome?.Invoke(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation,
                    transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.rolledDiceValue);
                Debug.Log(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation + "--" + transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.rolledDiceValue);
                break;
            }
        }
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }
    private IEnumerator Subscribe()
    {
        yield return null;
        this.UpdateAsObservable()
            .Subscribe(value =>
            {
               

            })
            .AddTo(subscriptions);

    }
  
}
