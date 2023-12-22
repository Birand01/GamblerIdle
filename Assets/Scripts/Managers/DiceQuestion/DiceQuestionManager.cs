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

    


    private int currentQuestion;
    internal Operation operation;
    internal int expectedRolledDiceValue;
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
           
        }
       
    }

    
    
    
    public void ChangeQuestion(int change)
    {
        currentQuestion += change;
        SelectQuestion(currentQuestion);
       
    }

   
}
