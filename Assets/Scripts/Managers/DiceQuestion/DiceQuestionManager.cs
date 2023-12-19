using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceQuestionManager : MonoBehaviour
{
    [SerializeField] internal List<DiceGameQuestionSO> gameQuestionsSO;
    [SerializeField] private GameObject diceQuestionPrefab;
    [SerializeField] private int numberOfQuestion;
    [SerializeField] private Button prevButton, nextButton;

    public static event Action<int> OnDiceGameBetValue;
    public static event Action<Operation,int> OnExpectedDiceOutCome;


    private int currentQuestion;
    private void OnEnable()
    {
        InstantiateQuestion();
        SelectQuestion(0);
    }
   

    private void InstantiateQuestion()
    {
        for (int i = 0; i < numberOfQuestion; i++)
        {
            GameObject diceQuestion = Instantiate(diceQuestionPrefab, transform.position, transform.rotation);
            diceQuestion.transform.parent = transform;   
            diceQuestion.gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO = gameQuestionsSO[i];
            diceQuestion.gameObject.SetActive(false);
        }
    }
    private void SelectQuestion(int index)
    {
        prevButton.interactable = (index != 0);
        nextButton.interactable= (index != transform.childCount-1);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
            for (int j = 0; j < transform.childCount; j++)
            {
                if(transform.GetChild(j).gameObject.activeInHierarchy)
                {
                    OnDiceGameBetValue?.Invoke(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.betRate);
                   // Debug.Log(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.betRate);
                    OnExpectedDiceOutCome?.Invoke(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation, 
                        transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.rolledDiceValue);
                    Debug.Log(transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.operation +"--"+ transform.GetChild(j).gameObject.GetComponent<DiceQuestionConfiguration>().gameQuestionSO.rolledDiceValue);
                    break;
                }
            }
        }
       
    }
    
    
    public void ChangeQuestion(int change)
    {
        currentQuestion += change;
        SelectQuestion(currentQuestion);
       
    }

   
}
