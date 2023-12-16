using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceGameExitButton : MonoBehaviour
{
    private Button button;
    public static event Action OnExitFromDiceGame;
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
        OnExitFromDiceGame?.Invoke();


    }
}
