using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RollDiceButton : MonoBehaviour
{
    private Button button;

    public delegate IEnumerator OnRollDiceEventHandler();
    public static event OnRollDiceEventHandler OnRollDice;

    private void OnEnable()
    {
        Dice.OnDisableRollDiceButton += IsButtonInteractable;
    }
    private void OnDisable()
    {
        Dice.OnDisableRollDiceButton -= IsButtonInteractable;

    }
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }

    private void OnButtonClickEvent()
    {
       StartCoroutine( OnRollDice?.Invoke());
    }

    private void IsButtonInteractable(bool state)
    {
        if (state)
        {
            button.interactable = state;
        }
        else
        {
            button.interactable = false;
        }
    }
}
