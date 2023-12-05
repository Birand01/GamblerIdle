using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BetOptionBase : MonoBehaviour
{
    protected Button button;
    [SerializeField] protected TMP_Text betValueText;

    public static event Action<int> OnChangeBetAmount;
    [SerializeField] protected int betAmount;
    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }
   

    protected virtual void Start()
    {
        button.onClick.AddListener(OnButtonClickEvent);
    }

    protected virtual void OnButtonClickEvent()
    {
        OnChangeBetAmount?.Invoke(betAmount);
    }
    
}
